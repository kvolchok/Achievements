using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<AchievementView, int, int> OnScoreChanged;

    [field:SerializeField]
    public int GoldScore { get; private set; }
    [field:SerializeField]
    public int GemScore { get; private set; }

    public void AddGoldScore(AchievementView achievement, int delta)
    {
        var oldScore = GoldScore;
        GoldScore = oldScore + delta;
        
        OnScoreChanged?.Invoke(achievement, oldScore, GoldScore);
    }
    
    public void AddGemScore(AchievementView achievement, int delta)
    {
        var oldScore = GemScore;
        GemScore = oldScore + delta;
        
        OnScoreChanged?.Invoke(achievement, oldScore, GemScore);
    }
}