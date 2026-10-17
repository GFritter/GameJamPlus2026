using UnityEngine;
using UnityEngine.InputSystem;

public class CursorBehaviour : MonoBehaviour
{

    public Sprite handOpen, handClose;

    public SpriteRenderer spriteRenderer;

    bool close;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // spriteRenderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        mousePos.z = 0;
        this.gameObject.transform.position = mousePos;

        if(Mouse.current.leftButton.isPressed && !close)
        {
            close = true;
            spriteRenderer.sprite = handClose;
        }

        else if (!Mouse.current.leftButton.isPressed && close)
        {
            close = false;
            spriteRenderer.sprite = handOpen;
        }
    }

    private void OnMouseDown()
    {
        spriteRenderer.sprite = handClose;
    }

    private void OnMouseUp()
    {
        spriteRenderer.sprite = handOpen;
    }


}
