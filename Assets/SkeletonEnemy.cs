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
        targetPoint = pointA; // Baþlangýç noktasý
        player = GameObject.FindWithTag("Player").transform; // Oyuncuyu bul
    }

    void Update()
    {
        if (isAttacking)
        {
            Debug.Log("Saldýrý modunda, hareket durduruldu."); // Buraya giriyor mu?
            return; // Saldýrý durumunda diðer iþlemleri durdur
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("Oyuncuyu algýladý, saldýrýya geçiliyor.");
            AttackPlayer();
        }
        else
        {
            Debug.Log("Oyuncu algýlanmadý, gezinmeye devam ediliyor.");
            Patrol();
        }
    }

    void Patrol()
    {
        animator.SetBool("isWalking", true); // Yürüme animasyonunu baþlat

        // Hedef noktaya doðru hareket et
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Hedef noktaya ulaþtýðýnda diðer noktaya geç
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA; // Hedefi deðiþtir
            Flip(); // Yön deðiþtir
        }
    }

    async void AttackPlayer()
    {
        isAttacking = true;
        animator.SetBool("isWalking", false); // Yürüme animasyonunu durdur
        animator.SetTrigger("attack"); // Saldýrý animasyonunu tetikle

        Debug.Log("Saldýrý baþladý, animasyon bitimi bekleniyor.");

        // Animasyonun tamamlanmasýný bekle
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            await System.Threading.Tasks.Task.Yield();
        }

        Debug.Log("Animasyon tamamlandý, oyuncuya zarar veriliyor.");

        // Oyuncuya zarar ver
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            player.GetComponent<PlayerRespawn>().Respawn(); // Respawn fonksiyonunu çaðýr
        }

        AttackComplete(); // Saldýrý tamamlandý, diðer iþlemler devam etsin
    }


    public void DealDamage() // Bu fonksiyon attack animasyonu sýrasýnda çaðrýlýr
    {
        Debug.Log("Zarar verme fonksiyonu çalýþtý.");
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            Debug.Log("Oyuncuya zarar verildi.");
            // Oyuncuya zarar ver
            player.GetComponent<PlayerRespawn>().Respawn(); // Respawn fonksiyonunu çaðýr
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;

        if (transform.position.x < targetPoint.position.x)
            localScale.x = Mathf.Abs(localScale.x); // Saða bak
        else
            localScale.x = -Mathf.Abs(localScale.x); // Sola bak

        transform.localScale = localScale;
    }
    public void AttackComplete()
    {
        Debug.Log("Saldýrý tamamlandý, yürümeye dönülüyor.");
        isAttacking = false;
        animator.ResetTrigger("attack");
        animator.SetBool("isWalking", true);
    }

}
