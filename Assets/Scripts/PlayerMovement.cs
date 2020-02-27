using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    private float horizontalMovement = 0f;
    private bool jump = false;

    public TextMeshProUGUI txt_DoubleJump, txt_AirDash, txt_WallClimbing;
    private int activeAbility;

    private void Start()
    {
        UpdateCanvas();
    }

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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            controller.SetActiveAbility(1);
            activeAbility = 1;
            UpdateCanvas();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            controller.SetActiveAbility(2);
            activeAbility = 2;
            UpdateCanvas();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            controller.SetActiveAbility(3);
            activeAbility = 3;
            UpdateCanvas();
        }
    }

    private void FixedUpdate()
    {
        // Move the character
        controller.Move(horizontalMovement * Time.fixedDeltaTime, jump);
        jump = false;
    }

    private void UpdateCanvas()
    {
        if (activeAbility == 1)
        {
            txt_DoubleJump.color = Color.white;
            txt_AirDash.color = Color.black;
            txt_WallClimbing.color = Color.black;
        }
        else if (activeAbility == 2)
        {
            txt_DoubleJump.color = Color.black;
            txt_AirDash.color = Color.white;
            txt_WallClimbing.color = Color.black;
        }
        else if (activeAbility == 3)
        {
            txt_DoubleJump.color = Color.black;
            txt_AirDash.color = Color.black;
            txt_WallClimbing.color = Color.white;
        }
        else
        {
            txt_DoubleJump.color = Color.black;
            txt_AirDash.color = Color.black;
            txt_WallClimbing.color = Color.black;
        }
    }
}
