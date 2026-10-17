using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]

public class MouseInteractable : MonoBehaviour
{
    public bool Hoverable, Clickable, Draggable;

    public FE_Behaviour[] ClickBehaviours;
    public FE_Behaviour[] HoverBehaviours;
    public FE_Behaviour[] UnHoverBehaviours;

    public FE_Behaviour[] ReleaseBehaviours;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for(int i=0;i<ClickBehaviours.Length;++i)
        {
            ClickBehaviours[i] = Instantiate(ClickBehaviours[i]);
            ClickBehaviours[i].Init(this.gameObject);
        }

        for (int i = 0; i < HoverBehaviours.Length; ++i)
        {
            HoverBehaviours[i] = Instantiate(HoverBehaviours[i]);
            HoverBehaviours[i].Init(this.gameObject);
        }

        for (int i = 0; i < UnHoverBehaviours.Length; ++i)
        {
            UnHoverBehaviours[i] = Instantiate(UnHoverBehaviours[i]);
            UnHoverBehaviours[i].Init(this.gameObject);
        }

        for (int i = 0; i < ReleaseBehaviours.Length; ++i)
        {
            ReleaseBehaviours[i] = Instantiate(ReleaseBehaviours[i]);
            ReleaseBehaviours[i].Init(this.gameObject);
        }


        //foreach (FE_Behaviour Beh in ClickBehaviours)
        //{
        //    FE_Behaviour temp = Beh ;


        //    Beh.Init(this.gameObject);
        //}

        //foreach (FE_Behaviour Beh in HoverBehaviours)
        //{
        //    Beh.Init(this.gameObject);
        //}

        //foreach (FE_Behaviour Beh in UnHoverBehaviours)
        //{
        //    Beh.Init(this.gameObject);
        //}

        //foreach (FE_Behaviour Beh in ReleaseBehaviours)
        //{
        //    Beh.Init(this.gameObject);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHover()
    {
        foreach (FE_Behaviour Beh in HoverBehaviours)
        {
            Beh.OnActivate();
        }
    }

    public void OnHoverEnd()
    {
        foreach (FE_Behaviour Beh in UnHoverBehaviours)
        {
            Beh.OnActivate();
        }
    }

    public void OnClicked()
    {
        foreach(FE_Behaviour Beh in ClickBehaviours)
        {
            Beh.OnActivate();
        }
    }

    public void OnRelease()
    {
        Debug.Log("On Release Trigger");

        foreach (FE_Behaviour Beh in ReleaseBehaviours)
        {
            Beh.OnActivate();
        }
    }




    private void OnMouseEnter()
    {
        OnHover();
    }

    private void OnMouseExit()
    {
        OnHoverEnd();
    }

    private void OnMouseDown()
    {
        OnClicked();
    }

    private void OnMouseUpAsButton()
    {
        OnRelease();
    }

    private void OnMouseDrag()
    {
        if(Draggable)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            mousePos.z = 0;
            this.gameObject.transform.position = mousePos;
        }
        
    }
}
