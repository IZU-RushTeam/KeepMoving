using UnityEngine;

public class BladeSwing : MonoBehaviour
{
    public float swingSpeed = 100f;  // Sallanma hýzý
    public float maxAngle = 45f;     // Maksimum sallanma açýsý
    private float currentAngle = 0f; // Kýlýcýn mevcut açýsý
    private bool swingingRight = true; // Sallanma yönü

    // Kýlýcýn sadece alt kýsmýný döndüren objeyi referans alýyoruz
    public Transform bladeHandle; // Kýlýcýn alt kýsmýný tutan objeyi baðlayacaðýz

    void Update()
    {
        SwingBlade();
    }

    void SwingBlade()
    {
        // Sallanma hareketini yönet
        if (swingingRight)
        {
            currentAngle += swingSpeed * Time.deltaTime; // Sað yönde artýþ
            if (currentAngle >= maxAngle)
                swingingRight = false;  // Yön deðiþtir
        }
        else
        {
            currentAngle -= swingSpeed * Time.deltaTime; // Sol yönde azalma
            if (currentAngle <= -maxAngle)
                swingingRight = true;   // Yön deðiþtir
        }

        // Alt kýsmý döndür
        bladeHandle.localRotation = Quaternion.Euler(0f, 0f, currentAngle);  // Yalnýzca Z ekseninde döndürme
    }
}
