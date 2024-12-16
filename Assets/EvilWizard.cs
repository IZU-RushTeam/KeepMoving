using UnityEngine;

public class EvilWizardController : MonoBehaviour
{
    public Transform pointA;  // A noktasýný belirt
    public Transform pointB;  // B noktasýný belirt
    public Animator evilWizardAnimator;  // Animasyon kontrolü için Animator

    private Transform player;  // Oyuncu
    private bool isPlayerInRange = false;  // Oyuncu yakýn mý?

    public float moveSpeed = 2f;  // Hýz

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Oyuncu objesini bul
    }

    void Update()
    {
        MoveBetweenPoints();

        if (isPlayerInRange)
        {
            evilWizardAnimator.SetTrigger("attack_evilWizard");  // Attack animasyonunu baþlat
            RespawnPlayer();  // Oyuncuyu respawn et
        }
    }

    void MoveBetweenPoints()
    {
        // EvilWizard hareket ederken animasyonu deðiþtirmek için
        if (Vector3.Distance(transform.position, pointA.position) > 0.1f)
        {
            evilWizardAnimator.SetBool("isMoving", true);  // Hareket animasyonu
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            evilWizardAnimator.SetBool("isMoving", false);  // Idle animasyonu
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;  // Oyuncu EvilWizard'a yaklaþtý
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;  // Oyuncu uzaklaþtý
        }
    }

    void RespawnPlayer()
    {
        // Oyuncuyu respawn etmek için
        player.position = new Vector3(0, 0, 0);  // Yeni pozisyona ayarla (örnek: (0, 0, 0))
        // Respawn animasyonu varsa, onu burada tetikleyebilirsin.
    }
}
