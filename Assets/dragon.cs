using UnityEngine;
using System.Collections;

public class DragonFire : MonoBehaviour
{
    public GameObject fireballPrefab;  // Ateþ topu prefab'ý
    public Transform firePoint;        // Ateþin çýkacaðý nokta (ejderhanýn aðzý)
    public float fireInterval = 3f;    // Ateþin atýlma aralýðý (saniye cinsinden)
    public float fireSpeed = 5f;       // Ateþ topunun hýzý

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();  // Animator'ý al
        StartCoroutine(FireAtIntervals());
    }

    // Coroutine, belirli bir aralýkla ateþ edecek
    private IEnumerator FireAtIntervals()
    {
        while (true) // Sonsuz bir döngüde, sürekli ateþ edecek
        {
            animator.SetTrigger("Attack");  // Attack animasyonunu tetikle

            // Ateþ topunu instantiate et
            ShootFireball();

            // Belirli bir süre bekle (ateþin aralýk zamaný)
            yield return new WaitForSeconds(fireInterval);
        }
    }

    private void ShootFireball()
    {
        // Ateþ topunu instantiate et ve ejderhanýn aðzýndan çýkar
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        // Ateþ topunun hareket etmesi için bir hýz ekleyin
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.right * fireSpeed;  // Ateþ topunu saða doðru hareket ettir
        }
    }

    private void empty()
    {

    }
}
