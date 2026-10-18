using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Serializable_FE_ArgBehav
{
    public string argument_name;
    public float argument_value;


}

[CreateAssetMenu(fileName = "FE_Behaviour", menuName = "Scriptable Objects/FE_Behaviour")]
public class FE_Behaviour : ScriptableObject,ISerializationCallbackReceiver
{
    [SerializeField()]
    public FE_BehaviourArgs externalArgs;

    [SerializeField()]
    public GameObject Owner;

    public Dictionary<string, float> Arguments;

    [SerializeField()]
    public bool ongoing;

    [SerializeField()]
    public bool shouldEnd;

    [SerializeField()]
    public bool active;

    public List<Serializable_FE_ArgBehav> serializableNodes;

    public void OnBeforeSerialize()
    {

        if (serializableNodes == null)
        {
            serializableNodes = new List<Serializable_FE_ArgBehav>();

        }

    }

    public void OnAfterDeserialize()
    {

    }

    void OnGUI()
    {
        foreach(Serializable_FE_ArgBehav sarg in serializableNodes)
        {
            Display(sarg);
        }

    }

    void Display(Serializable_FE_ArgBehav seArg)
    {
        GUILayout.Label("Arguments:");
        seArg.argument_name = GUILayout.TextField(seArg.argument_name, GUILayout.Width(100));
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.BeginVertical();

        if(GUILayout.Button("Add argument"))
        {
            serializableNodes.Add(new Serializable_FE_ArgBehav());
        }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

    }




    public virtual void Init(GameObject owner)
    {
        Owner = owner;
        Arguments = new Dictionary<string, float>();

        foreach(Serializable_FE_ArgBehav arg in serializableNodes)
        {
            Arguments.Add(arg.argument_name, arg.argument_value); 
        }
    }

    public virtual void OnActivate()
    {
        foreach( string s in Arguments.Keys)
        {
            Debug.Log(s);
            
            
        }
    }

    public void OnDeactivate()
    { 

    }

    public void OnOngoing()
    {

    }

}
