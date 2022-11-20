using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectedRewards : MonoBehaviour
{
    [Header("Collected Reward Elements")]
    public GameObject container;
    public GameObject rewardTabPrefab;

    private List<CollectedRewardTab> collectedRewardTabs = new List<CollectedRewardTab>();
    private CollectedReward currentCollectedReward;

    // Start is called before the first frame update
    void Start()
    {
        ClearRewards();
    }

    private void OnEnable()
    {
        BusSystem.OnSpinEnd += OnSpinEnd;
        BusSystem.OnPopUpCollectButtonClicked += CheckRewardTab; 
        BusSystem.OnGameOver += ClearRewards;
    }

    private void OnDisable()
    {
        BusSystem.OnSpinEnd -= OnSpinEnd;
        BusSystem.OnPopUpCollectButtonClicked += CheckRewardTab;
        BusSystem.OnGameOver += ClearRewards;
    }

    private void OnSpinEnd(Reward _reward)
    {
        currentCollectedReward = new CollectedReward();
        currentCollectedReward.rewardType = _reward.rewardType;
        currentCollectedReward.Icon = _reward.Icon;
        currentCollectedReward.Amount = _reward.Amount;
    }

    private void CheckRewardTab()
    {
        if (collectedRewardTabs.Count == 0)
        {
            CreateNewTab();
            return;
        }

        for (int i = 0; i < collectedRewardTabs.Count; i++)
        {
            if (collectedRewardTabs[i].collectedReward.rewardType == currentCollectedReward.rewardType)
            {
                collectedRewardTabs[i].UpdateRewardTabValues(currentCollectedReward.Amount);
                break;
            }

            if (i == (collectedRewardTabs.Count - 1))
            {
                CreateNewTab();
                break;
            }
        }
    }

    private void CreateNewTab()
    {
        GameObject newRewardTab = Instantiate(rewardTabPrefab, container.transform.position, Quaternion.identity, container.transform);
        if (newRewardTab.TryGetComponent(out CollectedRewardTab _collectedRewardTab))
        {
            _collectedRewardTab.FillRewardTabValues(currentCollectedReward);

            collectedRewardTabs.Add(_collectedRewardTab);
        }
    }

    private void ClearRewards()
    {
        var allChildren = container.GetComponentsInChildren<Transform>().Where(t => t != container.transform);
        foreach (var child in allChildren)
        {
            Destroy(child.gameObject);
        }

        collectedRewardTabs.Clear();
        collectedRewardTabs = new List<CollectedRewardTab>();
    }
}