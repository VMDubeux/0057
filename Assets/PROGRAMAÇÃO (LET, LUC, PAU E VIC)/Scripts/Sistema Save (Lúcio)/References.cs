using UnityEngine;

namespace Main_Folders.Scripts
{
    public class References : MonoBehaviour
    {
        public GameObject CurrentEnemyBattle;
        public GameObject player;
        public static References Instance;
        private void Awake()
        {
            Instance = this;
            GameObject[] objs = GameObject.FindGameObjectsWithTag("ref");

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        }
    }
}
