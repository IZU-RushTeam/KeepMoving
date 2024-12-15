using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public void Respawn()
    {
        Debug.Log("Respawn fonksiyonu çalýþtý!"); 
        transform.position = startPos; 
        Debug.Log("Oyuncu yeniden doðdu!");
    }
    private Vector2 startPos;
    public Vector2 StartPos
    {
        get { return startPos; }
        set { startPos = value; }
    }

    private void Start()
    {
        startPos = transform.position; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpýþma testi
        Debug.Log("Çarpýþma Algýlandý!");
        Debug.Log("Çarpýþma Algýlandý! Çarpýþan Objelerin Tag'i: " + collision.tag); 

        if (collision.CompareTag("ates"))
        {
            Debug.Log("Ateþe deðildi! Oyuncu öldü.");
            Respawn(); 
        }
        Debug.Log("deneme");

    }


}
