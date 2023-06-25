using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchievementsSettings", menuName = "ScriptableObject/AchievementsSettings",
    order = 50)]
public class AchievementsSettings : ScriptableObject
{
    [SerializeField]
    private List<AchievementModel> _achievements = new();

    public List<AchievementModel> GetSettings()
    {
        return _achievements;
    }
}