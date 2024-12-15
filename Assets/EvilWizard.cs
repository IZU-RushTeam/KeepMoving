using UnityEngine;

public class EvilWizardController : MonoBehaviour
{
    public Transform pointA; // A noktasýný belirt
    public Transform pointB; // B noktasýný belirt
    private Animator evilWizardAnimator; // Animasyon kontrolü için Animator

    private Transform player; // Oyuncu
    private bool isPlayerInRange = false; // Oyuncu yakýn mý?

    public float moveSpeed = 2f; // Hýz
    private Transform currentTarget; // Mevcut hedef nokta

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncu objesini bul
        evilWizardAnimator = GetComponent<Animator>(); // Animator bileþenini otomatik baðla

        if (evilWizardAnimator == null)
        {
            Debug.LogError("EvilWizard üzerinde bir Animator bileþeni bulunamadý!");
        }

        currentTarget = pointA; // Baþlangýç hedefi
    }

    void Update()
    {
        if (!isPlayerInRange)
        {
            MoveBetweenPoints();
        }
        else
        {
            evilWizardAnimator.SetTrigger("attack_evilWizard"); // Attack animasyonunu baþlat
            RespawnPlayer(); // Oyuncuyu respawn et
        }
    }

    void MoveBetweenPoints()
    {
        // Hedefe doðru hareket et
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        // Hareket sýrasýnda animasyonu deðiþtir
        evilWizardAnimator.SetBool("isMoving", true);

        // Hedefe ulaþýldýðýnda diðer noktaya geç
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA; // Hedefi deðiþtir
            evilWizardAnimator.SetBool("isMoving", false); // Idle animasyonu
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Oyuncu EvilWizard'a yaklaþtý
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Oyuncu uzaklaþtý
        }
    }

    void RespawnPlayer()
    {
        // Oyuncuyu respawn etmek için
        player.position = new Vector3(0, 0, 0); // Yeni pozisyona ayarla (örnek: (0, 0, 0))
        // Respawn animasyonu varsa, onu burada tetikleyebilirsin.
    }
}
