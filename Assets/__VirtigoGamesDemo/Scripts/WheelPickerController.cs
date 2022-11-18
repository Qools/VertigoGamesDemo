using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPickerController : MonoBehaviour
{
    public static WheelPickerController Instance { get; private set; }

    public List<Reward> takenRewards = new List<Reward>();

    public int currentZone = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
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
        currentZone = Utility.GetIntValue(PlayerPrefKeys.currentZone);
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
    }

    private void GameOver()
    {
        currentZone = 0;
        Utility.SetIntValue(PlayerPrefKeys.currentZone, 0);

        takenRewards.Clear();
        takenRewards = new List<Reward>();
    }

    public void AddReward(Reward _reward)
    {
        takenRewards.Add(_reward);
    }
}
