using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public void Respawn()
    {
        Debug.Log("Respawn fonksiyonu �al��t�!"); // Burada da bir debug mesaj� ekledik
        transform.position = startPos; // Ba�lang�� noktas�na ta��
        Debug.Log("Oyuncu yeniden do�du!");
    }
    private Vector2 startPos;
    public Vector2 StartPos
    {
        get { return startPos; }
        set { startPos = value; }
    }

    private void Start()
    {
        startPos = transform.position; // Ba�lang�� pozisyonunu kaydet
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �arp��ma testi
        Debug.Log("�arp��ma Alg�land�!");
        Debug.Log("�arp��ma Alg�land�! �arp��an Objelerin Tag'i: " + collision.tag); // Tag'i kontrol et

        if (collision.CompareTag("ates"))
        {
            Debug.Log("Ate�e de�ildi! Oyuncu �ld�.");
            Respawn(); // Respawn fonksiyonunu �a��r
        }
        Debug.Log("deneme");

    }


}
