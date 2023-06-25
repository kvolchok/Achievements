using UnityEngine;

[CreateAssetMenu(fileName = "ItemModel", menuName = "ScriptableObject/ItemModel", order = 50)]
public class ItemModel : ScriptableObject
{
    [field:SerializeField]
    public int Id { get; private set; }
    [field:SerializeField]
    public string Description { get; private set; }
    [field:SerializeField]
    public string Name { get; private set; }
    [field:SerializeField]
    public Sprite Icon { get; private set; }
}