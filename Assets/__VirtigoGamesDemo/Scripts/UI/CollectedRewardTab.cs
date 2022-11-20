using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectedRewardTab : MonoBehaviour
{
    public CollectedReward collectedReward;

    public void FillRewardTabValues(CollectedReward _collectedReward)
    {
        collectedReward = _collectedReward;

        transform.GetChild(0).GetComponent<Image>().sprite = collectedReward.Icon;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = collectedReward.Amount.ToString();
    }

    public void UpdateRewardTabValues(int _value)
    {
        collectedReward.Amount += _value;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = collectedReward.Amount.ToString();
    }
}



[System.Serializable]
public class CollectedReward
{
    public RewardType rewardType;
    public Sprite Icon;
    public int Amount;
}