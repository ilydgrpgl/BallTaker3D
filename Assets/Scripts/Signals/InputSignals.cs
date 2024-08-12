using System;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoBehaviour
    {
        #region Singletion

        public static InputSignals Instance;

        private void Awake()
        {
            if (Instance == null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = null;
        }

        #endregion

        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction onInputTaken = delegate { };
        public UnityAction onInputReleased = delegate { };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
        public UnityAction onEnableInput = delegate { };
        public UnityAction onDisableInput = delegate { };
    }
}