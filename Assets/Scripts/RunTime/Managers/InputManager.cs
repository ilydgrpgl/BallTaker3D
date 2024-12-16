using System.Collections.Generic;
using RunTime.Commands.Input;
using RunTime.Data.UnıtyObjects;
using RunTime.Data.ValueObjects;
using Signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RunTime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private InputData _data;
        private bool _isAvailableForTouch, _isFirstTimeTouchTaken, _isTouching;
        private Vector2? _mousePosition;
        private float _currentVelocity;
        private float3 _moveVector;

        private OnTouchingFinishedCommand _finishedCommand;
        private OnTouchingStartedCommand _startedCommand;
        private OnTouchingContinuesCommand _continuesCommand;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetInputData();
            Init();
        }

        private void Init()
        {
            _startedCommand = new OnTouchingStartedCommand(false);
            _finishedCommand = new OnTouchingFinishedCommand(true);
            _mousePosition = Input.mousePosition;
             _continuesCommand = new OnTouchingContinuesCommand(true, _data, _mousePosition, _currentVelocity, _moveVector);
        }

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").Data;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;

            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onReset += OnReset;
        }


        private void OnEnableInput()
        {
            _isAvailableForTouch = true;
        }

        private void OnDisableInput()
        {
            _isAvailableForTouch = false;
        }

        private void OnReset()
        {
            _isAvailableForTouch = false;
            _isTouching = false;
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void Update()
        {
            if (!_isAvailableForTouch) return;


            if (Input.GetMouseButtonUp(0))
            {

                _finishedCommand.Execute();
            }

            if (Input.GetMouseButtonDown(0))
            {
               

                _startedCommand.Execute();
                if (!_isFirstTimeTouchTaken)
                {
                    _isFirstTimeTouchTaken = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                }

                _mousePosition = Input.mousePosition;
            }


            if (Input.GetMouseButton(0))
            {
                _mousePosition = Input.mousePosition;


                if (_mousePosition.HasValue)
                {
                    _continuesCommand.Execute();
                }
            }
        }
    }
}