using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimParam_Other", menuName = "Scriptable Objects/Animation/AnimPar_OnOther")]
public class SetAnimParamOnOther : AnimPar_Behaviour
{
    public override void OnActivate()
    {
        ColliderArray2D collidedObjects;
        Collider2D ownerCollider;
        if (Owner.TryGetComponent<Collider2D>(out ownerCollider))
        {
            collidedObjects = ownerCollider.GetContactColliders(ContactFilter2D.noFilter);
            foreach (Collider2D item in collidedObjects)
            {

                AnimatorControllerParameter param;
                Animator anim;
                if (item.gameObject.TryGetComponent<Animator>(out anim))
                {
                    float f = 0.0f;
                    foreach (Serializable_FE_ArgBehav arg in boolArgs)
                    {
                        bool b = arg.argument_value > 0;

                        anim.SetBool(arg.argument_name, b);

                    }

                    foreach (Serializable_FE_ArgBehav arg in floatArgs)
                    {
                        anim.SetFloat(arg.argument_name, arg.argument_value);
                    }

                    foreach (Serializable_FE_ArgBehav arg in triggerArgs)
                    {
                        anim.SetTrigger(arg.argument_name);
                    }
                }
            }

        }
        else
        {
            return;
        }
    }
}
