using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TankController : MonoBehaviour
{
    public int tankNumber;
    public GameObject barrel;
    public GameObject projectile;
    public Text hpTextElement;

    private Rigidbody2D rigidbody2d;
    private float speed = 3.0f;
    private float turnSpeed = 130.0f;
    private float fireRate = 0.5f;
    private float fireRateCounter = 0.0f;
    private int health = 100;
    private string horizontalAxisName;
    private string verticalAxisName;
    private string shootAxisName;

    public void Damage(int points)
    {
        health -= points;
        hpTextElement.text = string.Format("Player {0} HP: {1}", tankNumber, health);
    }

    public void Repair(int points)
    {
        health += points;
        hpTextElement.text = string.Format("Player {0} HP: {1}", tankNumber, health);
    }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        if (tankNumber < 1)
        {
            throw new System.Exception("Invalid tank number");
        }

        horizontalAxisName = string.Format("Tank{0}Horizontal", tankNumber);
        verticalAxisName = string.Format("Tank{0}Vertical", tankNumber);
        shootAxisName = string.Format("Tank{0}Shoot", tankNumber);
    }

    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Health")
        {
            Repair(50);
            Destroy(col.gameObject);
        }
        else if (col.tag == "SpeedPickup")
        {
            speed *= 1.5f;
            turnSpeed *= 1.5f;
            Destroy(col.gameObject);
        }
        else if (col.tag == "DamagePickup")
        {
            fireRate /= 2.0f;
            Destroy(col.gameObject);
        }
    }

    void FixedUpdate()
    {
        float movementInputValue = Input.GetAxis(verticalAxisName);

        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector2 movement = -transform.up * movementInputValue * speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rigidbody2d.MovePosition(rigidbody2d.position + movement);

        float rotationInputValue = Input.GetAxis(horizontalAxisName);

        float turn = -rotationInputValue * turnSpeed * Time.deltaTime;

        rigidbody2d.MoveRotation(rigidbody2d.rotation + turn);

        fireRateCounter += Time.deltaTime;

        if (Input.GetAxis(shootAxisName) != 0.0f && fireRateCounter > fireRate)
        {
            Instantiate(projectile, barrel.transform.position, transform.transform.rotation);
            fireRateCounter = 0.0f;
        }
    }
}
