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

        public SkillPoints skillPointsScript;

        internal bool isTalking = false;

        [SerializeField] private GameObject PauseCanvasMenu;

        [SerializeField] internal GameObject LevelCanvas;

        [SerializeField] internal GameObject CanvasInventario;

        [SerializeField] private GameObject CameraPivot;

        [SerializeField] private GameObject EventSystem;

        [SerializeField] private GameObject Light;

        [SerializeField] private Camera minimapCamera;
        [SerializeField] private GameObject minimapGameObject;
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
                    if (variable.name == "Player")
                        variable.transform.position = new Vector3(-11.0f, 0.2f, -22.6f);
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

            if (Input.GetKeyDown(KeyCode.Escape) && currentGameSceneIndex > 1 && SceneManager.sceneCount == 1)
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
                CameraPivot = FindFirstObjectByType<CameraPivot>(FindObjectsInactive.Include).gameObject;
                EventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include).gameObject;
                minimapCamera = FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include).transform
                    .Find("Camera").GetComponent<Camera>();
                minimapGameObject = FindFirstObjectByType<MarkerHolder>(FindObjectsInactive.Include).gameObject;
                playerGameObject = FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include).gameObject;
                Light = FindFirstObjectByType<Light>(FindObjectsInactive.Include).gameObject;
            }
        }

        private void LateUpdate()
        {
            switch (currentGameSceneIndex)
            {
                case 1:
                    return;

                case > 1:
                {
                    if (minimapCamera.enabled)
                    {
                        Quaternion rotacao = new Quaternion();
                        Vector3 orientacao = new Vector3();

                        orientacao.x = 0;
                        orientacao.y = 0;
                        playerGameObject = FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include)
                            .gameObject;
                        orientacao.z = playerGameObject.transform.rotation.eulerAngles.y;

                        rotacao.eulerAngles = orientacao;
                    }

                    break;
                }
            }
        }

        private void OnGUI()
        {
            currentGameSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentGameSceneIndex == 1)
            {
                /*PauseCanvasMenu.SetActive(false);
                CanvasInventario = null;
                LevelCanvas = null;
                CameraPivot = null;
                EventSystem = null;
                Light = null;
                playerGameObject.SetActive(false);*/

                foreach (var variable in staticObjects)
                {
                    Destroy(variable);
                }

                Destroy(gameObject);
            }

            if (currentGameSceneIndex > 1 && SceneManager.sceneCount == 1)
            {
                CameraPivot = FindFirstObjectByType<CameraPivot>(FindObjectsInactive.Include).gameObject;
                EventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include).gameObject;
                Light = FindFirstObjectByType<Light>(FindObjectsInactive.Include).gameObject;
                playerGameObject = FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include).gameObject;
                minimapGameObject = FindFirstObjectByType<MarkerHolder>(FindObjectsInactive.Include).gameObject;
                CameraPivot.SetActive(true);
                EventSystem.SetActive(true);
                Light.SetActive(true);
                playerGameObject.SetActive(true);

                if (isTalking == false)
                {
                    CanvasInventario = FindAnyObjectByType<CanvasInventario>(FindObjectsInactive.Include).gameObject;
                    LevelCanvas = FindAnyObjectByType<CanvasHUD>(FindObjectsInactive.Include).gameObject;
                    CanvasInventario.SetActive(true);
                    LevelCanvas.SetActive(true);
                }
                else
                {
                    CanvasInventario = FindAnyObjectByType<CanvasInventario>(FindObjectsInactive.Include).gameObject;
                    LevelCanvas = FindAnyObjectByType<CanvasHUD>(FindObjectsInactive.Include).gameObject;
                    CanvasInventario.SetActive(false);
                    LevelCanvas.SetActive(false);
                }

                if (nivelAtual != 1)
                {
                    VisualizarMiniMapa(true);
                }
            }
            else if (currentGameSceneIndex > 1 && SceneManager.sceneCount == 2)
            {
                CanvasInventario = FindAnyObjectByType<CanvasInventario>(FindObjectsInactive.Include).gameObject;
                LevelCanvas = FindAnyObjectByType<CanvasHUD>(FindObjectsInactive.Include).gameObject;
                CameraPivot = FindFirstObjectByType<CameraPivot>(FindObjectsInactive.Include).gameObject;
                EventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include).gameObject;
                Light = FindFirstObjectByType<Light>(FindObjectsInactive.Include).gameObject;
                minimapGameObject = FindFirstObjectByType<MarkerHolder>(FindObjectsInactive.Include).gameObject;
                CameraPivot.SetActive(false);
                CanvasInventario.SetActive(false);
                LevelCanvas.SetActive(false);
                EventSystem.SetActive(false);
                Light.SetActive(false);
                minimapGameObject.SetActive(false);
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
            PlayerPrefs.DeleteAll();
            SceneLoader.LoadScene(1);
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