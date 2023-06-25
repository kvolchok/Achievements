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
    private WalletManager _walletManager;
    [SerializeField]
    private WalletView _walletView;
    [SerializeField]
    private ItemManager _itemManager;

    private void Awake()
    {
        _walletManager.Initialize(_walletView.AddScore);
        
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
                    () => _walletManager.AddGoldScore(achievement, achievementModel.RewardValue));
                break;
            }
            case AchievementType.Gem:
            {
                var achievement = Instantiate(_achievementPrefab, _achievementRoot);
                achievement.Initialize(achievementModel,
                    () => _walletManager.AddGemScore(achievement, achievementModel.RewardValue));
                break;
            }
            case AchievementType.Item:
            {
                var achievement = Instantiate(_achievementTypeItemPrefab, _achievementRoot);
                achievement.Initialize(achievementModel,
                    () => _itemManager.AddItem(achievementModel.RewardValue));
                break;
            }
        }
    }
}