using System.Collections.Generic;
using UnityEngine;

namespace Snakey.SyncObjects
{
    public class Directory : MonoBehaviour
    {
        public static Directory Instance { get; private set; }

        public List<LineRenderer> LineRenderers;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Singleton already exists in the scene", gameObject);
                Destroy(gameObject);
            }
        }
    }
}