using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectedRewards : MonoBehaviour
{
    public GameObject container;

    public GameObject rewardTabPrefab;

    public List<Reward> collectedRewards = new List<Reward>();

    private Reward currentReward;

    // Start is called before the first frame update
    void Start()
    {
        ClearRewards();
    }

    private void OnEnable()
    {
        BusSystem.OnSpinEnd += OnSpinEnd;
        BusSystem.OnPopUpCollectButtonClicked += CreateRewardTab; 
        BusSystem.OnGameOver += ClearRewards;
    }

    private void OnDisable()
    {
        BusSystem.OnSpinEnd -= OnSpinEnd;
        BusSystem.OnPopUpCollectButtonClicked += CreateRewardTab;
        BusSystem.OnGameOver += ClearRewards;
    }

    private void OnSpinEnd(Reward _reward)
    {
        collectedRewards.Add(_reward);
        currentReward = _reward;
    }

    private void CreateRewardTab()
    {
        //foreach (var collectedReward in collectedRewards)
        //{
        //    if(collectedReward.rewardType = _reward.rewardType)
        //    {
        //        
        //    }
        //}

        GameObject newRewardTab = Instantiate(rewardTabPrefab, container.transform.position, Quaternion.identity, container.transform);
        newRewardTab.transform.GetChild(0).GetComponent<Image>().sprite = currentReward.Icon;
        newRewardTab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentReward.Amount.ToString();
    }

    private void ClearRewards()
    {
        var allChildren = container.GetComponentsInChildren<Transform>().Where(t => t != container.transform);
        foreach (var child in allChildren)
        {
            Destroy(child.gameObject);
        }

        collectedRewards.Clear();
        collectedRewards = new List<Reward>();
    }
}
