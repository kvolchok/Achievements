using UnityEngine;

[CreateAssetMenu(fileName = "AchievementModel", menuName = "ScriptableObject/AchievementModel", order = 50)]
public class AchievementModel : ScriptableObject
{
    [field:SerializeField]
    public AchievementType Type { get; private set; }
    [field:SerializeField]
    public string Name { get; private set; }
    [field:SerializeField]
    public string Description { get; private set; }
    [field:SerializeField]
    public Sprite Icon { get; private set; }
    [field:SerializeField]
    public Sprite Reward { get; private set; }
    [field:SerializeField]
    public int RewardValue { get; private set; }
}