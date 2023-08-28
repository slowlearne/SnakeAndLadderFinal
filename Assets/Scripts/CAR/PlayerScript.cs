using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce;
    public float sideForce;
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(0, 0, forwardForce * Time.fixedDeltaTime);
        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -forwardForce * Time.fixedDeltaTime); ;
        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(-sideForce, 0 , 0 * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(sideForce, 0, 0 * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }


    }
   
}
