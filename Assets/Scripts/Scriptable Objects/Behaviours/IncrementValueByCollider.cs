using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "FE_Behaviour", menuName = "Scriptable Objects/ValueByCollider")]
public class IncrementValueByCollider : FE_Behaviour
{
    public override void OnActivate()
    {
        ColliderArray2D collidedObjects;
        Collider2D ownerCollider ;
        if (Owner.TryGetComponent<Collider2D>(out ownerCollider))
        {
            collidedObjects = ownerCollider.GetContactColliders(ContactFilter2D.noFilter);
            foreach (Collider2D item in collidedObjects)
            {
                Entity e;
                if(item.TryGetComponent<Entity>(out e))
                {
                    foreach(KeyValuePair<string,float> arg in Arguments)
                    {
                        e.UpdateValue(arg.Key, arg.Value);
                    }

                    if(DestroyOwnerOnActiveEnd)
                    {
                        Destroy(Owner);
                    }

                    break;
                }
            }

        }
        else
        {
            return;
        }

        
    }



}
