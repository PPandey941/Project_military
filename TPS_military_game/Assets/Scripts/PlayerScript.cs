using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;

    //[Header("Player Health Things")];

    [Header("Player script and Cameras")]
    public Transform playerCamera;


    [Header("Player Animator And Gravity")]
    public CharacterController cC;
    public float gravity = -9.81f;


    [Header("Player Jumping and Velocity")]
    public float jumpRange = 1f;
    Vector3 velocity;
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f
    public LayerMask surfaceMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if(onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);

        playerMove();

        Jump();
    }

    void playerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized; //stores the directions
        //horizontal_axis for x-axis, 0f for y-axis, vertical_axis for z-axis

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle= Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if(input.GetButtonDown("Jump") && onSurface)
        {
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);

        }
    }
}