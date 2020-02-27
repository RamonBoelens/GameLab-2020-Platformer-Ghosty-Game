using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    private float horizontalMovement = 0f;
    private bool jump = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            controller.Dash();
        }
    }

    private void FixedUpdate()
    {
        // Move the character
        controller.Move(horizontalMovement * Time.fixedDeltaTime, jump);
        jump = false;
    }
}
