using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main_Folders.Scripts.Visuals
{
    public delegate void OnBattleVisuals(BattleVisuals battleVisuals);
    public class BattleVisuals : MonoBehaviour, IPointerClickHandler
    {
        public OnBattleVisuals onBattleVisualsClicked = delegate { };
        public OnBattleVisuals onBattleVisualsTakeTurn = delegate { };
        public TagModifier[] Modify = new TagModifier[(int)ModifierTags.None];

        [SerializeField] private Slider healthBar;
        [SerializeField] private TextMeshProUGUI levelText;

        [SerializeField]
        public int HP, MaxHP, Block, Strength, BlockAmount, Level, SerialNumber;
        [SerializeField] private BattleEntities currentEntity;

        private Animator anim;

        private const string LEVEL_ABB = "Lvl: ";

        private const string IS_ATTACK_PARAM = "IsAttack";
        private const string IS_HIT_PARAM = "IsHit";
        private const string IS_DEATH_PARAM = "IsDeath";

        [SerializeField] private TextMeshProUGUI nameCharacterUI;

        private void Start()
        {
            anim = gameObject.GetComponent<Animator>();
            nameCharacterUI = GetComponentInChildren<TextMeshProUGUI>();
            nameCharacterUI.text = currentEntity.ToString();
        }

        public void SetStartingValues(int hp, int maxHP, int block, int strength, int level, int serialNumber, BattleEntities currentEntity)
        {
            this.HP = hp;
            this.MaxHP = maxHP;
            this.Block = block;
            this.Strength = strength;
            this.Level = level;
            this.SerialNumber = serialNumber;
            this.currentEntity = currentEntity;
            levelText.text = LEVEL_ABB + this.Level.ToString();
            UpdateHealthBar();
        }

        public int GetStatValue(int value)
        {
            int statValue = 0;

            switch (value)
            {
                case 1:
                    statValue = currentEntity.HP;
                    break;
                case 3:
                    statValue = currentEntity.Block;
                    break;
                case 4:
                    statValue = currentEntity.Strength;
                    break;
                default:
                    statValue = 0;
                    break;
            }

            return statValue;
        }

        public void SetStatValue(int value, int newStatValue)
        {
            switch (value)
            {
                case 1:
                    HP = newStatValue;
                    currentEntity.HP = newStatValue;
                    ChangeHealth(newStatValue);
                    break;
                case 3:
                    Block = newStatValue;
                    currentEntity.Block = newStatValue;
                    break;
                case 4:
                    Strength = newStatValue;
                    currentEntity.Strength = newStatValue;
                    break;
            }
        }

        public void ChangeHealth(int hp)
        {
            this.HP = hp;
            /*if (hp <= 0)
        {
            PlayDeathAnimation();
            Destroy(gameObject, 1);
        }*/
            UpdateHealthBar();
        }

        public void UpdateHealthBar()
        {
            healthBar.maxValue = MaxHP;
            healthBar.value = HP;

            if (healthBar.value <= 0)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                Debug.Log("MORREU, JOVEM");
            }
        }

        /*public void PlayAttackAnimation()
    {
        anim.SetTrigger(IS_ATTACK_PARAM);
    }
    public void PlayHitAnimation()
    {
        anim.SetTrigger(IS_HIT_PARAM);
    }
    public void PlayDeathAnimation()
    {
        anim.SetTrigger(IS_DEATH_PARAM);
    }*/

        public void OnPointerClick(PointerEventData eventData)
        {
            onBattleVisualsClicked(this);
        }

        public virtual IEnumerator Recover()
        {
            yield return null;
            SetStatValue(3, 0); //Toda vez que a batalha (o seu respectivo turno) for iniciada o atributo Block aparecer� zerado.
            onBattleVisualsTakeTurn(this);
        }
    
        public int GetBlockAmount()
        {
            BlockAmount = Random.Range(2,5);
            return BlockAmount;
        }
    }
}