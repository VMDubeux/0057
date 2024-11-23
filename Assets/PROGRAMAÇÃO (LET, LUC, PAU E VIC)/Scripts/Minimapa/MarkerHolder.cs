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

        // Lista de objetivos com suas posições e marcadores
        private List<(GameObject objectiveObject, RectTransform markerRectTransform)> currentObjectives;
        private List<(GameObject enemyPosition, RectTransform markerRectTransform)> currentEnemies;
        private List<(VendorPosition vendorPosition, RectTransform markerRectTransform)> currentVendors;

        void Awake()
        {
            currentObjectives = new List<(GameObject objectiveObject, RectTransform markerRectTransform)>();
            currentEnemies = new List<(GameObject enemyPosition, RectTransform markerRectTransform)>();
            currentVendors = new List<(VendorPosition vendorPosition, RectTransform markerRectTransform)>();
            playerObject = FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include).gameObject;
            minimapCamera = GameObject.Find("CameraMinimap").GetComponent<Camera>();
        }

        void Update()
        {
            Vector3 playerPosition = playerObject.transform.position;

            // Atualização para objetivos
            foreach (var marker in currentObjectives) // Correção aqui para usar "var"
            {
                GameObject objectiveObject = marker.objectiveObject;  // Acessando a tupla corretamente
                RectTransform markerRectTransform = marker.markerRectTransform;

                // Acessando a posição do objetivo diretamente a partir do GameObject
                Vector3 offset = objectiveObject.transform.position - playerPosition;
                offset = Vector3.ClampMagnitude(offset, minimapCamera.orthographicSize);

                // Normaliza o offset baseado no tamanho da câmera
                Vector2 normalizedOffset = new Vector2(offset.x, offset.z) / minimapCamera.orthographicSize;

                // Converte o offset normalizado para a posição da UI
                Vector2 markerPosition = normalizedOffset * (markerParentRectTransform.rect.width / 2f);
                markerRectTransform.anchoredPosition = markerPosition;

                // Verifica se o objeto tem o script QuestObjects
                QuestObjects questObject = objectiveObject.GetComponent<QuestObjects>();
                if (questObject != null && questObject.isCompleted)
                {
                    // Se o quest estiver completado, remove o marcador
                    RemoveObjectiveMarker(objectiveObject);
                }
            }

            // Atualização para inimigos
            foreach (var marker in currentEnemies) // Correção aqui para usar "var"
            {
                Vector3 offset = marker.enemyPosition.transform.position - playerPosition;
                offset = Vector3.ClampMagnitude(offset, minimapCamera.orthographicSize);

                // Normaliza o offset baseado no tamanho da câmera
                Vector2 normalizedOffset = new Vector2(offset.x, offset.z) / minimapCamera.orthographicSize;

                // Converte o offset normalizado para a posição da UI
                Vector2 markerPosition = normalizedOffset * (markerParentRectTransform.rect.width / 2f);
                marker.markerRectTransform.anchoredPosition = markerPosition;
            }

            // Atualização para vendedores
            foreach (var marker in currentVendors) // Correção aqui para usar "var"
            {
                Vector3 offset = marker.vendorPosition.transform.position - playerPosition;
                offset = Vector3.ClampMagnitude(offset, minimapCamera.orthographicSize);

                // Normaliza o offset baseado no tamanho da câmera
                Vector2 normalizedOffset = new Vector2(offset.x, offset.z) / minimapCamera.orthographicSize;

                // Converte o offset normalizado para a posição da UI
                Vector2 markerPosition = normalizedOffset * (markerParentRectTransform.rect.width / 2f);
                marker.markerRectTransform.anchoredPosition = markerPosition;
            }
        }

        // Método para adicionar marcadores de objetivos
        public void AddObjectiveMarker(GameObject sender)
        {
            // Usamos o GameObject de Objective diretamente
            RectTransform rectTransform = Instantiate(markerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
            currentObjectives.Add((sender, rectTransform)); // Adicionando corretamente a tupla
        }

        // Método para adicionar marcadores de inimigos
        public void AddEnemyMarker(GameObject sender)
        {
            RectTransform rectTransform = Instantiate(enemyMarkerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
            currentEnemies.Add((sender, rectTransform)); // Adicionando corretamente a tupla
        }

        // Método para adicionar marcadores de vendedores
        public void AddVendorMarker(VendorPosition sender)
        {
            RectTransform rectTransform = Instantiate(vendorMarkerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
            currentVendors.Add((sender, rectTransform)); // Adicionando corretamente a tupla
        }

        // Método para remover marcador de objetivo
        public void RemoveObjectiveMarker(GameObject sender)
        {
            // Verifica se o marcador existe na lista de objetivos
            var foundObj = currentObjectives.Find(objective => objective.objectiveObject == sender);
            if (foundObj.objectiveObject == null)
                return;

            Destroy(foundObj.markerRectTransform.gameObject); // Usando o markerRectTransform da tupla
            currentObjectives.Remove(foundObj); // Remove a tupla corretamente
        }

        // Método para remover marcador de inimigos
        public void RemoveEnemyMarker(GameObject sender)
        {
            var foundObj = currentEnemies.Find(objective => objective.enemyPosition == sender);
            if (foundObj.enemyPosition == null)
                return;

            Destroy(foundObj.markerRectTransform.gameObject); // Usando o markerRectTransform da tupla
            currentEnemies.Remove(foundObj); // Remove a tupla corretamente
        }
    }
}
