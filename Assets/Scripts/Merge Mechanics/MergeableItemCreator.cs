using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class MergeableItemCreator : MonoBehaviour
{

    public int minItemsSpawn, maxItemsSpawn;

    public GameObject[] levelSpawnablePrefabs;

    public FloatRangeParameter itemValueModifierRange;

    public FE_Behaviour[] HoverBehaviours;
    public FE_Behaviour[] UnHoverBehaviours;

    public FE_Behaviour[] OnPreCreateBehaviours, OnPostCreateBehaviours;

    public Collider2D spawnTarget;
 

    public int spawnCost;

    public int initialItemsSpawned;

    public Entity barHolder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(var beh in OnPreCreateBehaviours)
        {
            beh.Init(this.gameObject);
        }
        foreach (var beh in OnPostCreateBehaviours)
        {
            beh.Init(this.gameObject);
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


        StartCoroutine(InitialDrop());
      

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator InitialDrop()
    {
        yield return new  WaitForSecondsRealtime(3f);

        SpawnItems(initialItemsSpawned);
    }

    public void SpawnItems()
    {
        OnPreCreate();

        int itemsToCreate = Random.Range(minItemsSpawn, maxItemsSpawn);

        for(int i=0;i<itemsToCreate;++i)
        {
            int itemType = Random.Range(0, levelSpawnablePrefabs.Length);
            GameObject tempItem = Instantiate(levelSpawnablePrefabs[itemType]);

            MergeableItem mItem = tempItem.GetComponent<MergeableItem>();
            
          for(int j=0;j<mItem.itemValues.Length;++j)
            {
                float mod = Random.Range(itemValueModifierRange.value.x, itemValueModifierRange.value.y);
                Debug.Log("O modifier foi:" + mod);
                mItem.itemValues[j].argument_value *= mod;

            }

        }


    }

    public void SpawnItems(int numItems)
    {
        OnPreCreate();

        int itemsToCreate =numItems;

        for (int i = 0; i < itemsToCreate; ++i)
        {
            int itemType = Random.Range(0, levelSpawnablePrefabs.Length);
            GameObject tempItem = Instantiate(levelSpawnablePrefabs[itemType],this.transform.position,this.transform.rotation);

            MergeableItem mItem = tempItem.GetComponent<MergeableItem>();

            for (int j = 0; j < mItem.itemValues.Length; ++j)
            {
                float mod = Random.Range(itemValueModifierRange.value.x, itemValueModifierRange.value.y);
                Debug.Log("O modifier foi:" + mod);
                mItem.itemValues[j].argument_value *= mod;
            }

        }

        OnPostCreate();

    }

    void OnPreCreate()
    {
        foreach(var beh in OnPreCreateBehaviours)
        {
            beh.OnActivate();
        }
    }

    void OnPostCreate()
    {
        foreach (var beh in OnPostCreateBehaviours)
        {
            beh.OnActivate();
        }
    }

    private void OnMouseUpAsButton()
    {
        if(barHolder.overloadBar.value+spawnCost <= barHolder.overloadBar.maxValue)
        {
            barHolder.IncrementOverloadBar(spawnCost);
            SpawnItems();
        }

       
    }

    private void OnMouseEnter()
    {
        OnHover();
    }

    private void OnMouseExit()
    {
        OnHoverEnd();
    }

    public virtual void OnHover()
    {
        foreach (FE_Behaviour Beh in HoverBehaviours)
        {
            Beh.OnActivate();
        }
    }

    public virtual void OnHoverEnd()
    {
        foreach (FE_Behaviour Beh in UnHoverBehaviours)
        {
            Beh.OnActivate();
        }
    }


}
