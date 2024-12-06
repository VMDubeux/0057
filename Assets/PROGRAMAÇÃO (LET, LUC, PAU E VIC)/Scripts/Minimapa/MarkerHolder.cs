using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Folders.Scripts.Minimapa
{
    public class MarkerHolder : MonoBehaviour
    {
        public GameObject questMarker;
        public GameObject enemyMarkerPrefab;
        public GameObject vendorMarkerPrefab;
        public GameObject portalMarker;
        public GameObject playerMarker;
        public GameObject trunkMarker;
        public GameObject playerObject;
        public RectTransform markerParentRectTransform;
        public Camera minimapCamera;

        private List<(GameObject objectiveObject, RectTransform markerRectTransform)> currentObjectives;
        private List<(GameObject enemyPosition, RectTransform markerRectTransform)> currentEnemies;
        private List<(VendorPosition vendorPosition, RectTransform markerRectTransform)> currentVendors;
        private List<(PortalPosition portalPosition, RectTransform markerRectTransform)> currentPortals;
        private List<(TrunkPosition trunkPosition, RectTransform markerRectTransform)> currentTrunks;
        private (PlayerPosition playerPosition, RectTransform markerRectTransform) currentPlayer;

        void Awake()
        {
            // Impede a destruição deste objeto ao mudar de cena
            DontDestroyOnLoad(gameObject);

            currentObjectives = new List<(GameObject, RectTransform)>();
            currentEnemies = new List<(GameObject, RectTransform)>();
            currentVendors = new List<(VendorPosition, RectTransform)>();
            currentPortals = new List<(PortalPosition, RectTransform)>();
            currentTrunks = new List<(TrunkPosition, RectTransform)>();

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

        internal void InitializeReferences()
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

            // Atualização dos marcadores de portal
            foreach (var marker in currentPortals)
            {
                UpdateMarkerPosition(marker.portalPosition.transform.position, marker.markerRectTransform, playerPosition);
            }

            // Atualização dos marcadores de trunk
            foreach (var marker in currentTrunks)
            {
                UpdateMarkerPosition(marker.trunkPosition.transform.position, marker.markerRectTransform, playerPosition);
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
            RectTransform rectTransform = Instantiate(questMarker, markerParentRectTransform).GetComponent<RectTransform>();
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

        public void AddPortalMarker(PortalPosition sender)
        {
            RectTransform rectTransform = Instantiate(portalMarker, markerParentRectTransform).GetComponent<RectTransform>();
            currentPortals.Add((sender, rectTransform));
        }

        public void AddTrunkMarker(TrunkPosition sender)
        {
            RectTransform rectTransform = Instantiate(trunkMarker, markerParentRectTransform).GetComponent<RectTransform>();
            currentTrunks.Add((sender, rectTransform));
        }

        public void AddPlayerMarker(PlayerPosition sender)
        {
            RectTransform rectTransform = Instantiate(playerMarker, markerParentRectTransform).GetComponent<RectTransform>();
            currentPlayer = (sender, rectTransform);
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

        public void RemoveTrunkMarker(TrunkPosition sender)
        {
            var foundObj = currentTrunks.Find(trunk => trunk.trunkPosition == sender);
            if (foundObj.trunkPosition == null) return;

            Destroy(foundObj.markerRectTransform.gameObject);
            currentTrunks.Remove(foundObj);
        }

        public void RemovePortalMarker(GameObject sender)
        {
            var foundObj = currentPortals.Find(portal => portal.portalPosition == sender);
            if (foundObj.portalPosition == null) return;

            Destroy(foundObj.markerRectTransform.gameObject);
            currentPortals.Remove(foundObj);
        }

        public void ClearAllMarkers()
        {
            foreach (var marker in currentObjectives)
                Destroy(marker.markerRectTransform.gameObject);
            currentObjectives.Clear();

            foreach (var marker in currentEnemies)
                Destroy(marker.markerRectTransform.gameObject);
            currentEnemies.Clear();

            foreach (var marker in currentVendors)
                Destroy(marker.markerRectTransform.gameObject);
            currentVendors.Clear();

            foreach (var marker in currentPortals)
                Destroy(marker.markerRectTransform.gameObject);
            currentPortals.Clear();

            foreach (var marker in currentTrunks)
                Destroy(marker.markerRectTransform.gameObject);
            currentTrunks.Clear();

            Destroy(currentPlayer.markerRectTransform);
            currentPlayer.markerRectTransform = null;
            currentPlayer.playerPosition = null;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Limpeza feita ao carregar cena adicionalmente
            if (SceneManager.sceneCount > 1) return;
            ClearAllMarkers();
            InitializeReferences();
        }
    }
}
