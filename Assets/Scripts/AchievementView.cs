using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementView : MonoBehaviour
{
    private Action _onRewardClaimed;

    private static readonly int IsClaimed = Animator.StringToHash("Is Claimed");

    public AchievementType Type { get; private set; }
    public Transform RewardRoot => _reward.transform;

    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Image _reward;
    [SerializeField]
    private TextMeshProUGUI _rewardValue;

    private Animator _animator;
    private bool _isClaimed;

    public void Initialize(AchievementModel achievementModel, Action onRewardClaimed)
    {
        _name.text = achievementModel.Name;
        _description.text = achievementModel.Description;
        _icon.sprite = achievementModel.Icon;
        _reward.sprite = achievementModel.Reward;
        _rewardValue.text = achievementModel.RewardValue.ToString();
        
        Type = achievementModel.Type;
        _onRewardClaimed = onRewardClaimed;

        _animator = GetComponent<Animator>();
    }

    [UsedImplicitly]
    public void Claim()
    {
        ChangeState(_isClaimed);
        _onRewardClaimed?.Invoke();
    }

    private void ChangeState(bool newState)
    {
        _isClaimed = !newState;
        _animator.SetBool(IsClaimed, _isClaimed);
    }
}