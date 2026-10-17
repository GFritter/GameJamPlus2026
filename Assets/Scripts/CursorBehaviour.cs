using UnityEngine;
using UnityEngine.InputSystem;

public class CursorBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        mousePos.z = 0;
        this.gameObject.transform.position = mousePos;
    }
}
