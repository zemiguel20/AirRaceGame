using System.Collections;
using System.Collections.Generic;
using AirRace.Player;
using UnityEngine;

namespace AirRace.Test.Manual
{
    public class MovementTest : MonoBehaviour
    {
        [SerializeField] private Airplane player;
        [SerializeField] private InputController playerInput;

        // Start is called before the first frame update
        void Start()
        {
            player.Initialize(playerInput);
        }
    }
}
