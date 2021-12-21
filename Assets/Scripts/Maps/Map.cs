using System;
using UnityEngine;

namespace AirRace
{
    [Serializable]
    public class Map
    {
        [SerializeField] private string _name;
        [SerializeField] private Texture2D _image;
        private Leaderboard _leaderboard = new Leaderboard();

        public string name { get { return _name; } }
        public Texture2D image { get { return _image; } }
        public Leaderboard leaderboard { get { return _leaderboard; } }
    }
}