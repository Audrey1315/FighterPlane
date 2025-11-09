using UnityEngine;

public class SecondEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float frequency = 2f;   
    public float magnitude = 0.75f;
    public GameObject explosionPrefab;

    private GameManager gameManager;
    private Vector3 startPos;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startPos = transform.position;
    }

    void Update()
    {
        startPos += Vector3.down * speed * Time.deltaTime;
        transform.position = startPos + Vector3.right * Mathf.Sin(Time.time * frequency) * magnitude;

        if (transform.position.y < -gameManager.verticalScreenSize - 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Weapons"))
        {
            Destroy(other.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(gameObject);
        }
    }
}
