using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AirRace.UI
{
    public class DisableOnStart : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);
        }
    }
}