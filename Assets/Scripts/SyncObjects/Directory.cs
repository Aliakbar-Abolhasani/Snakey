using UnityEngine;

namespace Snakey.SyncObjects
{
    public class Directory : MonoBehaviour
    {
        public static Directory Instance { get; private set; }

        public LineRenderer LineRenderer;

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