using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Folders.Scripts.Minimapa
{
    public class MarkerHolder : MonoBehaviour
    {
        public GameObject markerPrefab;
        public GameObject enemyMarkerPrefab;
        public GameObject vendorMarkerPrefab;
        public GameObject playerObject;
        public RectTransform markerParentRectTransform;
        public Camera minimapCamera;

        private List<(GameObject objectiveObject, RectTransform markerRectTransform)> currentObjectives;
        private List<(GameObject enemyPosition, RectTransform markerRectTransform)> currentEnemies;
        private List<(VendorPosition vendorPosition, RectTransform markerRectTransform)> currentVendors;

        void Awake()
        {
            // Impede a destruição deste objeto ao mudar de cena
            DontDestroyOnLoad(gameObject);

            currentObjectives = new List<(GameObject, RectTransform)>();
            currentEnemies = new List<(GameObject, RectTransform)>();
            currentVendors = new List<(VendorPosition, RectTransform)>();

            // Configura eventos para carregar referências ao mudar de cena
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Inicializa as referências
            InitializeReferences();
        }

        void OnDestroy()
        {
            // Remove o evento para evitar exceções
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void InitializeReferences()
        {
            playerObject = FindFirstObjectByType<PlayerMovement>()?.gameObject;
            minimapCamera = GameObject.Find("CameraMinimap")?.GetComponent<Camera>();
        }

        void Update()
        {
            if (playerObject == null || minimapCamera == null) return;

            Vector3 playerPosition = playerObject.transform.position;

            // Atualização dos marcadores de objetivos
            foreach (var marker in currentObjectives)
            {
                UpdateMarkerPosition(marker.objectiveObject.transform.position, marker.markerRectTransform, playerPosition);
            }

            // Atualização dos marcadores de inimigos
            foreach (var marker in currentEnemies)
            {
                UpdateMarkerPosition(marker.enemyPosition.transform.position, marker.markerRectTransform, playerPosition);
            }

            // Atualização dos marcadores de vendedores
            foreach (var marker in currentVendors)
            {
                UpdateMarkerPosition(marker.vendorPosition.transform.position, marker.markerRectTransform, playerPosition);
            }
        }

        private void UpdateMarkerPosition(Vector3 targetPosition, RectTransform markerRectTransform, Vector3 playerPosition)
        {
            Vector3 offset = targetPosition - playerPosition;
            offset = Vector3.ClampMagnitude(offset, minimapCamera.orthographicSize);

            Vector2 normalizedOffset = new Vector2(offset.x, offset.z) / minimapCamera.orthographicSize;
            Vector2 markerPosition = normalizedOffset * (markerParentRectTransform.rect.width / 2f);
            markerRectTransform.anchoredPosition = markerPosition;
        }

        public void AddObjectiveMarker(GameObject sender)
        {
            RectTransform rectTransform = Instantiate(markerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
            currentObjectives.Add((sender, rectTransform));
        }

        public void AddEnemyMarker(GameObject sender)
        {
            RectTransform rectTransform = Instantiate(enemyMarkerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
            currentEnemies.Add((sender, rectTransform));
        }

        public void AddVendorMarker(VendorPosition sender)
        {
            RectTransform rectTransform = Instantiate(vendorMarkerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
            currentVendors.Add((sender, rectTransform));
        }

        public void RemoveObjectiveMarker(GameObject sender)
        {
            var foundObj = currentObjectives.Find(objective => objective.objectiveObject == sender);
            if (foundObj.objectiveObject == null) return;

            Destroy(foundObj.markerRectTransform.gameObject);
            currentObjectives.Remove(foundObj);
        }

        public void RemoveEnemyMarker(GameObject sender)
        {
            var foundObj = currentEnemies.Find(enemy => enemy.enemyPosition == sender);
            if (foundObj.enemyPosition == null) return;

            Destroy(foundObj.markerRectTransform.gameObject);
            currentEnemies.Remove(foundObj);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InitializeReferences();
        }
    }
}
