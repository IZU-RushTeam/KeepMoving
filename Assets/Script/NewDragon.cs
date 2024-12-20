using UnityEngine;
using System.Collections;

public class NewDragonFire : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireInterval = 0.5f;   
    public float fireSpeed = 5f;
    public float fireballLifetime = 2f;
    public float fireDuration = 5f;    
    public float waitDuration = 5f;    

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            float elapsedFireTime = 0f;
            while (elapsedFireTime < fireDuration)
            {
                animator.SetTrigger("Attack");
                ShootFireball();
                elapsedFireTime += fireInterval;
                yield return new WaitForSeconds(fireInterval);
            }

            yield return new WaitForSeconds(waitDuration);
        }
    }

    private void ShootFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 fireDirection = firePoint.right.normalized;
            rb.velocity = fireDirection * fireSpeed;
        }

        Destroy(fireball, fireballLifetime);
    }
}
