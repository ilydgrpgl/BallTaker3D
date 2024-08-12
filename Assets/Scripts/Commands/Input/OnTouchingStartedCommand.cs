using Signals;
using UnityEngine;

namespace Commands
{
    public class OnTouchingStartedCommand
    {
        private bool _isTouching;

        internal OnTouchingStartedCommand(bool isTouching)
        {
            _isTouching = isTouching;
        }

        internal void Execute()
        {
            if (_isTouching)
                InputSignals.Instance.onInputTaken?.Invoke();
        }
    }
}