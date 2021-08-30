using UnityEngine;

namespace Joyixir.Utils
{
    public class TimeScaler : MonoBehaviour
    {
        private static float _defaultFixedDeltaTime;
        private static bool _timeScalerInitialized;
        private static bool _timeAlreadyScaled;

        private void Awake()
        {
            if (_timeScalerInitialized) return;
            _defaultFixedDeltaTime = Time.fixedDeltaTime;
            _timeScalerInitialized = true;
        }

        public static void ScaleUnityTime(float slowdownFactor)
        {
            if(!_timeScalerInitialized)
                Debug.LogError("Attach timescaler to a unity game object, you may see unexpected behavior if you don't.");
            if (_timeAlreadyScaled)
            {
                Debug.LogWarning("Time is already scaled, pretending to scale unity default time scale.");
                SetUnityTimeScalesToNormal();
            }

            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * _defaultFixedDeltaTime;
            _timeAlreadyScaled = true;
        }

        public static void SetUnityTimeScalesToNormal()
        {
            if(!_timeScalerInitialized)
                Debug.LogError("Attach timescaler to a unity game object, you may see unexpected behavior if you don't.");
            if (!_timeAlreadyScaled) return;
            Time.timeScale = 1;
            Time.fixedDeltaTime = _defaultFixedDeltaTime;
            _timeAlreadyScaled = false;
        }
    }
}