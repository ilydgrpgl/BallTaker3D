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
            _startedCommand = new OnTouchingStartedCommand(true);
            _finishedCommand = new OnTouchingFinishedCommand(false);
            _continuesCommand = new OnTouchingContinuesCommand(_isTouching, _data, _mousePosition, _currentVelocity, _moveVector);
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
            CoreGameSignals.Instance.onReset += OnReset;
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
        }


        private void OnEnableInput()
        {
            _isAvailableForTouch = false;
        }

        private void OnDisableInput()
        {
            _isAvailableForTouch = true;
        }

        private void OnReset()
        {
            _isAvailableForTouch = false;
            _isTouching = false;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
        }

        private void Update()
        {
            if (!_isAvailableForTouch) return;
            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _startedCommand.Execute();
            }

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                _finishedCommand.Execute();
                if (!_isFirstTimeTouchTaken)
                {
                    _isFirstTimeTouchTaken = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                }

                _mousePosition = Input.mousePosition;
            }


            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
                _continuesCommand.Execute(mouseDeltaPos);
            }
        }

        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);

            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}