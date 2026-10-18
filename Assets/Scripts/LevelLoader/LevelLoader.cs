using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public Button buttonTransition;
    public bool story;

   
    public GameObject Cauldron, Entity;
    public bool creature;
    public Sprite CauldronSprite, CreatureSprite;
    public Transform outOfBoundsPosition;
    public Transform centerScreenPosition;

    public float transitionTime = 1f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Button btn = buttonTransition.GetComponent<Button>();
        //btn.onClick.AddListener(LoadNextLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && story==true)
        {
            LoadNextLevel();
        }
    }
    
    public void SwapScenePosition()
    {

        if (creature)
        {
            Entity.transform.position = outOfBoundsPosition.position;
            Entity.SetActive(false);

            Cauldron.transform.position = centerScreenPosition.position;
            Cauldron.SetActive(true);

            buttonTransition.image.sprite = CreatureSprite;

            creature = false;

        }
        else
        {

            Entity.transform.position = centerScreenPosition.position; ;
            Entity.SetActive(true);

            Cauldron.transform.position = outOfBoundsPosition.position;
            Cauldron.SetActive(false);

            buttonTransition.image.sprite = CauldronSprite;

            creature = true;

        }


    }

    public void PerformSwap()
    {
        StartCoroutine(Darken());
    }


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));

    }

    IEnumerator Darken()
    {

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        SwapScenePosition();



        transition.SetTrigger("End");

    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);
    }
}
