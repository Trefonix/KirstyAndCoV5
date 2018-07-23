using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D Controller; // References the CharacterController2D Script located in player

    float HorizontalMove = 0f; // Variable to store the returned value from "Input.GetAxisRaw" which represents user input for horizontal movement

    public float runSpeed = 40f; // Variable to store the speed at which the character will move in-game


	// Update is called once per frame
	void Update () {
        // Get input from player
        HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // Returns a value based on user input for horizontal movement e.g -1 = Left Arrow, 1 = Right Arrow, 0 = null

	}

    // FixedUpdate is called a fixed number of times per second
    private void FixedUpdate()
    {
        // Move the character
        Controller.Move(HorizontalMove * Time.fixedDeltaTime, false, false); // Move the character based on input from the "Input.GetAxisRaw" for horizontal axis. False for crouching, False for jumping.
        //Time.fixedDeltaTime references the time since the subroutine was last called to allow for consistent speed across platforms

    }
}
