using UnityEngine;

public class ManagedVisualAids : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject managedGameObject;
    public FE_Behaviour managingBehaviour;

    void Start()

    {
        managingBehaviour = Instantiate(managingBehaviour);
        managingBehaviour.Init(managedGameObject);
    }

    private void Update()
    {
        if(managingBehaviour.ongoing)
        {
            managingBehaviour.OnOngoing();
        }
    }

    public void TriggerManagedBehaviour()
    {
        managingBehaviour.OnActivate();
    }
}
