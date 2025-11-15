using UnityEngine;

public class Coin : MonoBehaviour
{
    public float lifetime = 3f;
    public int scoreValue = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}