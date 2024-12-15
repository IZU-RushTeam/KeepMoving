using UnityEngine;

public class EvilWizardController : MonoBehaviour
{
    public Transform pointA; // A noktas�n� belirt
    public Transform pointB; // B noktas�n� belirt
    private Animator evilWizardAnimator; // Animasyon kontrol� i�in Animator

    private Transform player; // Oyuncu
    private bool isPlayerInRange = false; // Oyuncu yak�n m�?

    public float moveSpeed = 2f; // H�z
    private Transform currentTarget; // Mevcut hedef nokta

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncu objesini bul
        evilWizardAnimator = GetComponent<Animator>(); // Animator bile�enini otomatik ba�la

        if (evilWizardAnimator == null)
        {
            Debug.LogError("EvilWizard �zerinde bir Animator bile�eni bulunamad�!");
        }

        currentTarget = pointA; // Ba�lang�� hedefi
    }

    void Update()
    {
        if (!isPlayerInRange)
        {
            MoveBetweenPoints();
        }
        else
        {
            evilWizardAnimator.SetTrigger("attack_evilWizard"); // Attack animasyonunu ba�lat
            RespawnPlayer(); // Oyuncuyu respawn et
        }
    }

    void MoveBetweenPoints()
    {
        // Hedefe do�ru hareket et
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        // Hareket s�ras�nda animasyonu de�i�tir
        evilWizardAnimator.SetBool("isMoving", true);

        // Hedefe ula��ld���nda di�er noktaya ge�
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA; // Hedefi de�i�tir
            evilWizardAnimator.SetBool("isMoving", false); // Idle animasyonu
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Oyuncu EvilWizard'a yakla�t�
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Oyuncu uzakla�t�
        }
    }

    void RespawnPlayer()
    {
        // Oyuncuyu respawn etmek i�in
        player.position = new Vector3(0, 0, 0); // Yeni pozisyona ayarla (�rnek: (0, 0, 0))
        // Respawn animasyonu varsa, onu burada tetikleyebilirsin.
    }
}
