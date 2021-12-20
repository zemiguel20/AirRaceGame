using UnityEngine;

namespace AirRace
{
    public class MapRepository : MonoBehaviour
    {
        [SerializeField] private Map[] _maps;

        public Map[] maps { get { return _maps; } }
    }
}
