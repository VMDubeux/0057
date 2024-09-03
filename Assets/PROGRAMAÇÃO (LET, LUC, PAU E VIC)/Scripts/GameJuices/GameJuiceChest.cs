using System.Collections;
using UnityEngine;

namespace Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.GameJuices
{
    public class GameJuiceChest : global::GameJuices
    {
        [Header("Specific Variables:")]
        [SerializeField] private GameObject _lightGameJuice;
        [SerializeField] private GameObject _particleGameJuice;

        public GameJuiceChest(GameObject lightGameJuice, GameObject particleGameJuice)
        {
            _lightGameJuice = lightGameJuice;
            _particleGameJuice = particleGameJuice;
        }

        protected override void HandleTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !wasOpen)
            {
                CanvasGameJuices.SetActive(true);
                isInside = true;
            }
        }

        protected override void HandleTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CanvasGameJuices.SetActive(false);
                isInside = false;
            }
        }

        protected override IEnumerator IsInside()
        {
            if (isInside)
                if (Input.GetKeyDown(KeyCode.E))
                    HandleButtonPress();

            yield return null;
        }

        protected override void HandleButtonPress()
        {
            GetComponent<Animator>().SetBool("Trigger", true);
            wasOpen = true;
            CanvasGameJuices.SetActive(false);
            if (_lightGameJuice != null)
                _lightGameJuice.SetActive(true);

            if (_particleGameJuice != null)
                _particleGameJuice.SetActive(true);
        }

        protected override void SetupReturnToOrigin()
        {
            throw new System.NotImplementedException();
        }
    }
}
