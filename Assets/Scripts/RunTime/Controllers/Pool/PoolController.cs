using System.Collections.Generic;
using DG.Tweening;
using RunTime.Data.UnıtyObjects;
using RunTime.Data.ValueObjects;
using Signals;
using TMPro;
using UnityEngine;

namespace RunTime.Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshPro poolText;
        [SerializeField] private byte stageID;
        [SerializeField] private List<Animator> stageAnimators = new List<Animator>();
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Color poolAfterColor;

        #endregion

        #region Private Variables

        private PoolData _data;
        private byte _collectedCount;

        private readonly string _collectable = "Collectable";

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPoolData();
        }

        private PoolData GetPoolData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level")
                .Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()].Pools[stageID];
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Player"))
            {
                stageAnimators[0].SetBool("isColliding", true);
                stageAnimators[1].SetBool("isOpen", true);
            }

            if (other.CompareTag(_collectable))
            {
                IncreaseCollectedAmount();
                SetCollectedAmountToPool();
            }
        }


        private void Start()
        {
            SetRequiredAmountText();
        }

        private void SetRequiredAmountText()
        {
            poolText.text = $"0/{_data.ReguiredObjectCount}";
        }

        public bool TakeResults(byte managerStageValue)
        {
            Debug.Log("stageID: " + stageID + " managerStageValue: " + managerStageValue);
            if (stageID == managerStageValue)
            {
                return _collectedCount >= _data.ReguiredObjectCount;
            }

            return false;
        }

        private void IncreaseCollectedAmount()
        {
            _collectedCount++;
        }

        private void SetCollectedAmountToPool()
        {
            poolText.text = $"{_collectedCount}/{_data.ReguiredObjectCount}";
        }

        private void DecreaseCollectedAmount()
        {
            _collectedCount--;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                stageAnimators[0].SetBool("isColliding", true);
                stageAnimators[1].SetBool("isOpen", true);
            }

            if (other.CompareTag(_collectable))
            {
                SetCollectedAmountToPool();
            }
        }
    }
}