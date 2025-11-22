using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerController : MonoBehaviour
{

    public int lives;
    private float speed;

    private GameManager gameManager;

    private float horizontalInput;
    private float verticalInput;

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    private bool shieldActive = false;
    private float shieldTimer = 0f;

    public TextMeshProUGUI shieldText;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        speed = 5.0f;
        gameManager.ChangeLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();

        if (shieldActive)
        {
            shieldTimer -= Time.deltaTime;

            if (shieldTimer <= 0)
            {
            shieldActive = false;

                if (shieldText != null)
                shieldText.gameObject.SetActive(false);

            gameManager.PlaySound(2);
            }
        }
    }

    public void LoseALife()
    {
        if (shieldActive)
        {
            shieldActive = false;
            if (shieldText != null)
            shieldText.gameObject.SetActive(false);
            return;
        }

        lives--;
        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        float clampedY = Mathf.Clamp(transform.position.y, -verticalScreenSize, 0);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

    }

    public void ActivateShield(float time)
    {
        shieldActive = true;
        shieldTimer = time;
        if (shieldText != null)
        shieldText.gameObject.SetActive(true);

        gameManager.PlaySound(1);
        
    }
}