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
            Debug.LogError("Animator bile�eni bu GameObject üzerinde bulunamad�!");
        }
    }

    void Update()
    {
        if (isAttacking)
        {
            Debug.Log("Saldırı modunda, hareket durduruldu.");
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Debug.Log("Oyuncuyu algıladı, saldırıya geçiliyor.");
            AttackPlayer();
        }
        else
        {
            Debug.Log("Oyuncu algılanmadı, gezinmeye devam ediliyor.");
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
        animator.SetBool("isWalking", false);

        animator.SetTrigger("attack");

        isAttacking = true;

        Debug.Log("Saldırı başladı, animasyon bitimi bekleniyor.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Oyuncu ile çarpışma tespit edildi, saldırı baslatılıyor.");
            AttackPlayer();
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
        hitboxScript.Activate();
        Debug.Log("Saldırı tamamlandı, yürümeye dönülüyor.");
        isAttacking = false;
        animator.ResetTrigger("attack");
        animator.SetBool("isWalking", true);
    }
}