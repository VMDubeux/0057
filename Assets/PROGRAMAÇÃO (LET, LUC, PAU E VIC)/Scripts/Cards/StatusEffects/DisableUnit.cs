using UnityEngine;

namespace Main_Folders.Scripts.Cards.StatusEffects
{
    public class DisableUnit : StatusEffect
    {
        [SerializeField] internal int smrCount;

        [SerializeField] internal SkinnedMeshRenderer[] bodyRenderers;

        ModifierTags Tag = ModifierTags.WhenUnitDies;

        public override void OnInflicted()
        {
            _host.Modify[(int)Tag] += Change;
        }

        public override void OnRemoved()
        {
            _host.Modify[(int)Tag] -= Change;
        }

        void Change(ModifiedValues modifiedValues)
        {
            if (gameObject.CompareTag("Player")) return;

            foreach (SkinnedMeshRenderer smr in gameObject.transform.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                smrCount++;
            }

            bodyRenderers = new SkinnedMeshRenderer[smrCount];

            for (int i = 0; i < bodyRenderers.Length; i++)
            {
                bodyRenderers[i] = GetComponentsInChildren<SkinnedMeshRenderer>()[i];
            }

            if (bodyRenderers != null)
            {
                for (int i = 0; i < bodyRenderers.Length; i++)
                {
                    bodyRenderers[i].enabled = !bodyRenderers[i].enabled;
                }
            }

            Collider2D coll = GetComponentInChildren<Collider2D>();
            if (coll != null)
            {
                coll.enabled = !coll.enabled;
            }
        }
    }
}