using UnityEngine ;
using UnityEngine.UI ;
using DG.Tweening ;
using UnityEngine.Events ;
using System.Collections.Generic ;

public class PickerWheel : MonoBehaviour
{

    [Header("References :")]

    [Space]
    [SerializeField] private Transform PickerWheelTransform;
    [SerializeField] private Transform wheelCircle;
    [SerializeField] private GameObject wheelPiecePrefab;
    [SerializeField] private Transform wheelPiecesParent;

    [Space]
    [Header("Sounds :")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tickAudioClip;
    [SerializeField] [Range(0f, 1f)] private float volume = .5f;
    [SerializeField] [Range(-3f, 3f)] private float pitch = 1f;

    [Space]
    [Header("Picker wheel settings :")]
    [Range(1, 20)] public int spinDuration = 8;
    [SerializeField] [Range(.2f, 2f)] private float wheelSize = 1f;

    [Space]
    [Header("Picker wheel pieces :")]
    public RewardSetsDatabase wheelPieceDatabase;

    private bool _isSpinning = false;

    public bool IsSpinning { get { return _isSpinning; } }


    private Vector2 pieceMinSize = new Vector2(81f, 146f);
    private Vector2 pieceMaxSize = new Vector2(144f, 213f);
    private int piecesMin = 2;
    private int piecesMax = 12;

    private float pieceAngle;
    private float halfPieceAngle;
    private float halfPieceAngleWithPaddings;


    private double accumulatedWeight;
    private System.Random rand = new System.Random();

    private List<int> nonZeroChancesIndices = new List<int>();

    private int currentZone = 0;

    private void Start()
    {
        currentZone = Utility.GetIntValue(PlayerPrefKeys.currentZone);

        ReSizePieceAngle();

        Generate();

        CalculateWeightsAndIndices();
        if (nonZeroChancesIndices.Count == 0)
            Debug.LogError("You can't set all pieces chance to zero");


        SetupAudio();
    }

    private void SetupAudio()
    {
        audioSource.clip = tickAudioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
    }

    private void ReSizePieceAngle()
    {
        pieceAngle = 360 / wheelPieceDatabase.rewardSets[currentZone].rewards.Count;
        halfPieceAngle = pieceAngle / 2f;
        halfPieceAngleWithPaddings = halfPieceAngle - (halfPieceAngle / 4f);
    }

    private void Generate()
    {
        wheelPiecePrefab = InstantiatePiece();

        RectTransform rt = wheelPiecePrefab.transform.GetChild(0).GetComponent<RectTransform>();
        float pieceWidth = Mathf.Lerp(pieceMinSize.x, pieceMaxSize.x, 1f - Mathf.InverseLerp(piecesMin, piecesMax, wheelPieceDatabase.rewardSets[currentZone].rewards.Count));
        float pieceHeight = Mathf.Lerp(pieceMinSize.y, pieceMaxSize.y, 1f - Mathf.InverseLerp(piecesMin, piecesMax, wheelPieceDatabase.rewardSets[currentZone].rewards.Count));
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pieceWidth);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pieceHeight);

        for (int i = 0; i < wheelPieceDatabase.rewardSets[currentZone].rewards.Count; i++)
            DrawPiece(i);

        Destroy(wheelPiecePrefab);
    }

    private void DrawPiece(int index)
    {
        Reward piece = wheelPieceDatabase.rewardSets[currentZone].rewards[index];
        Transform pieceTrns = InstantiatePiece().transform.GetChild(0);

        pieceTrns.GetChild(0).GetComponent<Image>().sprite = piece.Icon;
        //pieceTrns.GetChild(1).GetComponent<Text>().text = piece.Label;
        //pieceTrns.GetChild(2).GetComponent<Text>().text = piece.Amount.ToString();

        pieceTrns.RotateAround(wheelPiecesParent.position, Vector3.back, pieceAngle * index);
    }

    private GameObject InstantiatePiece()
    {
        return Instantiate(wheelPiecePrefab, wheelPiecesParent.position, Quaternion.identity, wheelPiecesParent);
    }


    public void Spin()
    {
        if (!_isSpinning)
        {
            _isSpinning = true;

            BusSystem.CallSpinStart();

            int index = GetRandomPieceIndex();
            Reward piece = wheelPieceDatabase.rewardSets[currentZone].rewards[index];

            if (piece.Chance == 0 && nonZeroChancesIndices.Count != 0)
            {
                index = nonZeroChancesIndices[Random.Range(0, nonZeroChancesIndices.Count)];
                piece = piece = wheelPieceDatabase.rewardSets[currentZone].rewards[index];
            }

            float angle = -(pieceAngle * index);

            float rightOffset = (angle - halfPieceAngleWithPaddings) % 360;
            float leftOffset = (angle + halfPieceAngleWithPaddings) % 360;

            float randomAngle = Random.Range(leftOffset, rightOffset);

            Vector3 targetRotation = Vector3.back * (randomAngle + 2 * 360 * spinDuration);

            //float prevAngle = wheelCircle.eulerAngles.z + halfPieceAngle ;
            float prevAngle, currentAngle;
            prevAngle = currentAngle = wheelCircle.eulerAngles.z;

            bool isIndicatorOnTheLine = false;

            wheelCircle
            .DORotate(targetRotation, spinDuration, RotateMode.Fast)
            .SetEase(Ease.InOutQuart)
            .OnUpdate(() => {
                float diff = Mathf.Abs(prevAngle - currentAngle);
                if (diff >= halfPieceAngle)
                {
                    if (isIndicatorOnTheLine)
                    {
                        audioSource.PlayOneShot(audioSource.clip);
                    }
                    prevAngle = currentAngle;
                    isIndicatorOnTheLine = !isIndicatorOnTheLine;
                }
                currentAngle = wheelCircle.eulerAngles.z;
            })
            .OnComplete(() => {
                _isSpinning = false;

                BusSystem.CallSpinEnd(piece);
            });

        }
    }

    private int GetRandomPieceIndex()
    {
        double r = rand.NextDouble() * accumulatedWeight;

        for (int i = 0; i < wheelPieceDatabase.rewardSets[currentZone].rewards.Count; i++)
            if (wheelPieceDatabase.rewardSets[currentZone].rewards[i]._weight >= r)
                return i;

        return 0;
    }

    private void CalculateWeightsAndIndices()
    {
        for (int i = 0; i < wheelPieceDatabase.rewardSets[currentZone].rewards.Count; i++)
        {
            Reward piece = wheelPieceDatabase.rewardSets[currentZone].rewards[i];

            //add weights:
            accumulatedWeight += piece.Chance;
            piece._weight = accumulatedWeight;

            //add index :
            piece.Index = i;

            //save non zero chance indices:
            if (piece.Chance > 0)
                nonZeroChancesIndices.Add(i);
        }
    }

    private void OnValidate()
    {
        if (PickerWheelTransform != null)
            PickerWheelTransform.localScale = new Vector3(wheelSize, wheelSize, 1f);
    }
}