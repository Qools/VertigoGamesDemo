using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRewardSetsDatabase", menuName = "Prizes/NewRewardSetsDatabase", order = 2)]
public class RewardSetsDatabase : ScriptableObject
{
    [Header("Reward Sets")]
    public List<RewardsSet> rewardSets = new List<RewardsSet>();
}
