using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            MovePlayer(other.gameObject.GetComponent<Rigidbody2D>(), verticalInput);
        }
    }

    private void MovePlayer(Rigidbody2D rb, float verticalInput)
    {
        Vector2 verticalMovement = new Vector2(0, verticalInput * climbSpeed);
        rb.velocity = verticalMovement;
    }
}