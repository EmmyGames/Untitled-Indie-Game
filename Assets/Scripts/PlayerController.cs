using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private float moveX;
    private float moveZ;
    private const float MOVE_SPEED = 10.0f;
    private const float SPRINT_SPEED = 20.0f;
    private const float ROTATE_SPEED = 180;
    public float speed;
    Vector3 lastPosition = Vector3.zero;
    float moveSpeed = 0.0f;
    Vector3 moveDirection = Vector3.zero;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MovePlayer();
        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void MovePlayer()
    {
        if (Input.GetButton("Sprint") && moveZ >= 0.0f)
        {
            speed = SPRINT_SPEED;
        }
        else if(moveZ < 0.0f)
        {
            speed = MOVE_SPEED * 0.5f;
        }
        else
        {
            speed = MOVE_SPEED;
        }
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        moveDirection = transform.forward * moveZ * speed * Time.deltaTime;
        transform.Translate(moveDirection, Space.World);
        transform.Rotate(0.0f, moveX * ROTATE_SPEED * Time.deltaTime, 0.0f);
    }

    void UpdateAnimation()
    {
        //Get speed of sheep
        float movementPerFrame = Vector3.Distance(lastPosition, transform.position);
        moveSpeed = movementPerFrame / Time.deltaTime;
        lastPosition = transform.position;
        //Set speed to animation variable
        anim.SetFloat("speed", moveSpeed);
    }
}
