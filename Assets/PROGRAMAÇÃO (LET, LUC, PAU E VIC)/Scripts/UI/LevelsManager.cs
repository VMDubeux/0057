using System.Collections;
using Main_Folders.Scripts.Audio;
using Main_Folders.Scripts.Managers;
using Main_Folders.Scripts.Minimapa;
using Main_Folders.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Main_Folders.Scripts.UI
{
    public class LevelsManager : MonoBehaviour
    {
        public static LevelsManager Instance;

        public static Vector3 NextPlayerPosition;

        public GameObject player;
        public GameObject partyManager;

        public SkillPoints skillPointsScript;
        [SerializeField] internal bool isTalking = false;

        [SerializeField] private GameObject PauseCanvasMenu;
        [SerializeField] internal GameObject LevelCanvas;
        [SerializeField] internal GameObject CanvasInventario;

        [SerializeField] private GameObject CameraPivot;
        [SerializeField] private Quaternion[] cameraRotations = new Quaternion[4];
        [SerializeField] private Vector3[] cameraPositions = new Vector3[4];
        private int activeTransformIndex = 0;
        private bool isTransitioning = false;

        [SerializeField] private GameObject EventSystem;
        [SerializeField] private GameObject Light;
        [SerializeField] private Camera minimapCamera;
        [SerializeField] private GameObject minimapGameObject;
        [SerializeField] private GameObject playerGameObject;
        [SerializeField] private MinimapaSetup[] setup;
        [SerializeField] private GameObject canvasQuestLog;
        [SerializeField] private GameObject gameJuiceCanvas;
        [SerializeField] private GameObject canvasMessageCard;
        [SerializeField] private GameObject canvasMinimapa;
        [SerializeField] private GameObject canvasTutorial;

        [Range(0, 3)] public int nivelInicial;
        private int nivelAtual;

        [SerializeField]
        [Tooltip("NÃO ESCREVA NADA")]
        internal int currentGameSceneIndex;

        [Header("UNDESTROYABLE:")]
        [SerializeField]
        private GameObject[] staticObjects;

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

                canvasTutorial.SetActive(true);

                // Adicionar callback para reposicionar o jogador após carregar uma nova cena
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                foreach (var variable in staticObjects)
                {
                    Destroy(variable);
                }

                Destroy(gameObject);

                canvasTutorial.SetActive(false);
            }

            gameJuiceCanvas.SetActive(false);
            PauseCanvasMenu.SetActive(false);
            canvasMessageCard.SetActive(false);
            CanvasInventario.SetActive(true);
            LevelCanvas.SetActive(true);
            canvasMinimapa.SetActive(true);
            canvasQuestLog.SetActive(true);
            player = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
            partyManager = GameObject.FindAnyObjectByType<PartyManager>().gameObject;
            canvasTutorial = GameObject.FindAnyObjectByType<CanvasTutorial>().gameObject;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (NextPlayerPosition != Vector3.zero) // Verifica se há uma posição salva
            {
                staticObjects[0].transform.position = NextPlayerPosition; // Reposiciona o jogador
                NextPlayerPosition = Vector3.zero; // Reseta o destino para evitar problemas futuros
            }
        }

        private void Start()
        {
            PauseCanvasMenu = FindAnyObjectByType<AudioControllerLevels>(FindObjectsInactive.Include).gameObject;
            PauseCanvasMenu.SetActive(false);

            Time.timeScale = 1.0f;
            nivelAtual = nivelInicial;
        }

        private void Update()
        {
            currentGameSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (Input.GetKeyDown(KeyCode.Z) && currentGameSceneIndex > 1)
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

            if (Input.GetKeyDown(KeyCode.C))
            {
                SwitchCamera();
            }

            if (currentGameSceneIndex > 1)
            {
                CanvasInventario = FindAnyObjectByType<CardInventoryManager>(FindObjectsInactive.Include).gameObject;
                LevelCanvas = FindAnyObjectByType<CanvasHUD>(FindObjectsInactive.Include).gameObject;
                EventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include).gameObject;
                minimapCamera = GameObject.Find("CameraMinimap").GetComponent<Camera>();
                minimapGameObject = FindFirstObjectByType<MarkerHolder>(FindObjectsInactive.Include).gameObject;
                playerGameObject = FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include).gameObject;
                Light = FindFirstObjectByType<Light>(FindObjectsInactive.Include).gameObject;
                canvasQuestLog = FindFirstObjectByType<CanvasQuestlog>(FindObjectsInactive.Include).gameObject;
                gameJuiceCanvas = FindFirstObjectByType<CanvasGameJuice>(FindObjectsInactive.Include).gameObject;
                canvasMessageCard = FindFirstObjectByType<CanvasMessageCard>(FindObjectsInactive.Include).gameObject;
                canvasMinimapa = FindFirstObjectByType<CanvasMinimapa>(FindObjectsInactive.Include).gameObject;
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

            if (currentGameSceneIndex > 1 && SceneManager.sceneCount == 1)
            {
                EventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include).gameObject;
                Light = FindFirstObjectByType<Light>(FindObjectsInactive.Include).gameObject;
                playerGameObject = FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include).gameObject;
                minimapGameObject = FindFirstObjectByType<MarkerHolder>(FindObjectsInactive.Include).gameObject;
                EventSystem.SetActive(true);
                Light.SetActive(true);
                playerGameObject.SetActive(true);

                if (isTalking == false)
                {
                    CanvasInventario = FindAnyObjectByType<CardInventoryManager>(FindObjectsInactive.Include).gameObject;
                    LevelCanvas = FindAnyObjectByType<CanvasHUD>(FindObjectsInactive.Include).gameObject;
                    canvasQuestLog = FindFirstObjectByType<CanvasQuestlog>(FindObjectsInactive.Include).gameObject;
                    CanvasInventario.SetActive(true);
                    LevelCanvas.SetActive(true);
                    canvasQuestLog.SetActive(true);
                }
                else
                {
                    CanvasInventario = FindAnyObjectByType<CardInventoryManager>(FindObjectsInactive.Include).gameObject;
                    LevelCanvas = FindAnyObjectByType<CanvasHUD>(FindObjectsInactive.Include).gameObject;
                    canvasQuestLog = FindFirstObjectByType<CanvasQuestlog>(FindObjectsInactive.Include).gameObject;
                    CanvasInventario.SetActive(false);
                    LevelCanvas.SetActive(false);
                    canvasQuestLog.SetActive(false);
                }

                if (nivelAtual != 1)
                {
                    VisualizarMiniMapa(true);
                }
            }
            else if (currentGameSceneIndex > 1 && SceneManager.sceneCount == 2)
            {
                CanvasInventario = FindAnyObjectByType<CardInventoryManager>(FindObjectsInactive.Include).gameObject;
                LevelCanvas = FindAnyObjectByType<CanvasHUD>(FindObjectsInactive.Include).gameObject;
                EventSystem = FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include).gameObject;
                Light = FindFirstObjectByType<Light>(FindObjectsInactive.Include).gameObject;
                minimapGameObject = FindFirstObjectByType<MarkerHolder>(FindObjectsInactive.Include).gameObject;
                canvasQuestLog = FindFirstObjectByType<CanvasQuestlog>(FindObjectsInactive.Include).gameObject;
                CanvasInventario.SetActive(false);
                LevelCanvas.SetActive(false);
                EventSystem.SetActive(false);
                Light.SetActive(false);
                minimapGameObject.SetActive(false);
                canvasQuestLog.SetActive(false);
            }
        }

        private void OnGUI()
        {
            currentGameSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentGameSceneIndex == 1)
            {
                foreach (var variable in staticObjects)
                {
                    Destroy(variable);
                }

                Destroy(gameObject);
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
            NextPlayerPosition = pos; // Salva a posição para a nova cena
            player.transform.position = pos;
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<NavMeshAgent>().enabled = true;
            partyManager.GetComponent<PartyManager>().SetPosition(pos);
        }

        private void SwitchCamera()
        {
            if (!isTransitioning)
            {
                activeTransformIndex = (activeTransformIndex + 1) % cameraRotations.Length;
                StartCoroutine(SmoothTransition(
                    CameraPivot.transform.position,
                    cameraPositions[activeTransformIndex],
                    CameraPivot.transform.rotation,
                    cameraRotations[activeTransformIndex],
                    1.0f
                ));
            }
        }

        private IEnumerator SmoothTransition(Vector3 startPosition, Vector3 endPosition, Quaternion startRotation, Quaternion endRotation, float duration)
        {
            isTransitioning = true;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                CameraPivot.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                CameraPivot.transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            CameraPivot.transform.position = endPosition;
            CameraPivot.transform.rotation = endRotation;

            isTransitioning = false;
        }
    }
}