using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public void Respawn()
    {
        Debug.Log("Respawn fonksiyonu �al��t�!"); 
        transform.position = startPos; 
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
        startPos = transform.position; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �arp��ma testi
        Debug.Log("�arp��ma Alg�land�!");
        Debug.Log("�arp��ma Alg�land�! �arp��an Objelerin Tag'i: " + collision.tag); 

        if (collision.CompareTag("ates"))
        {
            Debug.Log("Ate�e de�ildi! Oyuncu �ld�.");
            Respawn(); 
        }
        Debug.Log("deneme");

    }


}
