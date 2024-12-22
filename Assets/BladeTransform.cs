using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTransform : MonoBehaviour
{
    public Rigidbody2D rb;
    public float upwardForce = 9.8f; 

    void FixedUpdate()
    {
        rb.AddForce(Vector2.up * upwardForce); 
    }

}
