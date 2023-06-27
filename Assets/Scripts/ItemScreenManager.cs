using System.Linq;
using UnityEngine;

public class ItemScreenManager : MonoBehaviour
{
    [SerializeField]
    private ItemModel[] _itemModels;

    [SerializeField]
    private ItemScreen _itemScreenPrefab;

    public void AddItem(int id)
    {
        var itemModel = _itemModels.First(model => model.Id == id);
        ShowScreen(itemModel);
    }

    private void ShowScreen(ItemModel itemModel)
    {
        var itemScreen = Instantiate(_itemScreenPrefab, transform);
        itemScreen.Initialize(itemModel);
    }
}