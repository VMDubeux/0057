using UnityEngine;

public class PoseAnimation : MonoBehaviour
{


    [ContextMenu("Attack Pose")]
    public void AttackPose()
    {
            Animator anim = GetComponentInChildren<Animator>();
            anim.SetTrigger("attack");
            Debug.Log("Pose!");
    }
}
