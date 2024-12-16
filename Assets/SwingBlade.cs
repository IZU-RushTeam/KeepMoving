using UnityEngine;

public class BladeSwing : MonoBehaviour
{
    public float swingSpeed = 100f;  // Sallanma h�z�
    public float maxAngle = 45f;     // Maksimum sallanma a��s�
    private float currentAngle = 0f; // K�l�c�n mevcut a��s�
    private bool swingingRight = true; // Sallanma y�n�

    // K�l�c�n sadece alt k�sm�n� d�nd�ren objeyi referans al�yoruz
    public Transform bladeHandle; // K�l�c�n alt k�sm�n� tutan objeyi ba�layaca��z

    void Update()
    {
        SwingBlade();
    }

    void SwingBlade()
    {
        // Sallanma hareketini y�net
        if (swingingRight)
        {
            currentAngle += swingSpeed * Time.deltaTime; // Sa� y�nde art��
            if (currentAngle >= maxAngle)
                swingingRight = false;  // Y�n de�i�tir
        }
        else
        {
            currentAngle -= swingSpeed * Time.deltaTime; // Sol y�nde azalma
            if (currentAngle <= -maxAngle)
                swingingRight = true;   // Y�n de�i�tir
        }

        // Alt k�sm� d�nd�r
        bladeHandle.localRotation = Quaternion.Euler(0f, 0f, currentAngle);  // Yaln�zca Z ekseninde d�nd�rme
    }
}
