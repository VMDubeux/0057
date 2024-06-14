using Main_Folders.Scripts.Audio;
using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.Minimapa;
using Main_Folders.Scripts.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Main_Folders.Scripts.UI
{
    public class LevelsManager : MonoBehaviour
    {
        public static LevelsManager Instance;

        internal bool isTalking = false;

        [SerializeField] private GameObject PauseCanvasMenu;

        [SerializeField] internal GameObject LevelCanvas;

        [SerializeField] internal GameObject CanvasInventario;

        [SerializeField] private GameObject CameraPivot;

        [SerializeField] private GameObject EventSystem;

        [SerializeField] private GameObject Light;

        [SerializeField] private Camera minimapCamera;
        [SerializeField] private GameObject minimapGameObject;
        [SerializeField] private GameObject coordenadasGameObject;
        [SerializeField] private GameObject playerGameObject;
        [SerializeField] private MinimapaSetup[] setup;

        [Range(0, 3)] public int nivelInicial;

        private int nivelAtual;

        [SerializeField] [Tooltip("NÃO ESCREVA NADA")]
        private int currentGameSceneIndex;

        [Header("UNDESTROYABLE:")] [SerializeField]
        private GameObject[] staticObjects;
        //encouterSystem, enemyManager, partyManager, inventoryManager, eventSystem;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                foreach (var variable in staticObjects)
                {
                    DontDestroyOnLoad(variable);
                }

                DontDestroyOnLoad(gameObject);
            }
            else
            {
                foreach (var variable in staticObjects)
                {
                    Destroy(variable);
                }

                Destroy(gameObject);
            }
        }

        private void Start()
        {
            PauseCanvasMenu = FindAnyObjectByType<AudioControllerLevels>(FindObjectsInactive.Include).gameObject;

            PauseCanvasMenu.SetActive(false);

            Time.timeScale = 1.0f; // Verificar necessidade;

            nivelAtual = nivelInicial;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3) && currentGameSceneIndex > 1)
            {
                TrocaMapa();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && currentGameSceneIndex > 1)
            {
                if (PauseCanvasMenu.activeSelf)
                {
                    PauseCanvasMenu.SetActive(false);
                    Time.timeScale = 1;
                }
                else
                {
                    PauseCanvasMenu.SetActive(true);
                    Time.timeScale = 0;
                }
            }

            if (currentGameSceneIndex > 1)
            {
                CanvasInventario = FindAnyObjectByType<CanvasInventario>(FindObjectsInactive.Include).gameObject;
                LevelCanvas = FindAnyObjectByType<CanvasHUD>(FindObjectsInactive.Include).gameObject;
                CameraPivot = FindAnyObjectByType<CameraPivot>(FindObjectsInactive.Include).gameObject;
                EventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include).gameObject;
                Light = FindFirstObjectByType<Light>(FindObjectsInactive.Include).gameObject;
                minimapCamera = GameObject.Find("Player").transform.Find("Camera").GetComponent<Camera>();
                minimapGameObject = FindFirstObjectByType<MarkerHolder>(FindObjectsInactive.Include).gameObject;
                coordenadasGameObject = minimapGameObject.transform.Find("CoordenadaBussola").gameObject;
                playerGameObject = GameObject.Find("Player").gameObject;
            }
        }

        private void LateUpdate()
        {
            if (minimapCamera.enabled)
            {
                Quaternion rotacao = new Quaternion();
                Vector3 orientacao = new Vector3();

                orientacao.x = 0;
                orientacao.y = 0;
                orientacao.z = playerGameObject.transform.rotation.eulerAngles.y;

                rotacao.eulerAngles = orientacao;

                coordenadasGameObject.transform.rotation = rotacao;
            }
        }

        private void OnGUI()
        {
            currentGameSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentGameSceneIndex == 1)
            {
                PauseCanvasMenu.SetActive(false);
                CanvasInventario = null;
                LevelCanvas = null;
                CameraPivot = null;
                EventSystem = null;
                Light = null;
            }

            if (currentGameSceneIndex > 1 && SceneManager.sceneCount == 1)
            {
                CameraPivot.SetActive(true);
                EventSystem.SetActive(true);
                Light.SetActive(true);

                if (isTalking == false)
                {
                    CanvasInventario.SetActive(true);
                    LevelCanvas.SetActive(true);
                }
                else
                {
                    CanvasInventario.SetActive(false);
                    LevelCanvas.SetActive(false);
                }
            }
            else if (currentGameSceneIndex > 1 && SceneManager.sceneCount == 2)
            {
                CameraPivot.SetActive(false);
                CanvasInventario.SetActive(false);
                LevelCanvas.SetActive(false);
                EventSystem.SetActive(false);
                Light.SetActive(false);
            }
        }

        private void TrocaMapa()
        {
            if (nivelAtual - 1 < setup.Length)
            {
                if (nivelAtual != 0)
                {
                    VisualizarMiniMapa(true);
                    minimapCamera.orthographicSize = setup[nivelAtual - 1].zoomLevel;
                }
                else
                {
                    VisualizarMiniMapa(false);
                }

                nivelAtual++;
            }
            else
            {
                VisualizarMiniMapa(false);
                nivelAtual = 1;
            }
        }

        private void VisualizarMiniMapa(bool estado)
        {
            minimapGameObject.SetActive(estado);
            minimapCamera.enabled = estado;
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
            Time.timeScale = 1.0f;
        }

        public void ResumeGame()
        {
            PauseCanvasMenu.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void MoverPlayer(Vector3 pos)
        {
            staticObjects[0].transform.position = pos;
            staticObjects[3].GetComponent<PartyManager>().SetPosition(pos);
        }
    }
}