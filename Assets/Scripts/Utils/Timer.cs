namespace AirRace.Utils
{
    public class Timer
    {
        private int _remainingSeconds;

        public Timer(int seconds)
        {
            _remainingSeconds = seconds;
        }

        public int RemaingSeconds { get => _remainingSeconds; }
        public bool IsFinished { get => _remainingSeconds <= 0; }

        public void TickSeconds(int seconds)
        {
            _remainingSeconds -= seconds;

            if (_remainingSeconds < 0)
            {
                _remainingSeconds = 0;
            }
        }

    }
}
