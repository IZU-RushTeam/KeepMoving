using UnityEngine;
using System.Collections;

public class DragonFire : MonoBehaviour
{
    public GameObject fireballPrefab;  // Ate� topu prefab'�
    public Transform firePoint;        // Ate�in ��kaca�� nokta (ejderhan�n a�z�)
    public float fireInterval = 3f;    // Ate�in at�lma aral��� (saniye cinsinden)
    public float fireSpeed = 5f;       // Ate� topunun h�z�

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();  // Animator'� al
        StartCoroutine(FireAtIntervals());
    }

    // Coroutine, belirli bir aral�kla ate� edecek
    private IEnumerator FireAtIntervals()
    {
        while (true) // Sonsuz bir d�ng�de, s�rekli ate� edecek
        {
            animator.SetTrigger("Attack");  // Attack animasyonunu tetikle

            // Ate� topunu instantiate et
            ShootFireball();

            // Belirli bir s�re bekle (ate�in aral�k zaman�)
            yield return new WaitForSeconds(fireInterval);
        }
    }

    private void ShootFireball()
    {
        // Ate� topunu instantiate et ve ejderhan�n a�z�ndan ��kar
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        // Ate� topunun hareket etmesi i�in bir h�z ekleyin
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.right * fireSpeed;  // Ate� topunu sa�a do�ru hareket ettir
        }
    }

    private void empty()
    {

    }
}
