using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[Serializable]
public struct Serializable_Bar_Argument
{
    public string key;

    [SerializeField()]    
    public UnityEngine.UI.Slider bar;
}


public class Entity : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Dictionary<string, float> Elements;
    public Dictionary<string, UnityEngine.UI.Slider> ProgressBars;
    public Dictionary<string, float> TargetValues;

    public Gradient ColorGradient;

    public Serializable_FE_ArgBehav[] serializableElements;

    public Serializable_FE_ArgBehav[] targetNumbers;

    [SerializeField()]
    public Serializable_Bar_Argument[] serializableBars;

    public UnityEngine.UI.Slider overloadBar;

    public GameObject[] onWinObjects;

    void Start()

    {
        Elements = new Dictionary<string, float>();
        ProgressBars = new Dictionary<string, UnityEngine.UI.Slider>();
        TargetValues = new Dictionary<string, float>(); 

        foreach (Serializable_FE_ArgBehav arg in serializableElements)
        {
            Elements.Add(arg.argument_name, arg.argument_value);
        }

        foreach (Serializable_FE_ArgBehav arg in targetNumbers)
        {
            TargetValues.Add(arg.argument_name, arg.argument_value);
        }

        foreach (Serializable_Bar_Argument sb in  serializableBars)
        {
            ProgressBars.Add(sb.key, sb.bar);
            ProgressBars[sb.key].value = Elements[sb.key];
            SetBarColor(sb.key);
            Debug.Log("Setei a bar" +sb.bar.name  + "com o valor" + Elements[sb.key]);
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnValueUpdated(string key)
    {
        ProgressBars[key].value = Elements[key];
        SetBarColor(key);
        CheckWin();
        Debug.Log("Alterei a bar" + ProgressBars[key].name + "com o valor" + Elements[key]);
    }


    public void UpdateValue(string key, float value)
    {
        if (Elements.ContainsKey(key))
        {
            float v;
            Elements.TryGetValue(key, out v);
            overloadBar.value += Mathf.Abs(value);
            v = v + value;
            Elements[key] = v;

            Debug.Log("Alterei o valor" + key + "pra" + v);

            OnValueUpdated(key);
        }

    }

    void SetBarColor(string key)
    {
        float targetValue = TargetValues[key];
        float sliderValue = ProgressBars[key].value;

        float offset = Mathf.Abs(targetValue - sliderValue);
         offset = offset / targetValue;
        Debug.Log("Offset ta em: " + offset);

        ProgressBars[key].fillRect.GetComponent<UnityEngine.UI.Image>().color = ColorGradient.Evaluate(offset);

    }

    void CheckWin()
    {
        bool won = false;

        foreach (KeyValuePair<string, float> element in Elements)
        {
            if (element.Value == TargetValues[element.Key])
            {
                won = true;
            }

            else
            {
                won = false;
                break;

            }
              
        }


        if(won)
        {
            foreach(GameObject go in onWinObjects)
            {
                go.SetActive(true);
            }
        }

        else if (onWinObjects[0].activeSelf)
        {
            foreach (GameObject go in onWinObjects)
            {
                go.SetActive(false);
            }
        }

    }

}
