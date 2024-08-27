using System.Collections;
using Main_Folders.Scripts.Visuals;
using UnityEngine;
using UnityEngine.UI;

namespace Main_Folders.Scripts.Units
{
    public class PlayerUnit : BattleVisuals
    {
        //public new TagModifier[] Modify = new TagModifier[(int)ModifierTags.None];

        public GameObject PlayerOverworldVisualPrefab;

        public int MaxEnergy;
        public int Graciosidade = 0;
        public int CurrentEnergy
        {
            get
            {
                return _currentEnergy;
            }
            set
            {
                //modify/add/control 
                _currentEnergy = value;
                UpdateEnergyMeter();
            }
        }
        [SerializeField]
        int _currentEnergy;
        public int DrawAmount;
        public int MaxCards;
        [SerializeField] Text _energyMeter;

        void Awake()
        {
            _energyMeter = GameObject.Find("Canvas/EnergyMeter").GetComponent<Text>();
        }
        public override IEnumerator Recover()
        {
            yield return StartCoroutine(base.Recover());
            yield return StartCoroutine(CardsController.Instance.DiscardHand());
            CurrentEnergy = MaxEnergy;
            int cardsToDraw = Mathf.Min(MaxCards - CardsController.Instance.Hand.Cards.Count, DrawAmount);
            yield return StartCoroutine(CardsController.Instance.Draw(cardsToDraw));
        }
        void UpdateEnergyMeter()
        {
            _energyMeter.text = string.Format("{0}/{1}", CurrentEnergy, MaxEnergy);
        }
    }
}
