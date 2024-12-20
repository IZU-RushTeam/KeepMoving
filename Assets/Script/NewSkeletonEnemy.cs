using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkeletonEnemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float detectionRange = 2f;
    private Animator animator; 
    private Transform targetPoint;
    private Transform player;
    private bool isAttacking = false;
    [SerializeField] HitboxScript hitboxScript;
    void Start() 
    {
        targetPoint = pointA; 
        player = GameObject.FindWithTag("Player").transform; 

       
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator bile�eni bu GameObject �zerinde bulunamad�!");
        }
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

    void AttackPlayer()
    {
        isAttacking = true;
        animator.SetBool("isWalking", false);
        animator.SetTrigger("attack");
        Debug.Log("Sald�r� ba�lad�, animasyon bitimi bekleniyor.");

        StartCoroutine(WaitForAttackAnimationAndDie());
    }

    IEnumerator WaitForAttackAnimationAndDie()
    {
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        );

        Debug.Log("Sald�r� animasyonu tamamland�, Die metodu �a�r�l�yor.");
        player.GetComponent<GameController>().Die();

        AttackComplete();
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
        hitboxScript.Activate();
        Debug.Log("Sald�r� tamamland�, y�r�meye d�n�l�yor.");
        isAttacking = false;
        animator.ResetTrigger("attack");
        animator.SetBool("isWalking", true);
    }
}
