using System.Collections.Generic;
using Main_Folders.Scripts.Player;
using UnityEngine;

namespace Main_Folders.Scripts.Minimapa
{
    public class MarkerHolder : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject markerPrefab;
        public GameObject playerObject;
        public RectTransform markerParentRectTransform;
        public Camera minimapCamera;

        private List<(ObjectivePosition objectivePosition, RectTransform markerRectTransform)> currentObjectives;

        // Start is called before the first frame update
        void Awake()
        {
            currentObjectives = new List<(ObjectivePosition objectivePosition, RectTransform markerRectTransform)>();
            playerObject = FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include).gameObject;
            minimapCamera = FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include).transform.Find("Camera").GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            foreach ((ObjectivePosition objectivePosition, RectTransform markerRectTransform) marker in
                     currentObjectives)
            {
                Vector3 offset = Vector3.ClampMagnitude(
                    marker.objectivePosition.transform.position - playerObject.transform.position,
                    minimapCamera.orthographicSize);
                offset = offset / minimapCamera.orthographicSize * (markerParentRectTransform.rect.width / 2f);
                marker.markerRectTransform.anchoredPosition = new Vector2(offset.x, offset.z);
            }
        }

        public void AddObjectiveMarker(ObjectivePosition sender)
        {
            RectTransform rectTransform =
                Instantiate(markerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
            currentObjectives.Add((sender, rectTransform));
        }

        public void RemoveObjectiveMarker(ObjectivePosition sender)
        {
            if (!currentObjectives.Exists(objective => objective.objectivePosition == sender))
                return;
            (ObjectivePosition pos, RectTransform rectTrans) foundObj =
                currentObjectives.Find(objective => objective.objectivePosition == sender);
            Destroy(foundObj.rectTrans.gameObject);
            currentObjectives.Remove(foundObj);
        }
    }
}