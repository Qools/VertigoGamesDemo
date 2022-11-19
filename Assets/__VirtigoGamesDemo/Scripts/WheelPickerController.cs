using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPickerController : MonoBehaviour
{
    public static WheelPickerController Instance { get; private set; }

    public List<Reward> takenRewards = new List<Reward>();

    public int currentZone;

    private void Awake()
    {
        if (!(Instance is null) && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentZone = 1;
    }

    private void OnEnable()
    {
        BusSystem.OnSpinEnd += CheckSpinResult;
    }

    private void OnDisable()
    {
        BusSystem.OnSpinEnd -= CheckSpinResult;
    }

    public void CheckSpinResult(Reward _reward)
    {
        if (_reward.isBomb)
        {
            GameOver();
            return;
        }

        AddReward(_reward);
        currentZone++;

        BusSystem.CallZoneEnded();
    }

    private void GameOver()
    {
        currentZone = 1;

        takenRewards.Clear();
        takenRewards = new List<Reward>();
    }

    public int CheckZoneCondition(int condition)
    {
        return currentZone % condition;
    } 

    public void AddReward(Reward _reward)
    {
        takenRewards.Add(_reward);
    }
}
