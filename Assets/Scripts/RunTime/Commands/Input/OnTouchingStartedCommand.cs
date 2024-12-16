using Signals;
using UnityEngine;

namespace RunTime.Commands.Input
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
            if (!_isTouching)
            {
                _isTouching = true;
                InputSignals.Instance.onInputTaken?.Invoke();
            }
        }
    }
}