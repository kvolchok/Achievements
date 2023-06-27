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
        foreach (var model in settings)
        {
            CreateAchievement(model);
        }
    }

    private void CreateAchievement(AchievementModel model)
    {
        switch (model.Type)
        {
            case AchievementType.Gold:
            {
                var achievement = Instantiate(_achievementPrefab, _achievementRoot);
                achievement.Initialize(model,
                    () => _wallet.AddGoldScore(achievement, model.RewardValue));
                break;
            }
            case AchievementType.Gem:
            {
                var achievement = Instantiate(_achievementPrefab, _achievementRoot);
                achievement.Initialize(model,
                    () => _wallet.AddGemScore(achievement, model.RewardValue));
                break;
            }
            case AchievementType.Item:
            {
                var achievement = Instantiate(_achievementTypeItemPrefab, _achievementRoot);
                achievement.Initialize(model,
                    () => _itemScreenManager.AddItem(model.RewardValue));
                break;
            }
        }
    }
}