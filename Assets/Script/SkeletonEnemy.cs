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
        targetPoint = pointA; 
        player = GameObject.FindWithTag("Player").transform; 
    }

    void Update()
    {
        if (isAttacking)
        {
            Debug.Log("Sald�r� modunda, hareket durduruldu."); 
            return; 
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
        animator.SetBool("isWalking", true); 

        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA; 
            Flip(); 
        }
    }

    async void AttackPlayer()
    {
        isAttacking = true;
        animator.SetBool("isWalking", false); 
        animator.SetTrigger("attack");

        Debug.Log("Sald�r� ba�lad�, animasyon bitimi bekleniyor.");

        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            await System.Threading.Tasks.Task.Yield();
        }

        Debug.Log("Animasyon tamamland�, oyuncuya zarar veriliyor.");

        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            player.GetComponent<PlayerRespawn>().Respawn(); 
        }

        AttackComplete(); 
    }


    public void DealDamage() 
    {
        Debug.Log("Zarar verme fonksiyonu �al��t�.");
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            Debug.Log("Oyuncuya zarar verildi.");
            player.GetComponent<PlayerRespawn>().Respawn(); 
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;

        if (transform.position.x < targetPoint.position.x)
            localScale.x = Mathf.Abs(localScale.x); 
        else
            localScale.x = -Mathf.Abs(localScale.x); 

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
