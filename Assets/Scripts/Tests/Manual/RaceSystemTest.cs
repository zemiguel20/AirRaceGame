using System.Collections;
using System.Collections.Generic;
using AirRace.Player;
using AirRace.Race;
using UnityEngine;

namespace AirRace.Test.Manual
{
    public class RaceSystemTest : MonoBehaviour
    {
        [SerializeField] Airplane player;
        [SerializeField] Path path;
        [SerializeField] InputController inputController;

        // Start is called before the first frame update
        void Start()
        {
            player.Initialize(inputController);
            var raceController = gameObject.AddComponent<RaceController>();
            raceController.Initialize(player, path, new Leaderboard(), inputController);
            raceController.StartRace();
        }
    }
}
