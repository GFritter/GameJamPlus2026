using UnityEngine;

public class BehaviourTest : MonoBehaviour
{

    public FE_Behaviour test;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        test.Init(this.gameObject);
        test.OnActivate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
