using UnityEngine ;

[CreateAssetMenu(fileName = "NewPrize", menuName = "Prizes/NewPrize", order = 0)]
public class WheelPiece : ScriptableObject
{
    public Sprite Icon;
    public string Label;
    public bool isBomb;
    public bool isEpicReward;

    [Tooltip("Reward amount")] public int Amount;

    [Tooltip("Probability in %")]
    [Range(0f, 100f)]
    public float Chance = 100f;

    [HideInInspector] public int Index;
    [HideInInspector] public double _weight = 0f;
}
