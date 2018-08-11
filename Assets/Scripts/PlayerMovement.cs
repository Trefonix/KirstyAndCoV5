using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D Controller; // References the CharacterController2D Script located in player
    private float HorizontalMove = 0f; // Variable to store the returned value from "Input.GetAxisRaw" which represents user input for horizontal movement
    public float runSpeed = 40f; // Variable to store the speed at which the character will move in-game
    private Animator anim; // Variable to refrence the animator component
    public bool playerjump = false; // Variable to store if the player has started a jump
    public bool playercrouch = false; // Variable to store if the player is currently crouching
    public bool isRunning; // Is the character running?
    private Vector3 SpawnPosition; // Variable to store the starting position of the character
    [SerializeField] private bool m_EnableSprint; // Variable to store if the sprint is enabled
    [Range(1, 10)] [SerializeField] private float m_SprintDelay; // Variable to store the duration the sprint delay
    private bool currently_sprinting = false; // Variable to store if the character is currently sprinting
    private float original_speed; // Variable to store the original speed of the character
<<<<<<< HEAD
    private float sprint_timer = 0; // Variable to store the time that the sprint has been in action
    private float sprint_cooldown = 0; //
    private bool on_cooldown = false;
=======
    private float sprint_timer = 0; // Variable to store the time that the character is able to sprint
    private float sprint_cooldown = 0; // Variable to store how long the cooldown has been in effect
    private bool on_cooldown = false; // Variable to store if the character is currently on a cooldown
    [Range(1,10)][SerializeField] private float m_SprintModifier; // Variable to change the modifier for the sprint
    [Range(1,10)][SerializeField] private float m_SprintTime; // Variable to determine how long the sprint will last 
    public bool is_gliding = false;

>>>>>>> d73a5f9096d084c64fff08440622be4b67e369ca

    void Start()
    {
        anim = GetComponent<Animator>();
        SpawnPosition = transform.position; // Save the initial position of the character
        original_speed = runSpeed; // Save the initial speed of the character

    }
    // Update is called once per frame
    void Update() {
        anim.SetBool("isRunning", isRunning);       
        // Get input from player
        HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // Returns a value based on user input for horizontal movement e.g -1 = Left Arrow, 1 = Right Arrow, 0 = null

        // Sprinting code
        if(Input.GetKeyUp(KeyCode.RightShift)) // Check if the Right Control key is up to disable sprint
        {
            currently_sprinting = false;
            if (!on_cooldown) 
            {
                // If the character is not on cooldown already, it should be, hence enable cooldown
                on_cooldown = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightShift) && isRunning && !currently_sprinting && m_EnableSprint && !on_cooldown)  
        {
            currently_sprinting = true;
            runSpeed *= m_SprintModifier; // Increase the speed of the character by a factor of 1.5x
        }

        if(currently_sprinting)
        {
            if(sprint_timer > m_SprintTime) // Has the character been sprinting for over 2 seconds already?
            {
                // Disable the sprint and enable the cooldown
                currently_sprinting = false;
                on_cooldown = true;
            }
            sprint_timer += Time.deltaTime; // Increment the timer
        }
        else
        {
            sprint_timer = 0; // If the character is not sprinting, the sprint timer must be 0
        }

        if(!currently_sprinting) 
        {
            // Reset the character to status before the sprint was enabled
            runSpeed = original_speed;
            sprint_timer = 0;
        }

        if(on_cooldown)
        {
            sprint_cooldown += Time.deltaTime; // Increment the timer
            if(sprint_cooldown > m_SprintDelay) // Has the cooldown exceeded the allocated delay?
            {
                on_cooldown = false; // The cooldown is no longer in effect.
                sprint_cooldown = 0;
            }
        }
        
        // Running code
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0) // Tell the animator that the character is running
        {
            isRunning = true;          
        }
        else
        {
            isRunning = false;
        }

        // Jumping code
        if (Input.GetButtonDown("Jump"))  // Returns a value based on if the user has pressed the button bound to 'Jump'
        {
            playerjump = true; // Tells fixedupdate that the player wishes to jump
        }

        // Gliding code
        if(Input.GetAxisRaw("Vertical") == -1)
        {
            is_gliding = true;
        } else 
        {
            is_gliding = false;
        }
        

        // Player re-position code
        if(transform.position.y < -15) // If the player is out of bounds
        {
            transform.position = SpawnPosition; // Put them back to their initial position
           
        }
	}

    // FixedUpdate is called a fixed number of times per second
    private void FixedUpdate()
    {
        // Move the character
        Controller.Move(HorizontalMove * Time.fixedDeltaTime, false, playerjump, is_gliding); // Move the character based on input from the "Input.GetAxisRaw" for horizontal axis. Start crouching or jumping if applicable
        //Time.fixedDeltaTime references the time since the subroutine was last called to allow for consistent speed across platforms
        playerjump = false; // Automatically reset the value of 'playerjump' to prevent continuous jumping
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Output the Collider's GameObject's name
        if(collision.collider.tag == "Wall_Jump") 
        {
               
        }
        
    }


}
