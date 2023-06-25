using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private ItemModel[] _itemModels;

    [SerializeField]
    private ItemScreen _itemScreenPrefab;

    private ItemScreen _itemScreen;

    public void AddItem(int id)
    {
        var itemModel = _itemModels.First(model => model.Id == id);
        ShowScreen(itemModel);
    }

    private void ShowScreen(ItemModel itemModel)
    {
        _itemScreen = Instantiate(_itemScreenPrefab, transform);
        _itemScreen.Initialize(itemModel, CloseScreen);
    }

    private void CloseScreen()
    {
        Destroy(_itemScreen.gameObject);
    }
}