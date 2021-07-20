using UnityEngine;

namespace AirRace.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] GameObject grid;

        private void Start()
        {
            LoadMapButtons();
        }

        private void LoadMapButtons()
        {
            // MapButton button = Resources.Load<MapButton>("MainMenu/MapButton");
            // foreach (MapInfoSO map in GameManager.Instance.Maps)
            // {
            //     MapButton instance = Instantiate(button);
            //     instance.transform.SetParent(grid.transform);
            //     instance.SetMapInfo(map);
            // }
        }

    }
}