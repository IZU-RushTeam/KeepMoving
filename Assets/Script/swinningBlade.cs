using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swinningBlade : MonoBehaviour
{
    public float speed = 2f; // Sallanma hýzý
    public float angle = 45f; // Maksimum açý

    private float startAngle;

    void Start()
    {
        startAngle = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        float rotationAngle = startAngle + Mathf.Sin(Time.time * speed) * angle;
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}
