using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerScript playerScript;
    private Movement _movement;
    public Transform cam;
    
    //Movement
    private Vector3 _direction;
    private Vector3 _lastDirection;
    private float _startCamRotation;
    
    public float groundDistance;
    public Transform groundCheck;
    public LayerMask groundMask;
    public bool isGrounded;
    
    // Start is called before the first frame update
    private void Start()
    {
        _movement = GetComponent<Movement>();
        _startCamRotation = cam.eulerAngles.y;
    }

    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        _direction = new Vector3(playerScript.playerInput.moveX, 0f, playerScript.playerInput.moveZ).normalized;
        _movement.EntityMovement(isGrounded, playerScript.playerInput.isSprinting, playerScript.playerInput.isJumping, cam, _startCamRotation, _direction);
    }
}
