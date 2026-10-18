using UnityEngine;
using UnityEngine.Rendering;

public class MergeableItemCreator : MonoBehaviour
{

    public int minItemsSpawn, maxItemsSpawn;

    public GameObject[] levelSpawnablePrefabs;

    public FloatRangeParameter itemValueModifierRange;

    public FE_Behaviour[] OnPreCreateBehaviours, OnPostCreateBehaviours;

    public Collider2D spawnTarget;

    public int spawnCost;

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

    }

    // Update is called once per frame
    void Update()
    {
        
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
}
