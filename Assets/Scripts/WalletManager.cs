using System;
using UnityEngine;
using UnityEngine.Events;

public class WalletManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<int, int> _setStartScoreEvent;
    private Action<AchievementView, int, int> _increaseScoreEvent;

    [SerializeField]
    private int _goldScore;
    [SerializeField]
    private int _gemScore;

    public void Initialize(Action<AchievementView, int, int> increaseScoreEvent)
    {
        _increaseScoreEvent = increaseScoreEvent;
        
        _setStartScoreEvent?.Invoke(_goldScore, _gemScore);
    }
    
    public void AddGoldScore(AchievementView achievement, int value)
    {
        _increaseScoreEvent?.Invoke(achievement, _goldScore, value);
        _goldScore += value;
    }
    public void AddGemScore(AchievementView achievement, int value)
    {
        _increaseScoreEvent?.Invoke(achievement, _gemScore, value);
        _gemScore += value;
    }
}