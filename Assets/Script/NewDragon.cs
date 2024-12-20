using UnityEngine;
using System.Collections;

public class NewDragonFire : MonoBehaviour
{
    public GameObject fireballPrefab;  
    public Transform firePoint;        
    public float fireInterval = 3f;    
    public float fireSpeed = 5f;       

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();  
        StartCoroutine(FireAtIntervals());
    }

    
    private IEnumerator FireAtIntervals()
    {
        while (true) 
        {
            animator.SetTrigger("Attack");  

            ShootFireball();

            yield return new WaitForSeconds(fireInterval);
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
    }


   
}
