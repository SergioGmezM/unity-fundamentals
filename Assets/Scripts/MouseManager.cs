using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    // Know what objects are clickable
    public LayerMask clickableLayer;

    // Swap cursors per objects
    public Texture2D pointer; // Normal pointer
    public Texture2D target; // Cursor for clickable objects like the world
    public Texture2D doorway; // Cursor for doorways
    public Texture2D combat; // Cursor for combat actions

    // Listens to the event and passes a Vector3 as a parameter
    public EventVector3 onClickEnvironment;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;

            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            // Moving the player
            if (Input.GetMouseButtonDown(1))
            {
                onClickEnvironment.Invoke(hit.point);
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

// Makes it available in the inspector
[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
