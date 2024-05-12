using System;
using UnityEngine;

namespace GuessTheNumber.Scripts.Patterns.DI
{
    public class InjectedMonoBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            GlobalContainer.Instance.InjectAt(this);
            Init();
        }

        protected virtual void Init() {}
    }
}