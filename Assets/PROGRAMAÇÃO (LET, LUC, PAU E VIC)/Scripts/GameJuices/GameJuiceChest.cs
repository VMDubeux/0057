using Assets.PROGRAMA��O__LET__LUC__PAU_E_VIC_.Scripts.Inventory;
using System.Collections;
using Main_Folders.Scripts.Player;
using UnityEngine;

namespace Assets.PROGRAMA��O__LET__LUC__PAU_E_VIC_.Scripts.GameJuices
{
    public class GameJuiceChest : global::GameJuices
    {
        [Header("Specific Variables:")]
        [SerializeField] private GameObject _lightGameJuice;
        [SerializeField] private GameObject _particleGameJuice;
        [SerializeField] private ItemType[] _possibleItemTypes;

        protected override void Start()
        {
            if (PlayerPrefs.GetInt(_assetKey, 0) == 1)
            {
                wasOpen = true;

                if (_animator != null)
                {
                    string finalStateName = "Open";
                    _animator.Play(finalStateName, 0, 1f);
                    _animator.Update(0f);
                }

                if (_lightGameJuice != null)
                    _lightGameJuice.SetActive(true);

                if (_particleGameJuice != null)
                    _particleGameJuice.SetActive(false);
            }
        }

        protected override void HandleTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !wasOpen)
            {
                CanvasGameJuices.SetActive(true);
                isInside = true;
                _player = other.gameObject;
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
            if (isInside && Input.GetKeyDown(KeyCode.E))
            {
                HandleButtonPress();
            }

            yield return null;
        }

        protected override void HandleButtonPress()
        {
            if (!wasOpen)
            {
                if (_animator != null)
                {
                    _animator.SetBool("Trigger", true);
                }

                wasOpen = true;
                CanvasGameJuices.SetActive(false);

                if (_lightGameJuice != null)
                    _lightGameJuice.SetActive(true);

                if (_particleGameJuice != null)
                    _particleGameJuice.SetActive(true);

                AddRandomItemToInventory(); // Adiciona um item ao invent�rio

                PlayerPrefs.SetInt(_assetKey, 1); // Marca o ba� como aberto usando o identificador da classe pai
            }
        }

        protected override void AddRandomItemToInventory()
        {
            if (_possibleItemTypes != null && _possibleItemTypes.Length > 0)
            {
                int randomIndex = Random.Range(0, _possibleItemTypes.Length);
                ItemType selectedItemType = _possibleItemTypes[randomIndex];

                Debug.Log($"Selecionado ItemType: {selectedItemType}");

                if (_player != null)
                {
                    var playerShop = _player.GetComponent<PlayerShop>();
                    if (playerShop != null)
                    {
                        playerShop.BoughtItem(selectedItemType);
                    }
                    else
                    {
                        Debug.LogError("O componente PlayerShop n�o est� anexado ao jogador.");
                    }
                }
                else
                {
                    Debug.LogError("O jogador (_player) n�o est� definido.");
                }
            }
            else
            {
                Debug.LogWarning("O array de tipos de itens est� vazio ou n�o configurado.");
            }
        }

        protected override void SetupReturnToOrigin()
        {
            throw new System.NotImplementedException();
        }
    }
}
