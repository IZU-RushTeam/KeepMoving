using UnityEngine;

public class FireDragon : MonoBehaviour
{
    public float fireSpeed = 5f;  // Ate� topunun h�z�
    public float destroyTime = 2f;  // Ate� topunun �mr�
    private Rigidbody2D rb;

    private void Start()
    {
        // Rigidbody2D bile�enini al
        rb = GetComponent<Rigidbody2D>();

        // Ate� topunun sa�a do�ru hareket etmesi i�in velocity ayarla
        rb.velocity = Vector2.right * fireSpeed;


        // Ate� topunun 2 saniye sonra yok olmas�n� sa�la
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Ate�e �arpt�!"); // �arp��ma tespiti

            // Oyuncunun `dead` script'ine eri�
            PlayerRespawn player = collision.GetComponent<PlayerRespawn>();
          
                Debug.Log("Ate�e �arp22t�!"); // �arp��ma tespiti

                Vector2 respawnPos = player.StartPos; // Getter ile pozisyonu al
                collision.transform.position = respawnPos; // Oyuncuyu ba�lang�� noktas�na ta��
                Debug.Log("Oyuncu ba�lang�� pozisyonuna d�nd�!");
            

            Destroy(gameObject); // Ate�i yok et
        }
    }

    private void empty()
    {

    }
}
