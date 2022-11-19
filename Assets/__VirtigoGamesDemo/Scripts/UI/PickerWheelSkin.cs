using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickerWheelSkin : MonoBehaviour
{
    public WheelSkinsDatabase wheelSkinsDatabase;

    [Header("Wheel Images")]
    public Image wheelImage;
    public Image indicatorImage;

    private void Start()
    {
        StartCoroutine(Co_GetController());
    }

    private IEnumerator Co_GetController()
    {
        yield return new WaitWhile(() => WheelPickerController.Instance == null);

        ChangeWheelSkin(0);
    }

    private void OnEnable()
    {
        BusSystem.OnPopUpCollectButtonClicked += CheckWheelSkin;
        BusSystem.OnGameOver += ResetWheelSkin;
    }

    private void OnDisable()
    {
        BusSystem.OnPopUpCollectButtonClicked -= CheckWheelSkin;
        BusSystem.OnGameOver -= ResetWheelSkin;
    }

    private void CheckWheelSkin()
    {

        if (WheelPickerController.Instance.CheckZoneCondition(5) == 0)
        {
            ChangeWheelSkin(1);
        }

        else if(WheelPickerController.Instance.CheckZoneCondition(30) == 0)
        {
            ChangeWheelSkin(2);
        }

        else
        {
            ChangeWheelSkin(0);
        }
    } 

    private void ChangeWheelSkin(int _index)
    {
        wheelImage.sprite = wheelSkinsDatabase.wheelSkins[_index].wheelSprite;
        indicatorImage.sprite = wheelSkinsDatabase.wheelSkins[_index].indicatorSprite;
    }

    private void ResetWheelSkin()
    {
        wheelImage.sprite = wheelSkinsDatabase.wheelSkins[0].wheelSprite;
        indicatorImage.sprite = wheelSkinsDatabase.wheelSkins[0].indicatorSprite;
    }
}
