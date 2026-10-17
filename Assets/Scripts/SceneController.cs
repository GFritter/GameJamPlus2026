using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Light2D globalLight;
    public float darkSpeed;
    public string nextSceneName;
    public bool quit, start, shouldDark;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldDark)
        {
            Darken();
        }
    }

    public void QuitPressed()
    {
        quit = true;
        shouldDark = true;

    }

    public void StartPressed()
    {
        start = true;
        shouldDark = true;

    }

    void Darken()
    {
        globalLight.intensity = Mathf.Lerp(globalLight.intensity, 0, darkSpeed * Time.deltaTime);

        if(globalLight.intensity<=0.01f)
        {
            if(quit)
            {
                Application.Quit(0);
            }

            else
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

}
