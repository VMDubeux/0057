using UnityEngine;

public class PoseAnimation : MonoBehaviour
{


    [ContextMenu("Attack Pose")] public void AttackPose()
    {
            Animator anim = GetComponentInChildren<Animator>();
            anim.SetTrigger("attack");
            Debug.Log(anim.gameObject.name + " é o objeto");
    }
    [ContextMenu("Def Pose")] public void DefPose()
    {
        //implementa o VFX de defesa
        GameObject defVFX = gameObject.transform.Find("VFX Def").gameObject;
        if(defVFX.activeSelf == true)
        {
            defVFX.SetActive(false);
        }
        defVFX.SetActive(true);
        Debug.Log("VFX Def apply");       
    }
    [ContextMenu("Hit Pose")] public void HitAnim()
    {
        GameObject hitVFX = gameObject.transform.Find("Hit VFX").gameObject;
        if(hitVFX.activeSelf == true)
        {
            hitVFX.SetActive(false);
        }
        hitVFX.SetActive(true);
        Debug.Log("VFX Hit apply");   
    }
}
