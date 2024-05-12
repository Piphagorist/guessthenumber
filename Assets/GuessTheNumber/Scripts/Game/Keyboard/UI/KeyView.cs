using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GuessTheNumber.Scripts.Game.Keyboard.UI
{
    public class KeyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Button _button;
        
        public event Action<KeyView> OnClick;
        
        public string Value { get; private set; }

        private void Awake()
        {
            _button.onClick.AddListener(HandleButtonClick);
        }

        public void SetValue(string value)
        {
            _value.text = value;
            Value = value;
        }
        
        private void HandleButtonClick()
        {
            OnClick?.Invoke(this);
        }
    }
}