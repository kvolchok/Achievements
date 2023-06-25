using UnityEngine;

[CreateAssetMenu(fileName = "MoneyTypePrefab", menuName = "ScriptableObject/MoneyTypePrefab", order = 50)]
public class MoneyTypePrefab : ScriptableObject
{
    [field:SerializeField]
    public Transform Prefab { get; private set; }
    [field:SerializeField]
    public AchievementType Type { get; private set; }
}