using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TopBar : MonoBehaviour
{
    [Header("Top Bar Elements")]
    [SerializeField] private Image topBarBackgroundImage;
    [SerializeField] private TextMeshProUGUI topBarText;

    [SerializeField] private TopBarColors topBarColors;

    private void Start()
    {
        StartCoroutine(Co_GetController());
    }

    private IEnumerator Co_GetController()
    {
        yield return new WaitWhile(()=> WheelPickerController.Instance == null);

        CheckGameZone();
        ChangeTopBarText();
    }

    private void OnEnable()
    {
        BusSystem.OnGameOver += ResetTopBarValues;
        BusSystem.OnZoneEnded += CheckGameZone;
        BusSystem.OnZoneEnded += ChangeTopBarText;
    }

    private void OnDisable()
    {
        BusSystem.OnGameOver -= ResetTopBarValues;
        BusSystem.OnZoneEnded -= CheckGameZone;
        BusSystem.OnZoneEnded -= ChangeTopBarText;
    }

    private void CheckGameZone()
    {
        if (WheelPickerController.Instance.CheckZoneCondition(5) == 0)
        {
            ChangeTopBarSprite(1);
        }

        else if (WheelPickerController.Instance.CheckZoneCondition(30) == 0)
        {
            ChangeTopBarSprite(2);
        }

        else
        {
            ChangeTopBarSprite(0);
        }
    }

    private void ChangeTopBarSprite(int _index)
    {
        topBarBackgroundImage.sprite = topBarColors.topBarSprites[_index];
    }

    private void ChangeTopBarText()
    {
        topBarText.text = WheelPickerController.Instance.currentZone.ToString();
    }

    private void ResetTopBarValues()
    {
        topBarText.text = WheelPickerController.Instance.currentZone.ToString();
        topBarBackgroundImage.sprite = topBarColors.topBarSprites[0];
    }
}
