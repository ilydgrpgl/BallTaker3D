using System;
using RunTime.Keys;
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
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
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