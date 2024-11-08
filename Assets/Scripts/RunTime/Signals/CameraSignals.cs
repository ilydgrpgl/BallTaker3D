using UnityEngine;
using UnityEngine.Events;

namespace RunTime.Signals
{
    public class CameraSignals : MonoBehaviour
    {
        #region Singleton

        public static CameraSignals Instance;

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

        public UnityAction onSetCameraTarget = delegate { };
        
        
    }
}