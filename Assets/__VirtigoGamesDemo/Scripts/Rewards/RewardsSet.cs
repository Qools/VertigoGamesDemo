using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPrizeSet", menuName = "Prizes/NewPrizeSet", order = 1)]
public class RewardsSet : ScriptableObject
{
    [Header("Reward Set")]
    public List<Reward> rewards = new List<Reward>();

    public RewardSetType rewardSetType;
}


public enum RewardSetType
{
    common,
    epic,
    legendary
}
