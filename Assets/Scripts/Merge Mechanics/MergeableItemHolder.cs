using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MergeableItemHolder : MouseInteractable
{
    public List<MergeableItem> mItems;

    public float minRandRange, maxRandRange;

    public float SpawnOffsetY,SpawnOffsetX;

    public List<GameObject> mItemsList;

    public FE_Behaviour[] OnMergeBehaviours, OnExpelBehaviours;

    public Dictionary<string, float> ItemValues;
    public Dictionary<string, Color> ItemColors;

    public Collider2D bancadaCollider;

    public GameObject mergeableItemPrefab;

    public Sprite[] spritesTier1, spritesTier2, spritesTier3, spritesTier4, spritesTier5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void  Start()
    {

        ItemValues = new Dictionary<string, float>();
        ItemColors = new Dictionary<string, Color>();
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
            ReleaseBehaviours[i].Init(this.gameObject);
        }

        for (int i = 0; i < OnMergeBehaviours.Length; ++i)
        {
            OnMergeBehaviours[i] = Instantiate(OnMergeBehaviours[i]);
            OnMergeBehaviours[i].Init(this.gameObject);
        }


        for (int i = 0; i < OnExpelBehaviours.Length; ++i)
        {
            OnExpelBehaviours[i] = Instantiate(OnExpelBehaviours[i]);
            OnExpelBehaviours[i].Init(this.gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddToMergePot(GameObject obj)
    {
        mItemsList.Add(obj);
        MergeableItem mI;

        if(obj.TryGetComponent(out mI))
        {
            mItems.Add(mI);
        }

        obj.SetActive(false);
    }

    public void Merge()
    {
        foreach(MergeableItem i in mItems)
        {
            foreach(Serializable_FE_ArgBehav arg in i.itemValues)
            {
                if(ItemValues.ContainsKey(arg.argument_name))
                {
                    ItemValues[arg.argument_name] += arg.argument_value;
                }

                else
                {
                    ItemValues.Add(arg.argument_name, arg.argument_value);
                    ItemColors.Add(arg.argument_name, arg.argument_color);
                }
            }
        }
        Dictionary<string, float> tempDictionary = new Dictionary<string, float>();
        //modulate and alter values, new things can be added here to add to merge complexity
        foreach(KeyValuePair<string,float> itemV in ItemValues)
        {
            tempDictionary.Add(itemV.Key, (ItemValues[itemV.Key] / (mItems.Count - 1)) + (Random.Range(minRandRange,maxRandRange) * mItems.Count));
        }

        foreach (KeyValuePair<string,float> i in tempDictionary)
        {
            ItemValues[i.Key] = i.Value;
        }

        GameObject newItem = Instantiate(mergeableItemPrefab,this.transform.position,this.transform.rotation,transform.parent);
        MergeableItem tempMI = newItem.GetComponent<MergeableItem>();
        tempMI.itemValues = new Serializable_FE_ArgBehav[ItemValues.Count];

        int e = 0;

        foreach(KeyValuePair<string,float> v in ItemValues)
        {
            tempMI.itemValues[e].argument_name = v.Key;
            tempMI.itemValues[e].argument_value = v.Value;
            tempMI.itemValues[e].argument_color = ItemColors[v.Key];
            ++e;
        }


        int tempItemTier = (ItemValues.Count + mItemsList.Count) / 2;
        tempMI.itemTier = tempItemTier;

        switch (tempItemTier)
        {
            case 0:newItem.GetComponent<SpriteRenderer>().sprite = spritesTier1[Random.Range(0,spritesTier1.Length)]; break;
            case 1: newItem.GetComponent<SpriteRenderer>().sprite = spritesTier1[Random.Range(0, spritesTier1.Length)]; break;
            case 2: newItem.GetComponent<SpriteRenderer>().sprite = spritesTier2[Random.Range(0, spritesTier2.Length)]; break;
            case 3: newItem.GetComponent<SpriteRenderer>().sprite = spritesTier3[Random.Range(0, spritesTier3.Length)]; break;
            case 4: newItem.GetComponent<SpriteRenderer>().sprite = spritesTier4[Random.Range(0, spritesTier4.Length)]; break;
            case 5: newItem.GetComponent<SpriteRenderer>().sprite = spritesTier5[Random.Range(0, spritesTier5.Length)]; break;

        
        }

        Bounds spawnBounds = bancadaCollider.bounds;
        Vector3 spawnTarget =new Vector2(Random.Range(-SpawnOffsetX, SpawnOffsetX), Random.Range(3, SpawnOffsetY) * -1);
        spawnTarget = spawnBounds.ClosestPoint(spawnTarget);
        tempMI.moveSpeed = Random.Range(0.1f, 1f);
        tempMI.SetMoveTarget(this.transform.position + spawnTarget);


        OnMerge();
    }

    void OnMerge()
    {
        foreach(FE_Behaviour beh in OnMergeBehaviours)
        {
            beh.OnActivate();
        }

        foreach(GameObject goItem in mItemsList)
        {
            GameObject.Destroy(goItem);
        }

        ItemValues.Clear();
    }

    public override void OnClicked()
    {
        base.OnClicked();

        if (mItems.Count > 0) ;
        Merge();
    }

    public void Expel()
    {



        OnExpel();
    }

    void OnExpel()
    {
        foreach(FE_Behaviour beh in OnExpelBehaviours)
        {
            beh.OnActivate();
        }
    }
}
