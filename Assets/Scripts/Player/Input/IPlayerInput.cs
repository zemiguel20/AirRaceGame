using System;

namespace AirRace.Player
{
    public interface IPlayerInput
    {
        float RollInputMultiplier { get; }
        float PitchInputMultiplier { get; }
        float YawInputMultiplier { get; }
        float AccelerateInputMultiplier { get; }

        event Action PauseInputTriggered;
    }
}