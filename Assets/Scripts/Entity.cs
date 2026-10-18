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
    public TwinnedBarEffect bar;

    public Color argumentColorEffect;
}


public class Entity : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Dictionary<string, float> Elements;
    public Dictionary<string, TwinnedBarEffect> ProgressBars;
    public Dictionary<string, float> TargetValues;

    public float WinThreshold;

    public Gradient ColorGradient;

    public Serializable_FE_ArgBehav[] serializableElements;

    public Serializable_FE_ArgBehav[] targetNumbers;

    [SerializeField()]
    public Serializable_Bar_Argument[] serializableBars;

    public UnityEngine.UI.Slider overloadBar;
    
    public GameObject[] onWinObjects;
    public List<GameObject> handleObjects;
    public GameObject barMarker;

    public int overloadBarLives = 3;
    public GameObject[] OverloadRunes = new GameObject[3];


    public FE_Behaviour[] OnOverloadBehaviours;
 

    void Start()

    {
        Elements = new Dictionary<string, float>();
        ProgressBars = new Dictionary<string, TwinnedBarEffect>();
        TargetValues = new Dictionary<string, float>();
        handleObjects = new List<GameObject>(); 

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
            
            ProgressBars[sb.key].underBar.value = TargetValues[sb.key];
            GameObject handleCopy = Instantiate(barMarker, ProgressBars[sb.key].underBar.handleRect.gameObject.transform.position, ProgressBars[sb.key].underBar.handleRect.gameObject.transform.rotation, ProgressBars[sb.key].transform);
           
            handleObjects.Add(handleCopy);
            ProgressBars[sb.key].underBar.interactable = false;
            ProgressBars[sb.key].overBar.interactable = false;
            ProgressBars[sb.key].underBar.handleRect.gameObject.SetActive(false);
            ProgressBars[sb.key].overBar.handleRect.gameObject.SetActive(false);

            ProgressBars[sb.key].underBar.value = Elements[sb.key];
            ProgressBars[sb.key].overBar.value = Elements[sb.key];
            SetBarColor(sb.key);
            
            Debug.Log("Setei a bar" +sb.bar.name  + "com o valor" + Elements[sb.key]);
        }

        foreach (FE_Behaviour beh in OnOverloadBehaviours)
        {
            beh.Init(this.gameObject);
        }


        overloadBar.interactable = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnValueUpdated(string key)
    {
        ProgressBars[key].IncrementValue( Elements[key]);
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
            IncrementOverloadBar(Mathf.Abs(value));
            v = v + value;
            Elements[key] = v;

            // Debug.Log("Alterei o valor" + key + "pra" + v);

            OnValueUpdated(key);
        }

    }

    public void UpdateValueNoOverload(string key, float value)
    {
        if (Elements.ContainsKey(key))
        {
            float v;
            Elements.TryGetValue(key, out v);
          
            v = v + value;
            Elements[key] = v;

            // Debug.Log("Alterei o valor" + key + "pra" + v);

            OnValueUpdated(key);
        }

    }

    public void IncrementOverloadBar(float value)
    {
        overloadBar.value += value;
        CheckBarOverload();
    }

    void CheckBarOverload()
    {
        if (overloadBar.value >= overloadBar.maxValue)
        {
            if(overloadBarLives>0)
            {
                Debug.Log("Triggei Overload");
                TriggerOverload();
            }

            else
            {
                TriggerGameOver();
            }
        }
    }

    void TriggerOverload()
    {
        Animator anim;
        if(OverloadRunes[overloadBarLives - 1].TryGetComponent<Animator>(out anim))
        {
            anim.SetTrigger("destroy");
        }


        OverloadRunes[overloadBarLives - 1].SetActive(false);

        //spawnar alguma particula alguma coisa
        foreach (FE_Behaviour beh in OnOverloadBehaviours)
        {
            beh.OnActivate();
        }


        foreach(var bar in ProgressBars )
        {
            float value = bar.Value.underBar.value * (0.2f * (Mathf.Abs(overloadBarLives-4)));
            float offset = bar.Value.underBar.value - value;
            UpdateValueNoOverload(bar.Key,-offset);
        }

        overloadBar.value = 0;
        overloadBarLives --;

    }

    void TriggerGameOver()
    {
        Debug.Log("Triggei Game Over");
    }


    void SetBarColor(string key)
    {
        float targetValue = TargetValues[key];
        float sliderValue = ProgressBars[key].underBar.value;

        float offset = Mathf.Abs(targetValue - sliderValue);
         offset = offset / targetValue;
        Debug.Log("Offset ta em: " + offset);

        ProgressBars[key].ChangeOverBarColor( ColorGradient.Evaluate(offset));

    }

    void CheckWin()
    {
        bool won = false;

        foreach (KeyValuePair<string, float> element in Elements)
        {
            if (Mathf.Abs(element.Value - TargetValues[element.Key])<=WinThreshold)
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
