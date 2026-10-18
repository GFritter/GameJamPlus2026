using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct VisualElement
{
    public ParticleSystem particles;
    public Gradient colorGradient;
    public string axisKey;
    public string managedParameter;

}

public class MergeableItem : MouseInteractable
{
    public Serializable_FE_ArgBehav[] itemValues;
    public int itemTier;

    public VisualElement visualElement;

    public GameObject ParticlePrefab;
    public List<GameObject> particles; 

    public override void OnHover()
    {
        base.OnHover();

        foreach(GameObject pS in particles)
        {
            pS.SetActive(true);
        }

    }

    public override void OnHoverEnd()
    {
        base.OnHoverEnd();
        foreach (GameObject pS in particles)
        {
            pS.SetActive(false);
        
        }
    }

    private void Start()
    {
        particles = new List<GameObject>();

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

        CreateVisualElement();

    }


    public override void OnRelease()
    {
        base.OnRelease();

        CheckForMergeableHolders();


    }


    public void CreateVisualElement()
    {
        foreach(var arg in itemValues)
        {
            GameObject tempP = Instantiate(ParticlePrefab, this.transform);
            ParticleSystem ps = tempP.GetComponent<ParticleSystem>();

            ParticleSystem.MainModule m = ps.main;
            m.startColor = arg.argument_color;
            m.gravityModifier = -arg.argument_value;
            m.emitterVelocity *= arg.argument_value;

            tempP.SetActive(false);
            particles.Add(tempP);
            
        }
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


