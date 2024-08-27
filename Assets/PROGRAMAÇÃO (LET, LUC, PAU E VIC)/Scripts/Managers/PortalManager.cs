using System;
using System.Collections;
using Main_Folders.Scripts.Player;
using Main_Folders.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Folders.Scripts.Managers
{
    public class PortalManager : MonoBehaviour
    {
        [Header("Player destination coordinate:")]
        [Tooltip("Enter where the player will be teleported to after using the portal")]
        [SerializeField]
        private Vector3 destination;

        [Header("Player target scene index:")]
        [Tooltip("Inform (by index) which scene the player will be teleported to")]
        [SerializeField]
        private int sceneId;

        //Non-serialized fields
        private PartyManager _partyManager;

        private void Start()
        {
            _partyManager = FindFirstObjectByType<PartyManager>();
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            Vector3 destinationEnd = destination;

            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<PlayerMovement>().enabled = false;
                LevelsManager.Instance.MoverPlayer(destinationEnd);
                StartCoroutine(LoadScene());
            }
        }

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(0.5f);
            SceneLoader.LoadScene(sceneId);
        }
    }
}