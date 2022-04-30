using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

namespace UI.Elements
{
    
    public class UIOptions : Selectable, ISelectHandler, IDeselectHandler//, IMoveHandler
    {

        public delegate void ChangeValueHandler(int index, string value);
        public event ChangeValueHandler OnChangeValue;
        
        [SerializeField] private List<string> _values;
        [SerializeField] private bool _wrapAround;
        [Space]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        private int _index = -1;
        
        private SoundEffectsSystem _sfxSystem;

        [Inject]
        private void Construct(SoundEffectsSystem sfxSystem)
        {
            _sfxSystem = sfxSystem;
        }

        public void Construct(string name, int defaultValue, List<string> values)
        {
            _nameText.text = name;
            _values = values;
            _index = Mathf.Clamp(defaultValue, 0, _values.Count - 1);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _leftButton.onClick.AddListener(DecreaseValue);
            _rightButton.onClick.AddListener(IncreaseValue);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _leftButton.onClick.RemoveListener(DecreaseValue);
            _rightButton.onClick.RemoveListener(IncreaseValue);
        }

        protected override void Start()
        {
            base.Start();
            if (_index == -1) _index = 0;
            UpdateValue();
        }

        private void UpdateValue()
        {
            _valueText.text = _values[_index];
        }

        private void IncreaseValue()
        {
            _index = Mathf.Clamp(_index + 1, 0, _values.Count - 1);
            LeanTween.cancel(gameObject);
            LeanTween.value(gameObject, 0f, 15f, .5f)
            .setOnUpdate((value) => _valueText.GetComponent<RectTransform>().anchoredPosition = new Vector2(value, 0f))
            .setIgnoreTimeScale(true)
            .setEasePunch();
            UpdateValue();

            OnChangeValue?.Invoke(_index, _values[_index]);
            _sfxSystem.PlaySFX("ui_value");
        }

        private void DecreaseValue()
        {
            _index = Mathf.Clamp(_index - 1, 0, _values.Count - 1);
            LeanTween.cancel(gameObject);
            LeanTween.value(gameObject, 0f, -15f, .5f)
            .setOnUpdate((value) => _valueText.GetComponent<RectTransform>().anchoredPosition = new Vector2(value, 0f))
            .setIgnoreTimeScale(true)
            .setEasePunch();
            UpdateValue();

            OnChangeValue?.Invoke(_index, _values[_index]);
            _sfxSystem.PlaySFX("ui_value");
        }

        public override void OnMove(AxisEventData eventData)
        {
            base.OnMove(eventData);
            switch (eventData.moveDir)
            {
                case MoveDirection.Right:
                    _rightButton.onClick?.Invoke();
                    break;
                case MoveDirection.Left:
                    _leftButton.onClick?.Invoke();
                    break;
            }
        }
    }

}
