using UnityEngine;

public class MergeableItem : MouseInteractable
{
    public Serializable_FE_ArgBehav[] itemValues;
    public int itemTier;

    private void Start()
    {


        initialPosition = transform.position;

        for (int i = 0; i < ClickBehaviours.Length; ++i)
        {
            ClickBehaviours[i] = Instantiate(ClickBehaviours[i]);
            ClickBehaviours[i].Init(this.gameObject);
        }

        for (int i = 0; i < HoverBehaviours.Length; ++i)
        {
            HoverBehaviours[i] = Instantiate(HoverBehaviours[i]);
            HoverBehaviours[i].Init(this.gameObject);
        }

        for (int i = 0; i < UnHoverBehaviours.Length; ++i)
        {
            UnHoverBehaviours[i] = Instantiate(UnHoverBehaviours[i]);
            UnHoverBehaviours[i].Init(this.gameObject);
        }

        for (int i = 0; i < ReleaseBehaviours.Length; ++i)
        {
            ReleaseBehaviours[i] = Instantiate(ReleaseBehaviours[i]);
            if (ReleaseBehaviours[i].GetType()==typeof(IncrementValueByCollider))
            {
                IncrementValueByCollider temp = ReleaseBehaviours[i] as IncrementValueByCollider;
                temp.serializableNodes = new System.Collections.Generic.List<Serializable_FE_ArgBehav>();
                foreach(Serializable_FE_ArgBehav arg in itemValues)
                {
                    temp.serializableNodes.Add(arg);
                }
            }
            ReleaseBehaviours[i].Init(this.gameObject);
        }

        

    }


    public override void OnRelease()
    {
        base.OnRelease();

        CheckForMergeableHolders();


    }



    void CheckForMergeableHolders()
    {
        ColliderArray2D collidedObjects;
        Collider2D ownerCollider;
        if (this.gameObject.TryGetComponent<Collider2D>(out ownerCollider))
        {
            collidedObjects = ownerCollider.GetContactColliders(ContactFilter2D.noFilter);
            MergeableItemHolder mh;
            foreach (Collider2D item in collidedObjects)
            {

                if (item.gameObject.TryGetComponent<MergeableItemHolder>(out mh))
                {
                    mh.AddToMergePot(this.gameObject);
                }
            }

        }
        else
        {
            return;
        }
    }

}


