using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopUpScreen : MonoBehaviour
{
    [SerializeField] private GameObject container;

    [SerializeField] private float openingDuration;
    [SerializeField] private float openingDelay;

    [Header("Pop Up Screen Elements")]
    [SerializeField] private Image rewardIcon;
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private Image amountTextIcon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private GameObject collectButton;
    [SerializeField] private GameObject retryButton;

    // Start is called before the first frame update
    void Start()
    {
        EnablePopUpScreen(false, 0.01f);

        collectButton.SetActive(false);
        retryButton.SetActive(false);
    }

    private void OnEnable()
    {
        BusSystem.OnSpinStart += OnSpinStart;
        BusSystem.OnSpinEnd += OnSpinEnd;
    }

    private void OnDisable()
    {
        BusSystem.OnSpinStart -= OnSpinStart;
        BusSystem.OnSpinEnd -= OnSpinEnd;
    }

    private void OnSpinEnd(Reward _reward)
    {
        rewardIcon.sprite = _reward.Icon;
        labelText.text = _reward.Label;

        amountText.text = _reward.Amount.ToString();

        collectButton.SetActive(!(_reward.isBomb));
        retryButton.SetActive(_reward.isBomb);

        DOVirtual.DelayedCall(openingDelay, () => EnablePopUpScreen(true, openingDuration));
    }

    private void OnSpinStart()
    {
        EnablePopUpScreen(false, 0.01f);
    }

    private void EnablePopUpScreen(bool isEnable, float _duration)
    {
        Vector3 endScale = Vector3.one;

        if (!isEnable)
        {
            endScale = Vector3.zero;
        }

        if (!(container is null))
        {
            container.transform.DOScale(endScale, _duration);
        }
    }

    private void OnValidate()
    {
        if (!(collectButton is null))
        {
            if (collectButton.TryGetComponent(out Button _collectButton))
            {
                _collectButton.onClick.AddListener(() =>
                {
                    BusSystem.CallPopUpCollectButtonClicked();
                    EnablePopUpScreen(false, 0.01f);
                });
            }
        }


        if (!(retryButton is null))
        {
            if (retryButton.TryGetComponent(out Button _retryButton))
            {
                _retryButton.onClick.AddListener(() =>
                { 
                    BusSystem.CallGameOver();
                    EnablePopUpScreen(false, 0.01f);
                });
            }
        }
    }
}
