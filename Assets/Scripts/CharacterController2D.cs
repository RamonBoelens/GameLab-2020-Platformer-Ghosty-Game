/*
 * Based of a script from brackeys and modified
 * to suits our needs.
 * 
 * https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs
 */


using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
                    [SerializeField] private float     jumpForce = 700f;         // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float     movementSmoothing = .05f; // How much to smooth out the movement.
                    [SerializeField] private bool      airControl = false;       // Determines if the player can move while jumping.
                    [SerializeField] private LayerMask whatIsGround;             // A mask determining what layers is ground to the character.
                    [SerializeField] private Transform groundCheck;              // A position marker to check if the player is grounded.

    const float groundedRadius = .2f;        // Radius of the overlap circle to determine if grounded.
    private bool grounded;                   // Whether or not the player is grounded.
    private Rigidbody2D rigidbody2D;
    private bool facingRight = true;         // For determining which way the player is facing.
    private Vector3 velocity = Vector3.zero;

    [Header("Events")]
    [Space]
    public UnityEvent onLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        if (onLandEvent == null)
            onLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        // The player is grounded if the circlecast to the groundcheck position hits anything designated as ground.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            grounded = true;
            if (!wasGrounded)
                onLandEvent.Invoke();
        }
    }

    public void Move(float move, bool jump)
    {
        // Only control the player if grounded or airControl is turned on
        if (grounded || airControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rigidbody2D.velocity.y);
            // And then smoothing the movement out and applying it to the character
            rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            // If the input is moving the player right and the player is facing left..
            if (move > 0 && !facingRight)
            {
                // .. then flip the player.
                Flip();
            }

            // If the input is moving the player left and the player is facing right..
            if (move < 0 && facingRight)
            {
                // .. then flip the player.
                Flip();
            }
        }

        // If the player jumps
        if (grounded && jump)
        {
            // Add a vertical force to the player.
            grounded = false;
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}