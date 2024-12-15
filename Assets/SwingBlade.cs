using UnityEngine;

public class BladeSwing : MonoBehaviour
{
    public float swingSpeed = 100f;  
    public float maxAngle = 45f;     
    private float currentAngle = 0f; 
    private bool swingingRight = true; 

    public Transform bladeHandle; 

    void Update()
    {
        SwingBlade();
    }

    void SwingBlade()
    {
        // Sallanma hareketini yönet
        if (swingingRight)
        {
            currentAngle += swingSpeed * Time.deltaTime; 
            if (currentAngle >= maxAngle)
                swingingRight = false;  
        }
        else
        {
            currentAngle -= swingSpeed * Time.deltaTime; 
            if (currentAngle <= -maxAngle)
                swingingRight = true;   
        }

        bladeHandle.localRotation = Quaternion.Euler(0f, 0f, currentAngle);  
    }
}
