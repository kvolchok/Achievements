using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemScreen : MonoBehaviour
{
    private static readonly int IsActive = Animator.StringToHash("Is Active");
    
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private Image _icon;

    private Animator _animator;
    private bool _isActive;

    public void Initialize(ItemModel itemModel)
    {
        _description.text = itemModel.Description;
        _name.text = itemModel.Name;
        _icon.sprite = itemModel.Icon;
        
        _animator = GetComponentInChildren<Animator>();
        
        ChangeState(_isActive);
    }

    [UsedImplicitly]
    public void CloseScreen()
    {
        Destroy(gameObject);
    }

    private void ChangeState(bool newState)
    {
        _isActive = !newState;
        _animator.SetBool(IsActive, _isActive);
    }
}