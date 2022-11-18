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

    // Start is called before the first frame update
    void Start()
    {
        ClearRewards();
    }

    private void OnEnable()
    {
        BusSystem.OnPopUpScreenOpened += OnPopScreenOpened;
    }

    private void OnDisable()
    {
        BusSystem.OnPopUpScreenOpened -= OnPopScreenOpened;
    }

    private void OnPopScreenOpened(Reward _reward)
    {
        if (_reward.isBomb)
        {
            ClearRewards();
            return;
        }

        collectedRewards.Add(_reward);

        CreateRewardTab(_reward);
    }

    private void CreateRewardTab(Reward _reward)
    {
        //foreach (var collectedReward in collectedRewards)
        //{
        //    if(collectedReward.rewardType = _reward.rewardType)
        //    {
        //        
        //    }
        //}

        GameObject newRewardTab = Instantiate(rewardTabPrefab, container.transform.position, Quaternion.identity, container.transform);
        newRewardTab.transform.GetChild(0).GetComponent<Image>().sprite = _reward.Icon;
        newRewardTab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _reward.Amount.ToString();
    }

    private void ClearRewards()
    {
        var allChildren = container.GetComponentsInChildren<Transform>().Where(t => t != container.transform);
        foreach (var child in allChildren)
        {
            //Destroy(child);
        }

        collectedRewards.Clear();
        collectedRewards = new List<Reward>();
    }
}
