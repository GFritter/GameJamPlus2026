using UnityEngine;
using Unity.Collections;
using System;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "FE_BehaviourArgs", menuName = "Scriptable Objects/FE_BehaviourArgs")]
public class FE_BehaviourArgs : ScriptableObject
{
    [SerializeField()]
    string Alias;

    [SerializeField()]
    public Dictionary<string , float> Arguments;

    [SerializeField()]
   public string[] ArgsKeys;

    [SerializeField()]
   public  float[] ArgsValues;
}
