using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Light))]
    public class FireIntensity : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;

        private float _currentTime;
        private float _maxTime;
        private Light _light;

        private void Awake()
        {
            _light = GetComponent<Light>();
            
            _maxTime = _curve.keys[_curve.keys.Length - 1].time;
            _currentTime = 0f;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;

            _light.intensity = _curve.Evaluate(_currentTime);

            if (_currentTime >= _maxTime)
            {
                _currentTime = 0f;
            }
        }
    }
}