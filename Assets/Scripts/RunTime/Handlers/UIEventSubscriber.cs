using System;
using RunTime.Enums;
using RunTime.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RunTime.Handlers
{
    public class UIEventSubscriber : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private UIEventSubscriptionTypes type;
        [SerializeField] private Button button;
         private UIManager _manager;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void Awake() => GetReferences();

        private void GetReferences()
        {
            _manager = FindObjectOfType<UIManager>();
        }

        private void OnEnable() => SubscribeEvents();


        private void SubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                    button.onClick.AddListener(_manager.Play);
                    break;
                case UIEventSubscriptionTypes.OnNextLevel:
                    button.onClick.AddListener(_manager.NextLevel);
                    break;
                case UIEventSubscriptionTypes.OnRestartLevel:
                    button.onClick.AddListener(_manager.RestartLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UnSubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                    button.onClick.RemoveListener(_manager.Play);
                    break;
                case UIEventSubscriptionTypes.OnNextLevel:
                    button.onClick.RemoveListener(_manager.NextLevel);
                    break;
                case UIEventSubscriptionTypes.OnRestartLevel:
                    button.onClick.RemoveListener(_manager.RestartLevel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable() => UnSubscribeEvents();
    }
}