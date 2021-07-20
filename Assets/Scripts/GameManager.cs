using System.Collections.Generic;
using AirRace.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirRace
{
    public class GameManager : MonoBehaviour
    {
        private List<MapInfoSO> _maps;

        // Start is called before the first frame update
        void Start()
        {
            _maps = SaveManager.LoadAllMapInfos();

            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        }
    }
}
