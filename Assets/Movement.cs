using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float walkSpeed = 8f;
    public float sprintSpeed = 14f;
    public float maxVelocityChange = 10f;
    [Space]
    public float airControl = 0.5f;

    [Space]
    public float jumpHeight = 30f;


    private Vector2 input;
    private Rigidbody rb;
    private bool sprinting;

    private bool jumping;
    private bool grounded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Fixed method name and capitalized "Horizontal"
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        sprinting = Input.GetButton( "Sprint" );
        jumping = Input.GetButton ("Jump");

    }
    private void OnTriggerStay(Collider other){
        grounded = true;
    }
    // void FixedUpdate()
    // {
    //     if(grounded){
    //         if(jumping){
    //             rb.velocity = new Vector3(rb.velocity.x,jumpHeight,rb.velocity.z);
    //         }
    //         else if(input.magnitude > 0.5f){
    //         rb.AddForce(CalculateMovement(sprinting? sprintSpeed:walkSpeed),ForceMode.VelocityChange);
    //     }
    //     else{
    //         var velocity1 = rb.velocity;
    //         velocity1 =new Vector3(velocity1.x*0.2f*Time.fixedDeltaTime,velocity1.y,velocity1.z*0.2f*Time.fixedDeltaTime);
    //         rb.velocity = velocity1;
    //     }
    //     }
    //     else{
    //         if(input.magnitude > 0.5f){
    //         rb.AddForce(CalculateMovement(sprinting? sprintSpeed*airControl:walkSpeed*airControl),ForceMode.VelocityChange);
    //     }
    //     else{
    //         var velocity1 = rb.velocity;
    //         velocity1 =new Vector3(velocity1.x*0.2f*Time.fixedDeltaTime,velocity1.y,velocity1.z*0.2f*Time.fixedDeltaTime);
    //         rb.velocity = velocity1;
    //     }
    //     }

    //     grounded = false;   
        
    
    // }
    void FixedUpdate()
{
    if(grounded){
        if(jumping){
            // Calculate the force needed to achieve the desired jump height
            float g = Physics.gravity.y; // Gravity is negative in Unity
            float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(g) * jumpHeight);
            Vector3 jumpVector = new Vector3(0, jumpVelocity, 0);

            // Apply the calculated force to achieve the desired jump height
            rb.AddForce(jumpVector, ForceMode.VelocityChange);
        }
        else if(input.magnitude > 0.5f){
            rb.AddForce(CalculateMovement(sprinting ? sprintSpeed : walkSpeed), ForceMode.VelocityChange);
        }
        else {
            ApplyAirResistance();
        }
    }
    else{
        if(input.magnitude > 0.5f){
            rb.AddForce(CalculateMovement(sprinting ? sprintSpeed * airControl : walkSpeed * airControl), ForceMode.VelocityChange);
        }
        else{
            ApplyAirResistance();
        }
    }

    grounded = false;
}

void ApplyAirResistance() {
    var velocity = rb.velocity;
    velocity = new Vector3(velocity.x * 0.2f * Time.fixedDeltaTime, velocity.y, velocity.z * 0.2f * Time.fixedDeltaTime);
    rb.velocity = velocity;
}


    Vector3 CalculateMovement(float _speed)
    {
        // Fixed "vector3" to "Vector3" - capitalization matters in C#
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity) * _speed;

        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = targetVelocity - velocity;
        
        // Clamp the X and Z velocity change, not separate properties. Also fixed Mathf capitalization.
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

        // We do not change the Y velocity as it's typically used for gravity
        velocityChange.y = 0;

        return velocityChange;
    }
}
