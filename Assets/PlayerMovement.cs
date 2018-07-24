using System.Collections;
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

   
    void Start()
    {
        anim = GetComponent<Animator>();
    }
	// Update is called once per frame

	void Update () {


        anim.SetBool("isRunning", isRunning); 
        
        
        // Get input from player
        HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // Returns a value based on user input for horizontal movement e.g -1 = Left Arrow, 1 = Right Arrow, 0 = null

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


       // if(Input.GetButtonDown("Crouch")) <-- Removed as crouch functions not yet needed
       // {
        //    playercrouch = true;
      //  } else if(Input.GetButtonUp("Crouch"))
      //  {
      //      playercrouch = false;
     //   }
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
