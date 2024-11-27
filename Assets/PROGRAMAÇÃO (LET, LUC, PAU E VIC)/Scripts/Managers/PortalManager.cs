using Main_Folders.Scripts.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Main_Folders.Scripts.Managers
{
    public class PortalManager : MonoBehaviour
    {
        [Header("Player destination Transform:")]
        public Vector3 Destination;

        [Header("Player target scene index:")]
        [SerializeField]
        private int sceneId;

        private void Start()
        {
            PlayerMovement.isMovementBlocked = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //PlayerMovement.isMovementBlocked = true;
                other.GetComponent<PlayerMovement>().enabled = false;
                other.GetComponent<NavMeshAgent>().enabled = false;

                // Salvar a posição de destino antes de mudar a cena
                LevelsManager.Instance.MoverPlayer(Destination);

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
