using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _goldScoreLabel;
    [SerializeField]
    private TextMeshProUGUI _gemScoreLabel;
    
    [SerializeField]
    private MoneyTypePrefab[] _moneyTypePrefabs;
    
    [SerializeField]
    private Transform _canvasRoot;
    [SerializeField]
    private Transform _goldViewRoot;
    [SerializeField]
    private Transform _gemViewRoot;

    [SerializeField]
    private int _itemsCount;
    [SerializeField]
    private float _itemSpeed;
    [SerializeField]
    private float _itemSpawnDelay;
    [SerializeField]
    private float _moneyCalculationTime;
    
    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        
        SetScore(_goldScoreLabel, _wallet.GoldScore);
        SetScore(_gemScoreLabel, _wallet.GemScore);
        
        _wallet.OnScoreChanged += OnScoreChanged;
    }
    
    private void SetScore(TextMeshProUGUI scoreLabel, int score)
    {
        scoreLabel.text = score.ToString();
    }

    private void OnScoreChanged(AchievementView achievement, int oldScore, int newScore)
    {
        switch (achievement.Type)
        {
            case AchievementType.Gold:
                var moneyPrefab = _moneyTypePrefabs.First(moneyPrefab => moneyPrefab.Type == AchievementType.Gold);
                StartCoroutine(ShowItemMovementAnimation(moneyPrefab,
                    achievement.RewardRoot.position, _goldViewRoot.position));
                StartCoroutine(ShowMoneyCalculationAnimation(_goldScoreLabel, oldScore, newScore));
                break;
            case AchievementType.Gem:
                moneyPrefab = _moneyTypePrefabs.First(moneyPrefab => moneyPrefab.Type == AchievementType.Gem);
                StartCoroutine(ShowItemMovementAnimation(moneyPrefab,
                    achievement.RewardRoot.position, _gemViewRoot.position));
                StartCoroutine(ShowMoneyCalculationAnimation(_gemScoreLabel, oldScore, newScore));
                break;
        }
    }

    private IEnumerator ShowItemMovementAnimation(MoneyTypePrefab moneyPrefab, Vector3 fromPosition, Vector3 toPosition)
    {
        for (var i = 0; i < _itemsCount; i++)
        {
            var item = Instantiate(moneyPrefab.Prefab, _canvasRoot);
            item.transform.position = fromPosition;
            
            StartCoroutine(MoveItemTo(item, toPosition));
            yield return new WaitForSeconds(_itemSpawnDelay);
        }
    }
    
    private IEnumerator MoveItemTo(Transform itemPrefab, Vector3 toPosition)
    {
        var distance = Vector3.Distance(itemPrefab.position, toPosition);
        var travelDistance = 0f;

        while (travelDistance <= distance)
        {
            travelDistance = _itemSpeed * Time.deltaTime;
            var currentPosition = Vector3.MoveTowards(itemPrefab.position, toPosition, travelDistance);
            itemPrefab.position = currentPosition;
            distance -= travelDistance;

            yield return null;
        }

        itemPrefab.position = toPosition;
        Destroy(itemPrefab.gameObject);
    }

    private IEnumerator ShowMoneyCalculationAnimation(TextMeshProUGUI scoreLabel, int oldScore, int newScore)
    {
        var currentTime = 0f;

        while (currentTime <= _moneyCalculationTime)
        {
            var progress = currentTime / _moneyCalculationTime;
            var currentScore = (int)Mathf.Lerp(oldScore, newScore, progress);
            currentTime += Time.deltaTime;
            SetScore(scoreLabel, currentScore);

            yield return null;
        }
        
        SetScore(scoreLabel, newScore);
    }

    private void OnDestroy()
    {
        _wallet.OnScoreChanged -= OnScoreChanged;
    }
}