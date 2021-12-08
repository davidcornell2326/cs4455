using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FootIK : MonoBehaviour
{
    private Animator anim;
    public Vector3 footIKOffset;
    public float lookDownDistance = 1;
    public float aboveFootDistance = .1f;
    public Transform weaponHolder;

    struct hitReturn
    {
        public Vector3 point;
        public Vector3 normal;

        public hitReturn(Vector3 point, Vector3 normal) : this()
        {
            this.point = point;
            this.normal = normal;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("IKLeftFootWeight"));
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, anim.GetFloat("IKRightFootWeight"));

        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("IKLeftFootWeight"));
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat("IKRightFootWeight"));

        Vector3 lFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot).position;
        Vector3 rFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot).position;

        hitReturn lHit = GetHit(lFoot + Vector3.up * aboveFootDistance, lFoot - Vector3.up * lookDownDistance);
        hitReturn rHit = GetHit(rFoot + Vector3.up * aboveFootDistance, rFoot - Vector3.up * lookDownDistance);
        lFoot = lHit.point + footIKOffset;
        rFoot = rHit.point + footIKOffset;

        //float heightReduction = Mathf.Abs(lFoot.y - rFoot.y) / 2;
        //anim.bodyPosition = new Vector3(anim.bodyPosition.x, anim.bodyPosition.y - heightReduction, anim.bodyPosition.z);
        //weaponHolder.position = new Vector3(weaponHolder.position.x, weaponHolder.position.y - heightReduction, weaponHolder.position.z);



        anim.SetIKPosition(AvatarIKGoal.LeftFoot, lFoot);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rFoot);

        anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, lHit.normal));
        anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, rHit.normal));
    }

    private hitReturn GetHit(Vector3 origin, Vector3 dest)
    {
        RaycastHit hit;
        if (Physics.Linecast(origin, dest, out hit) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Environment"))
            return new hitReturn(hit.point, hit.normal);
        return new hitReturn(dest, Vector3.up);
    }
}
