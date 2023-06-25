using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _goldValueLabel;
    [SerializeField]
    private TextMeshProUGUI _gemValueLabel;
    
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

    public void SetStartScore(int goldValue, int gemValue)
    {
        SetScore(_goldValueLabel, goldValue);
        SetScore(_gemValueLabel, gemValue);
    }

    public void AddScore(AchievementView achievement, int startMoneyScore, int delta)
    {
        switch (achievement.Type)
        {
            case AchievementType.Gold:
                var moneyPrefab = _moneyTypePrefabs.First(moneyPrefab => moneyPrefab.Type == AchievementType.Gold);
                StartCoroutine(ShowItemMovementAnimation(moneyPrefab,
                    achievement.RewardRoot.position, _goldViewRoot.position));
                StartCoroutine(ShowMoneyCalculationAnimation(_goldValueLabel, startMoneyScore, delta));
                break;
            case AchievementType.Gem:
                moneyPrefab = _moneyTypePrefabs.First(moneyPrefab => moneyPrefab.Type == AchievementType.Gem);
                StartCoroutine(ShowItemMovementAnimation(moneyPrefab,
                    achievement.RewardRoot.position, _gemViewRoot.position));
                StartCoroutine(ShowMoneyCalculationAnimation(_gemValueLabel, startMoneyScore, delta));
                break;
        }
    }

    private void SetScore(TextMeshProUGUI moneyValueLabel, int moneyScore)
    {
        moneyValueLabel.text = moneyScore.ToString();
    }

    private IEnumerator ShowItemMovementAnimation(MoneyTypePrefab moneyTypePrefab, Vector3 fromPosition, Vector3 toPosition)
    {
        for (var i = 0; i < _itemsCount; i++)
        {
            var item = Instantiate(moneyTypePrefab.Prefab, _canvasRoot);
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

    private IEnumerator ShowMoneyCalculationAnimation(TextMeshProUGUI moneyValueLabel, int startMoneyScore, int delta)
    {
        var newMoneyScore = startMoneyScore + delta;
        var currentTime = 0f;

        while (currentTime <= _moneyCalculationTime)
        {
            var progress = currentTime / _moneyCalculationTime;
            var currentScore = (int)Mathf.Lerp(startMoneyScore, newMoneyScore, progress);
            currentTime += Time.deltaTime;
            SetScore(moneyValueLabel, currentScore);

            yield return null;
        }
        
        SetScore(moneyValueLabel, newMoneyScore);
    }
}