using UnityEngine;
using UnityEngine.UI;

public class TwinnedBarEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Slider underBar, overBar;
    public float incrementSpeed;
    public Color underbarColor, overbarColor;

    public UnityEngine.UI.Image underBarBackgroundEffect;

    bool isIncrementing;

    void Start()
    {
        underBar.fillRect.GetComponent<Image>().color= underbarColor;
        overBar.fillRect.GetComponent<Image>().color = overbarColor;
        underBarBackgroundEffect.color = underbarColor;

    }

    // Update is called once per frame
    void Update()
    {
        if (isIncrementing)
        {
            if(overBar.value<underBar.value)
            {
                overBar.value += incrementSpeed * Time.deltaTime;
            }

            else
            {
                overBar.value = underBar.value;
                isIncrementing = false;

            }
        }
        
    }

    public void SetToIncrement()
    {
        isIncrementing = true;
    }

    public void IncrementValue(float value)
    {
        underBar.value = value;
        isIncrementing = true;
    }

    public void ChangeOverBarColor(Color newColor)
    {
        overBar.fillRect.GetComponent<Image>().color = newColor;
    }

}
