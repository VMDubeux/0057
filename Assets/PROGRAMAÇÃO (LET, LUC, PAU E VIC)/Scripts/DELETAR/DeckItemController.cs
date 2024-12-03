using UnityEngine;
using UnityEngine.UI;

namespace Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.Inventory
{
    public class DeckItemController : MonoBehaviour
    {
        [SerializeField] private Image itemImage; // Refer�ncia � imagem do item no bot�o
        [SerializeField] private Button button; // Refer�ncia ao bot�o

        [SerializeField] private ItemPickUp itemPickUp; // Item associado a este bot�o

        void Awake()
        {
            // Obt�m refer�ncias aos componentes Image e Button
            itemImage = GetComponent<Image>();
            button = GetComponent<Button>();
        }

        private void Start()
        {
            if (button != null)
            {
                // Configura o evento de clique do bot�o para retornar o item ao invent�rio
                button.onClick.AddListener(ReturnItemToInventory);
            }
            else
            {
                Debug.LogError("Button component n�o encontrado no GameObject.");
            }
        }

        public void Setup(ItemPickUp item, Sprite itemSprite)
        {
            itemPickUp = item; // Associa o item ao bot�o
            if (itemImage != null)
            {
                itemImage.sprite = itemSprite; // Atualiza a imagem do bot�o
            }

            gameObject.SetActive(true);
        }

        public void ReturnItemToInventory()
        {
            if (itemPickUp != null)
            {
                // Retorna o item ao invent�rio
                InventoryManager.Instance.Add(itemPickUp);
                Clear(); // Limpa o bot�o e desativa o GameObject
            }
        }

        public bool IsEmpty()
        {
            // Verifica se o bot�o est� livre
            return itemPickUp == null;
        }

        private void Clear()
        {
            itemPickUp = null; // Limpa a refer�ncia ao item
            if (itemImage != null)
            {
                itemImage.sprite = null; // Limpa a imagem do bot�o
            }

            // Desativa o GameObject
            gameObject.SetActive(false);
        }
    }
}
