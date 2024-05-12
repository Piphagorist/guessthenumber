using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GuessTheNumber.Scripts.Game.Keyboard.UI
{
    public class KeyboardView : MonoBehaviour
    {
        [SerializeField] private Transform _keysContainer;
        [SerializeField] private KeyView _keyViewPrefab;
        [SerializeField] private Button _backspaceButton;

        public event Action<string> OnKeyClick;
        public event Action OnBackspaceClick; 

        private List<KeyView> _keyViews = new();
        private bool _locked;

        private void Awake()
        {
            _backspaceButton.onClick.AddListener(HandleBackspaceClick);
        }

        private void HandleBackspaceClick()
        {
            OnBackspaceClick?.Invoke();
        }

        public void SetKeys(string[] keys)
        {
            int i = 0;
            for (; i < keys.Length; i++)
            {
                string key = keys[i];

                if (_keyViews.Count == i)
                {
                    KeyView newKey = Instantiate(_keyViewPrefab, _keysContainer);
                    newKey.OnClick += HandleKeyClick;
                    _keyViews.Add(newKey);
                }
                
                _keyViews[i].SetValue(key);
            }
            
            for (; i < _keyViews.Count; i++)
            {
                _keyViews[i].gameObject.SetActive(false);
            }
        }

        private void HandleKeyClick(KeyView key)
        {
            if (_locked) return;
            
            OnKeyClick(key.Value);
        }

        public void SetLock(bool locked)
        {
            _locked = locked;
        }
    }
}