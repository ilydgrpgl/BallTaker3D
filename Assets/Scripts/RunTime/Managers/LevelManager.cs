using RunTime.Commands.Level;
using RunTime.Data.UnıtyObjects;
using RunTime.Data.ValueObjects;
using RunTime.Enums;
using Signals;
using UnityEngine;

namespace RunTime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serilazed Variables

        [SerializeField] private Transform levelHolder;
        [SerializeField] private byte totalLevelCount;

        #endregion

        #region Private Variables

        private OnLevelLoaderCommand _levelLoaderCommand;
        private OnLevelDestroyerCommand _levelDestroyerCommand;
        private short _currentLevel;
        private LevelData _levelData;

        #endregion

        #endregion

        private void Awake()
        {
            _levelData = GetLevelData();
            _currentLevel = GetActiveLevel();
            Init();
        }

        private void Init()
        {
            _levelLoaderCommand = new OnLevelLoaderCommand(levelHolder);
            _levelDestroyerCommand = new OnLevelDestroyerCommand(levelHolder);
        }


        private byte GetActiveLevel()
        {
            return (byte)_currentLevel;
        }

        private LevelData GetLevelData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[_currentLevel];
        }

        private void OnEnable() => SubscribeEvents();


        private void SubscribeEvents()
        {
            //observer pattern 
            CoreGameSignals.Instance.onLevelInitialize += _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Execute;
            CoreGameSignals.Instance.onGetLevelValue += OnGetLevelValue;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
        }

        private void OnNextLevel()
        {
            _currentLevel++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        }

        private byte OnGetLevelValue()
        {
            return (byte)((byte)_currentLevel % totalLevelCount);
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Execute;
            CoreGameSignals.Instance.onGetLevelValue -= OnGetLevelValue;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
        }


        private void OnDisable() => UnSubscribeEvents();

        private void Start()
        {
            //Çok fazla level oynanabilir modunu almalı.
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
        }
    }
}