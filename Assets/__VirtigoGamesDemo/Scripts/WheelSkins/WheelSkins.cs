using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWheelSkin", menuName = "WheelSkin/NewWheelSkin", order = 0)]
public class WheelSkins : ScriptableObject
{
    [Header("Sprites")]
    public Sprite wheelSprite;
    public Sprite indicatorSprite;

    [Header("Reward Type")]
    public RewardType rewardType;
}