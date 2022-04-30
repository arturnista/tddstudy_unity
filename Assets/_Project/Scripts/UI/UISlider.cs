using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    
    public class UISlider : Selectable
    {

        public delegate void ChangeValueHandler(float value);
        public event ChangeValueHandler OnChangeValue;
        
        [Space]
        [SerializeField] private float _manualIncrease = 1f;
        [Space]
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Slider _slider;

        private Func<float, string> _parseFunction;

        protected override void OnEnable()
        {
            base.OnEnable();
            _slider.onValueChanged.AddListener(HandleChangeValue);

            _parseFunction = v => v.ToString();
            HandleChangeValue(_slider.value);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _slider.onValueChanged.RemoveListener(HandleChangeValue);
        }

        private void HandleChangeValue(float value)
        {
            OnChangeValue?.Invoke(value);
            _valueText.text = _parseFunction?.Invoke(value);
        }

        public void Construct(string name, float defaultValue, Func<float, string> parseFunction = null)
        {
            _nameText.text = name;
            _slider.value = defaultValue;
            if (parseFunction != null) _parseFunction = parseFunction;
            HandleChangeValue(_slider.value);
        }

        public override void OnMove(AxisEventData eventData)
        {
            base.OnMove(eventData);
            switch (eventData.moveDir)
            {
                case MoveDirection.Right:
                    _slider.value = _slider.value + _manualIncrease;
                    break;
                case MoveDirection.Left:
                    _slider.value = _slider.value - _manualIncrease;
                    break;
            }
        }
    }

}
