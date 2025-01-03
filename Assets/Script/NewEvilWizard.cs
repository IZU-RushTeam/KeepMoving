using UnityEngine;

public class NewEvilWizardController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private Animator evilWizardAnimator;

    private Transform player;
    private bool isPlayerInRange = false;

    public float moveSpeed = 2f;
    private Transform currentTarget;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        evilWizardAnimator = GetComponent<Animator>();

        if (evilWizardAnimator == null)
        {
        }

        currentTarget = pointA;
    }

    void Update()
    {
        if (!isPlayerInRange)
        {
            MoveBetweenPoints();
        }
       
    }

    void MoveBetweenPoints()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        evilWizardAnimator.SetBool("isMoving", true);

        Flip();

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
            evilWizardAnimator.SetBool("isMoving", false);
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;

        if (transform.position.x < currentTarget.position.x)
            localScale.x = Mathf.Abs(localScale.x);
        else
            localScale.x = -Mathf.Abs(localScale.x);

        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            evilWizardAnimator.SetBool("isMoving", false);
            evilWizardAnimator.SetTrigger("attack_evilWizard");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            evilWizardAnimator.SetBool("isMoving", true);

            isPlayerInRange = false;

        }
    }

    void RespawnPlayer()
    {
        player.GetComponent<GameController>().Die();
    }
}
