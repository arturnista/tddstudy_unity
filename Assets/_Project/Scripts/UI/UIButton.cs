using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, ISubmitHandler
{

    [SerializeField] private Vector2 _childOffset;
    [Space]
    [SerializeField] private string _clickAudio = "ui_click";

    private SoundEffectsSystem _sfxSystem;
    
    private Button _button;
    private Transform _child;
    private Vector2 _originalLocalPosition;
    
    [Inject]
    private void Construct(SoundEffectsSystem sfxSystem)
    {
        _sfxSystem = sfxSystem;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _child = transform.GetChild(0);
        _originalLocalPosition = _child.localPosition;
    }
    
    private void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonClick);        
    }

    private void HandleButtonClick()
    {
        _sfxSystem.PlaySFX(_clickAudio);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HandleDownEffect();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        HandleDownEffect();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandleDownEffect();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        HandleUpEffect();
    }

    private void HandleDownEffect()
    {
        _child.localPosition = _originalLocalPosition + _childOffset;
    }

    private void HandleUpEffect()
    {
        _child.localPosition = _originalLocalPosition;
    }
}
