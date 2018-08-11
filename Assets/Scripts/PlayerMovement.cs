﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D Controller; // References the CharacterController2D Script located in player
    float HorizontalMove = 0f; // Variable to store the returned value from "Input.GetAxisRaw" which represents user input for horizontal movement
    public float runSpeed = 40f; // Variable to store the speed at which the character will move in-game
    private Animator anim;
    public bool playerjump = false; // Variable to store if the player has started a jump
    public bool playercrouch = false; // Variable to store if the player is currently crouching
    public bool isRunning; // Is the character running?
    public Vector3 SpawnPosition; // Variable to store the starting position of the character
    [SerializeField] private bool m_EnableSprint; 
    [Range(1, 10)] [SerializeField] private float m_SprintDelay; // Variable to store the duration the sprint delay
    private bool currently_sprinting = false; // Variable to store if the character is currently sprinting
    private float original_speed; // Variable to store the original speed of the character
    private float sprint_timer = 0; // Variable to store the time that the sprint has been in action
    private float sprint_cooldown = 0; //
    private bool on_cooldown = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        SpawnPosition = transform.position; // Save the initial position of the character
        original_speed = runSpeed;

    }
    // Update is called once per frame
    void Update() {
        anim.SetBool("isRunning", isRunning);       
        // Get input from player
        HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // Returns a value based on user input for horizontal movement e.g -1 = Left Arrow, 1 = Right Arrow, 0 = null

        // Sprinting code
       
       
        if(Input.GetKeyUp(KeyCode.RightControl)) // Check if the Right Control key is up to disable sprint
        {
            currently_sprinting = false;
            if (!on_cooldown)
            {
                on_cooldown = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightControl) && isRunning && !currently_sprinting && m_EnableSprint && !on_cooldown)  
        {
            currently_sprinting = true;
            runSpeed *= 1.5f;
        }
        
        if(currently_sprinting)
        {
            if(sprint_timer > 2)
            {
                currently_sprinting = false;
                on_cooldown = true;
            }
            sprint_timer += Time.deltaTime;
        }
        else
        {
            sprint_timer = 0;
        }
        if(!currently_sprinting)
        {
            runSpeed = original_speed;
            sprint_timer = 0;
        }
        if(on_cooldown)
        {
            sprint_cooldown += Time.deltaTime;
            if(sprint_cooldown > m_SprintDelay)
            {
                on_cooldown = false;
                sprint_cooldown = 0;
            }
        }

        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0) // Tell the animator that the character is running
        {
            isRunning = true;          
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetButtonDown("Jump"))  // Returns a value based on if the user has pressed the button bound to 'Jump'
        {
            playerjump = true; // Tells fixedupdate that the player wishes to jump
        }
        if(transform.position.y < -15) // If the player is out of bounds
        {
            transform.position = SpawnPosition; // Put them back to their initial position
           
        }
	}

    // FixedUpdate is called a fixed number of times per second
    private void FixedUpdate()
    {
        // Move the character
        Controller.Move(HorizontalMove * Time.fixedDeltaTime, false, playerjump); // Move the character based on input from the "Input.GetAxisRaw" for horizontal axis. Start crouching or jumping if applicable
        //Time.fixedDeltaTime references the time since the subroutine was last called to allow for consistent speed across platforms
        playerjump = false; // Automatically reset the value of 'playerjump' to prevent continuous jumping


    }
}
