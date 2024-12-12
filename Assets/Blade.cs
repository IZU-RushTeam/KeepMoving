using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float speed = 2f;   
    public float angle = 45f;  

    private float startAngle;

    void Start()
    {
        startAngle = transform.eulerAngles.z;
    }

    void Update()
    {
        float rotation = Mathf.Sin(Time.time * speed) * angle;
        transform.rotation = Quaternion.Euler(0, 0, startAngle + rotation);
    }
}
