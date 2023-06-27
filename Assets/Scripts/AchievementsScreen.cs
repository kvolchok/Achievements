using UnityEngine;

public class AchievementsScreen : MonoBehaviour
{
    [SerializeField]
    private AchievementsSettings _settings;
    [SerializeField]
    private AchievementView _achievementPrefab;
    [SerializeField]
    private AchievementView _achievementTypeItemPrefab;
    [SerializeField]
    private RectTransform _achievementRoot;

    [SerializeField]
    private WalletView _walletView;
    [SerializeField]
    private Wallet _wallet;
    [SerializeField]
    private ItemScreenManager _itemScreenManager;

    private void Awake()
    {
        _walletView.Initialize(_wallet);
        
        var settings = _settings.GetSettings();
        foreach (var achievementModel in settings)
        {
            CreateAchievement(achievementModel);
        }
    }

    private void CreateAchievement(AchievementModel achievementModel)
    {
        switch (achievementModel.Type)
        {
            case AchievementType.Gold:
            {
                var achievement = Instantiate(_achievementPrefab, _achievementRoot);
                achievement.Initialize(achievementModel,
                    () => _wallet.AddGoldScore(achievement, achievementModel.RewardValue));
                break;
            }
            case AchievementType.Gem:
            {
                var achievement = Instantiate(_achievementPrefab, _achievementRoot);
                achievement.Initialize(achievementModel,
                    () => _wallet.AddGemScore(achievement, achievementModel.RewardValue));
                break;
            }
            case AchievementType.Item:
            {
                var achievement = Instantiate(_achievementTypeItemPrefab, _achievementRoot);
                achievement.Initialize(achievementModel,
                    () => _itemScreenManager.AddItem(achievementModel.RewardValue));
                break;
            }
        }
    }
}