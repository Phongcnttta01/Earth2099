using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormal;
    [SerializeField] private Texture2D cursorShoot;
    [SerializeField] private Texture2D cursorReload;
    private Vector2 hotPos;
    void Start()
    {
        Cursor.SetCursor(cursorNormal, hotPos, CursorMode.Auto);
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) Cursor.SetCursor(cursorShoot, hotPos, CursorMode.Auto);
        else if(Input.GetMouseButtonUp(0)) Cursor.SetCursor(cursorNormal, hotPos, CursorMode.Auto);
        
        if(Input.GetMouseButtonDown(1)) Cursor.SetCursor(cursorReload, hotPos, CursorMode.Auto);
        else if(Input.GetMouseButtonUp(1)) Cursor.SetCursor(cursorNormal, hotPos, CursorMode.Auto);
    }
}
