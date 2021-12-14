using UnityEngine;

namespace AirRace
{
    public class Chronometer : MonoBehaviour
    {
        private float _time = 0;

        private void Start()
        {
            //Update is disabled by default
            enabled = false;
        }

        private void Update()
        {
            _time += Time.deltaTime;
        }

        public void ResetTime()
        {
            _time = 0;
        }

        public void StartCounting()
        {
            //Enable update
            enabled = true;
        }

        public void StopCounting()
        {
            //Disable update
            enabled = false;
        }

        public float time { get => _time; }
    }
}
