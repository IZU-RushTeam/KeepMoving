using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    [SerializeField] BoxCollider2D bc2D;
    

    public void Activate()
    {
        print("aktif");
        bc2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject);
        GameController gameController = collision.GetComponent<GameController>();
        if (gameController != null)
        {
            print("entered");
            gameController.Die();
        }
    }
}
