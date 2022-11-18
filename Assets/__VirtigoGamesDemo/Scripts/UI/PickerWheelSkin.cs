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
        ChangeWheelSkin(0);
    }

    private void OnEnable()
    {
        BusSystem.OnPopUpScreenOpened += CheckWheelSkin;
    }

    private void OnDisable()
    {
        BusSystem.OnPopUpScreenOpened -= CheckWheelSkin;
    }

    private void CheckWheelSkin(Reward _reward)
    {
        int mod5 = WheelPickerController.Instance.currentZone % 5;
        int mod30 = WheelPickerController.Instance.currentZone % 30;

        Debug.Log("mod 5" + mod5);
        Debug.Log("mod 30 " + mod30);

        if (mod5 == 0)
        {
            ChangeWheelSkin(1);
        }

        else if(mod30 == 0)
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
}
