using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemScreen : MonoBehaviour
{
    private Action _closeScreenEvent;
    
    private static readonly int IsActive = Animator.StringToHash("Is Active");
    
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private TextMeshProUGUI _name;
    [SerializeField]
    private Image _icon;

    private Animator _animator;
    private bool _isActive;

    public void Initialize(ItemModel itemModel, Action closeScreenEvent)
    {
        _description.text = itemModel.Description;
        _name.text = itemModel.Name;
        _icon.sprite = itemModel.Icon;

        _closeScreenEvent = closeScreenEvent;
        _animator = GetComponentInChildren<Animator>();
        
        ChangeState(_isActive);
    }

    [UsedImplicitly]
    public void CloseScreen()
    {
        _closeScreenEvent?.Invoke();
    }

    private void ChangeState(bool newState)
    {
        _isActive = !newState;
        _animator.SetBool(IsActive, _isActive);
    }
}