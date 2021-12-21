using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace AirRace
{
    public class MapListViewController
    {
        public event Action selectedMapChanged;

        private ListView mapListView;
        private MapRepository mapRepository;
        private VisualTreeAsset mapEntryTemplate;

        public MapListViewController(ListView mapListView, MapRepository mapRepository, VisualTreeAsset mapEntryTemplate)
        {
            this.mapListView = mapListView;
            this.mapEntryTemplate = mapEntryTemplate;
            this.mapRepository = mapRepository;
        }

        public void InitializeMapList()
        {
            mapListView.makeItem = MakeMapEntry;
            mapListView.bindItem = BindMapEntry;
            mapListView.itemsSource = mapRepository.maps;

            mapListView.onSelectionChange += OnSelectionChange;
        }

        //ListView specific callback to create map entries
        private VisualElement MakeMapEntry()
        {
            VisualElement mapEntry = mapEntryTemplate.Instantiate();
            return mapEntry;
        }

        //ListView specific callback to bind data to entries
        private void BindMapEntry(VisualElement entry, int index)
        {
            Map map = mapRepository.maps[index];
            entry.userData = map;
            //Each list entry has a name label and an image
            entry.Query<Label>("map-name").First().text = map.name;
            entry.Query<VisualElement>("map-image").First().style.backgroundImage = new StyleBackground(map.image);
        }

        private void OnSelectionChange(IEnumerable<object> selection)
        {
            //Only one selected item allowed, so its cleaner to get it
            //directly from the ListView instead of the selection list passed on
            selectedMapChanged?.Invoke();
        }

        public Map selectedMap
        {
            get { return mapListView.selectedItem as Map; }
        }
    }
}
