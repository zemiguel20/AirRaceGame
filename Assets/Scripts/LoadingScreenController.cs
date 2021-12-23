using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace AirRace
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        private Label loadingLabel;

        private void Awake()
        {
            loadingLabel = document.rootVisualElement.Query<Label>("loading-text");
        }

        public void Initialize(AsyncOperation loadingOperation)
        {
            loadingLabel.text = loadingOperation.progress.ToString();
            StartCoroutine(UpdateInfo(loadingOperation));
        }

        private IEnumerator UpdateInfo(AsyncOperation loadingOperation)
        {
            while (!loadingOperation.isDone)
            {
                loadingLabel.text = loadingOperation.progress.ToString("P0");
                yield return null;
            }

            loadingLabel.text = "Loading complete";
        }
    }
}
