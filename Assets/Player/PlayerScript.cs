using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public PlayerCollision playerCollision;
    public PlayerAnimation playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Make sure nothing else goes below this
        if (!Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
