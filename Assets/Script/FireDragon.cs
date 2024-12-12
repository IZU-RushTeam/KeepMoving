using UnityEngine;

public class FireDragon : MonoBehaviour
{
    public float fireSpeed = 5f;  // Ateþ topunun hýzý
    public float destroyTime = 2f;  // Ateþ topunun ömrü
    private Rigidbody2D rb;

    private void Start()
    {
        // Rigidbody2D bileþenini al
        rb = GetComponent<Rigidbody2D>();

        // Ateþ topunun saða doðru hareket etmesi için velocity ayarla
        rb.velocity = Vector2.right * fireSpeed;


        // Ateþ topunun 2 saniye sonra yok olmasýný saðla
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Ateþe çarptý!"); // Çarpýþma tespiti

            // Oyuncunun `dead` script'ine eriþ
            PlayerRespawn player = collision.GetComponent<PlayerRespawn>();
          
                Debug.Log("Ateþe çarp22tý!"); // Çarpýþma tespiti

                Vector2 respawnPos = player.StartPos; // Getter ile pozisyonu al
                collision.transform.position = respawnPos; // Oyuncuyu baþlangýç noktasýna taþý
                Debug.Log("Oyuncu baþlangýç pozisyonuna döndü!");
            

            Destroy(gameObject); // Ateþi yok et
        }
    }

    private void empty()
    {

    }
}
