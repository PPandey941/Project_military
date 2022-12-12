using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;

    [Header("Player Animator And Gravity")]
    public CharacterController cC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
    }
    void playerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized; //stores the directions
        //horizontal_axis for x-axis, 0f for y-axis, vertical_axis for z-axis

        if (direction.magnitude>=0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            cC.Move(direction.normalized * playerSpeed * Time.deltaTime);
        }
    }
}