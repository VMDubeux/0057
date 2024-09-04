using UnityEngine;
using UnityEngine.UI;

namespace Assets.PROGRAMAÇÃO__LET__LUC__PAU_E_VIC_.Scripts.Inventory
{
    public class DeckItemController : MonoBehaviour
    {
        [SerializeField] private Image itemImage; // Referência à imagem do item no botão
        [SerializeField] private Button button; // Referência ao botão

        [SerializeField] private ItemPickUp itemPickUp; // Item associado a este botão

        void Awake()
        {
            // Obtém referências aos componentes Image e Button
            itemImage = GetComponent<Image>();
            button = GetComponent<Button>();
        }

        private void Start()
        {
            if (button != null)
            {
                // Configura o evento de clique do botão para retornar o item ao inventário
                button.onClick.AddListener(ReturnItemToInventory);
            }
            else
            {
                Debug.LogError("Button component não encontrado no GameObject.");
            }
        }

        public void Setup(ItemPickUp item, Sprite itemSprite)
        {
            itemPickUp = item; // Associa o item ao botão
            if (itemImage != null)
            {
                itemImage.sprite = itemSprite; // Atualiza a imagem do botão
            }

            gameObject.SetActive(true);
        }

        public void ReturnItemToInventory()
        {
            if (itemPickUp != null)
            {
                // Retorna o item ao inventário
                InventoryManager.Instance.Add(itemPickUp);
                Clear(); // Limpa o botão e desativa o GameObject
            }
        }

        public bool IsEmpty()
        {
            // Verifica se o botão está livre
            return itemPickUp == null;
        }

        private void Clear()
        {
            itemPickUp = null; // Limpa a referência ao item
            if (itemImage != null)
            {
                itemImage.sprite = null; // Limpa a imagem do botão
            }

            // Desativa o GameObject
            gameObject.SetActive(false);
        }
    }
}
