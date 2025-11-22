using UnityEngine;

public class Shield : MonoBehaviour
{
public float duration = 5f;

private void OnTriggerEnter2D(Collider2D other)

{
    if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            player.ActivateShield(duration);

            GameObject.Find("GameManager").GetComponent<GameManager>().PlaySound(1);

            Destroy(gameObject);
        }
    }
}