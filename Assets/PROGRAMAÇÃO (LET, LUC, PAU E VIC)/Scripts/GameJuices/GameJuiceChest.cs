using System.Collections;
using System.Collections.Generic;
using Main_Folders.Scripts.Player;
using UnityEngine;

namespace Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.GameJuices
{
    public class GameJuiceChest : global::GameJuices
    {
        [Header("Specific Variables:")]
        [SerializeField] private GameObject _lightGameJuice;
        [SerializeField] private GameObject _particleGameJuice;
        [SerializeField] private CardToPickUp[] allAvailableCards;
        [SerializeField] private CardToPickUp.CardRarity[] _DroppableCardsRarity;
        [SerializeField] private List<CardToPickUp> _CardsToDrop;

        protected override void Start()
        {
            StartCoroutine(InitializeAfterDelay());
        }

        private IEnumerator InitializeAfterDelay()
        {
            yield return new WaitForEndOfFrame(); // Garante que tudo seja inicializado antes

            if (PlayerPrefs.GetInt(_assetKey, 0) == 1)
            {
                HandleChestAlreadyOpen();
            }
            else
            {
                InitializeDroppableCards();
            }
        }

        private void HandleChestAlreadyOpen()
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

        private void InitializeDroppableCards()
        {
            _CardsToDrop = new List<CardToPickUp>();

            if (CardInventoryManager.Instance == null || CardInventoryManager.Instance.cardsToPick == null)
            {
                Debug.LogError("CardInventoryManager.Instance ou cardsToPick não está inicializado!");
                return;
            }

            allAvailableCards = CardInventoryManager.Instance.cardsToPick;

            foreach (var card in allAvailableCards)
            {
                if (System.Array.Exists(_DroppableCardsRarity, rarity => rarity == card.cardRarity))
                {
                    Debug.Log($"Carta adicionada: {card.Name}, Raridade: {card.cardRarity}");
                    _CardsToDrop.Add(card);
                }
            }

            Debug.Log($"Total de cartas dropáveis configuradas: {_CardsToDrop.Count}");
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

                AddRandomItemToInventory(); // Adiciona um item ao inventário

                PlayerPrefs.SetInt(_assetKey, 1);
            }
        }

        internal override void AddRandomItemToInventory()
        {
            if (_CardsToDrop != null && _CardsToDrop.Count > 0)
            {
                int randomIndex = Random.Range(0, _CardsToDrop.Count);
                CardToPickUp randomSelectedCardToPick = _CardsToDrop[randomIndex];

                Debug.Log($"Carta selecionada: {randomSelectedCardToPick.Name} (Posição no inventário: {randomSelectedCardToPick.InventoryPos}).");

                CardInventoryManager.Instance.CardPickedUp(randomSelectedCardToPick);
            }
            else
            {
                Debug.LogWarning("A lista '_CardsToDrop' está vazia ou não foi inicializada.");
            }
        }

        protected override void SetupReturnToOrigin()
        {
            throw new System.NotImplementedException();
        }
    }
}
