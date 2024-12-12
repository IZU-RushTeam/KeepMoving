using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB; 
    public float speed = 2f; 
    public float detectionRange = 2f; 
    public Animator animator; 
    private Transform targetPoint; 
    private Transform player; 
    private bool isAttacking = false; 

    void Start()
    {
        targetPoint = pointA; // Ba�lang�� noktas�
        player = GameObject.FindWithTag("Player").transform; // Oyuncuyu bul
    }

    void Update()
    {
        if (isAttacking)
        {
            Debug.Log("Sald�r� modunda, hareket durduruldu."); // Buraya giriyor mu?
            return; // Sald�r� durumunda di�er i�lemleri durdur
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("Oyuncuyu alg�lad�, sald�r�ya ge�iliyor.");
            AttackPlayer();
        }
        else
        {
            Debug.Log("Oyuncu alg�lanmad�, gezinmeye devam ediliyor.");
            Patrol();
        }
    }

    void Patrol()
    {
        animator.SetBool("isWalking", true); // Y�r�me animasyonunu ba�lat

        // Hedef noktaya do�ru hareket et
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Hedef noktaya ula�t���nda di�er noktaya ge�
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA; // Hedefi de�i�tir
            Flip(); // Y�n de�i�tir
        }
    }

    async void AttackPlayer()
    {
        isAttacking = true;
        animator.SetBool("isWalking", false); // Y�r�me animasyonunu durdur
        animator.SetTrigger("attack"); // Sald�r� animasyonunu tetikle

        Debug.Log("Sald�r� ba�lad�, animasyon bitimi bekleniyor.");

        // Animasyonun tamamlanmas�n� bekle
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            await System.Threading.Tasks.Task.Yield();
        }

        Debug.Log("Animasyon tamamland�, oyuncuya zarar veriliyor.");

        // Oyuncuya zarar ver
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            player.GetComponent<PlayerRespawn>().Respawn(); // Respawn fonksiyonunu �a��r
        }

        AttackComplete(); // Sald�r� tamamland�, di�er i�lemler devam etsin
    }


    public void DealDamage() // Bu fonksiyon attack animasyonu s�ras�nda �a�r�l�r
    {
        Debug.Log("Zarar verme fonksiyonu �al��t�.");
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            Debug.Log("Oyuncuya zarar verildi.");
            // Oyuncuya zarar ver
            player.GetComponent<PlayerRespawn>().Respawn(); // Respawn fonksiyonunu �a��r
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;

        if (transform.position.x < targetPoint.position.x)
            localScale.x = Mathf.Abs(localScale.x); // Sa�a bak
        else
            localScale.x = -Mathf.Abs(localScale.x); // Sola bak

        transform.localScale = localScale;
    }
    public void AttackComplete()
    {
        Debug.Log("Sald�r� tamamland�, y�r�meye d�n�l�yor.");
        isAttacking = false;
        animator.ResetTrigger("attack");
        animator.SetBool("isWalking", true);
    }

}
