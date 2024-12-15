using UnityEngine;

public class EvilWizardController : MonoBehaviour
{
    public Transform pointA;  // A noktas�n� belirt
    public Transform pointB;  // B noktas�n� belirt
    public Animator evilWizardAnimator;  // Animasyon kontrol� i�in Animator

    private Transform player;  // Oyuncu
    private bool isPlayerInRange = false;  // Oyuncu yak�n m�?

    public float moveSpeed = 2f;  // H�z

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Oyuncu objesini bul
    }

    void Update()
    {
        MoveBetweenPoints();

        if (isPlayerInRange)
        {
            evilWizardAnimator.SetTrigger("attack_evilWizard");  // Attack animasyonunu ba�lat
            RespawnPlayer();  // Oyuncuyu respawn et
        }
    }

    void MoveBetweenPoints()
    {
        // EvilWizard hareket ederken animasyonu de�i�tirmek i�in
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
            isPlayerInRange = true;  // Oyuncu EvilWizard'a yakla�t�
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;  // Oyuncu uzakla�t�
        }
    }

    void RespawnPlayer()
    {
        // Oyuncuyu respawn etmek i�in
        player.position = new Vector3(0, 0, 0);  // Yeni pozisyona ayarla (�rnek: (0, 0, 0))
        // Respawn animasyonu varsa, onu burada tetikleyebilirsin.
    }
}
