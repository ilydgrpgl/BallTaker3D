using Signals;

namespace Commands
{
    public class OnTouchingFinishedCommand
    {
        private bool _isTouching;

        internal OnTouchingFinishedCommand(bool isTouching)
        {
            _isTouching = isTouching;
        }

        internal void Execute()
        {
            if (!_isTouching)
                InputSignals.Instance.onInputReleased?.Invoke();
        }
    }
}