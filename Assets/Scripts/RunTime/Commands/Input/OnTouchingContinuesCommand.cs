using RunTime.Data.ValueObjects;
using RunTime.Keys;
using Signals;
using Unity.Mathematics;
using UnityEngine;

namespace RunTime.Commands.Input
{
    public class OnTouchingContinuesCommand
    {
        private bool _isTouching;
        private InputData _data;

        private float _currentVelocity;
        private float3 _moveVector;
        private Vector2? _mousePosition;

        internal OnTouchingContinuesCommand(bool isTouching, InputData data, Vector2? mousePosition, float currentVelocity, float3 moveVector)
        {
            _isTouching = isTouching;
            _mousePosition = mousePosition;
            _data = data;
            _moveVector = moveVector;
            _currentVelocity = currentVelocity;
        }

        internal void Execute(Vector2 mouseDeltaPos)
        {
            if (_isTouching)
            {
                if (_mousePosition != null)
                {
                    if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                    {
                        _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    }
                    else if (mouseDeltaPos.x < _data.HorizontalInputSpeed)

                    {
                        _moveVector.x = -_data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    }
                    else
                    {
                        _moveVector.x = Mathf.SmoothDamp(-_moveVector.x, 0f, ref _currentVelocity, _data.ClampSpeed);
                    }

                    _mousePosition = UnityEngine.Input.mousePosition;
                    InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                    {
                        HorizantalValue = _moveVector.x,
                        ClampValues = _data.ClampValues
                    });
                }
            }
        }
    }
}