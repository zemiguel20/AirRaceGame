using System;
using UnityEngine;

namespace AirRace
{
    [Serializable]
    public class Map
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;

        public string name { get { return _name; } }
        public Sprite image { get { return _image; } }
    }
}