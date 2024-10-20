using System.Collections.Generic;
using UnityEngine;

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

        private List<(ObjectivePosition objectivePosition, RectTransform markerRectTransform)> currentObjectives;
        private List<(EnemyPosition enemyPosition, RectTransform markerRectTransform)> currentEnemies;
        private List<(VendorPosition vendorPosition, RectTransform markerRectTransform)> currentVendors;

        void Awake()
        {
            currentObjectives = new List<(ObjectivePosition objectivePosition, RectTransform markerRectTransform)>();
            currentEnemies = new List<(EnemyPosition enemyPosition, RectTransform markerRectTransform)>();
            currentVendors = new List<(VendorPosition vendorPosition, RectTransform markerRectTransform)>();
            playerObject = FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include).gameObject;
            minimapCamera = GameObject.Find("CameraMinimap").GetComponent<Camera>();
        }

        void Update()
        {
            Vector3 playerPosition = playerObject.transform.position;

            foreach ((ObjectivePosition objectivePosition, RectTransform markerRectTransform) marker in currentObjectives)
            {
                Vector3 offset = marker.objectivePosition.transform.position - playerPosition;
                offset = Vector3.ClampMagnitude(offset, minimapCamera.orthographicSize);

                // Normaliza o offset baseado no tamanho da c�mera
                Vector2 normalizedOffset = new Vector2(offset.x, offset.z) / minimapCamera.orthographicSize;

                // Converte o offset normalizado para a posi��o da UI
                Vector2 markerPosition = normalizedOffset * (markerParentRectTransform.rect.width / 2f);
                marker.markerRectTransform.anchoredPosition = markerPosition;
            }

            foreach ((EnemyPosition enemyPosition, RectTransform markerRectTransform) marker in currentEnemies)
            {
                Vector3 offset = marker.enemyPosition.transform.position - playerPosition;
                offset = Vector3.ClampMagnitude(offset, minimapCamera.orthographicSize);

                // Normaliza o offset baseado no tamanho da c�mera
                Vector2 normalizedOffset = new Vector2(offset.x, offset.z) / minimapCamera.orthographicSize;

                // Converte o offset normalizado para a posi��o da UI
                Vector2 markerPosition = normalizedOffset * (markerParentRectTransform.rect.width / 2f);
                marker.markerRectTransform.anchoredPosition = markerPosition;
            }

            foreach ((VendorPosition vendorPosition, RectTransform markerRectTransform) marker in currentVendors)
            {
                Vector3 offset = marker.vendorPosition.transform.position - playerPosition;
                offset = Vector3.ClampMagnitude(offset, minimapCamera.orthographicSize);

                // Normaliza o offset baseado no tamanho da c�mera
                Vector2 normalizedOffset = new Vector2(offset.x, offset.z) / minimapCamera.orthographicSize;

                // Converte o offset normalizado para a posi��o da UI
                Vector2 markerPosition = normalizedOffset * (markerParentRectTransform.rect.width / 2f);
                marker.markerRectTransform.anchoredPosition = markerPosition;
            }
        }

        public void AddObjectiveMarker(ObjectivePosition sender)
        {
            RectTransform rectTransform = Instantiate(markerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
            currentObjectives.Add((sender, rectTransform));
        }

        public void AddEnemyMarker(EnemyPosition sender)
        {
            RectTransform rectTransform = Instantiate(enemyMarkerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
            currentEnemies.Add((sender, rectTransform));
        }

        public void AddVendorMarker(VendorPosition sender)
        {
            RectTransform rectTransform = Instantiate(vendorMarkerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
            currentVendors.Add((sender, rectTransform));
        }

        public void RemoveObjectiveMarker(ObjectivePosition sender)
        {
            if (!currentObjectives.Exists(objective => objective.objectivePosition == sender))
                return;

            (ObjectivePosition pos, RectTransform rectTrans) foundObj = currentObjectives.Find(objective => objective.objectivePosition == sender);
            Destroy(foundObj.rectTrans.gameObject);
            currentObjectives.Remove(foundObj);
        }

        public void RemoveEnemyMarker(EnemyPosition sender)
        {
            if (!currentEnemies.Exists(objective => objective.enemyPosition == sender))
                return;

            (EnemyPosition pos, RectTransform rectTrans) foundObj = currentEnemies.Find(objective => objective.enemyPosition == sender);
            Destroy(foundObj.rectTrans.gameObject);
            currentEnemies.Remove(foundObj);
        }
    }
}
