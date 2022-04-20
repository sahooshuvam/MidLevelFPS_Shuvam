using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float playerSpeed = 12f;
    public Transform gunFirePoint;
    Animator animator;
    public Text scoreValue;
    public Text healthValue;

    int health = 100;
    int maxHealth = 100;
    int maxMedkitHealth = 50;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("isIdle");
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetTrigger("isRunning");
            PlayerRun(xInput, zInput);
        }

        animator.SetTrigger("isIdle");

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isShooting");
            WhenZombieGotHit();
        }
    }

    private void PlayerRun(float xInput, float zInput)
    {
        Vector3 move = transform.right * xInput + transform.forward * zInput;
        controller.Move(move * playerSpeed * Time.deltaTime);
       
    }
    private void WhenZombieGotHit()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(gunFirePoint.position, gunFirePoint.forward, out hitInfo, 100f))
        {
            GameObject hitZombie = hitInfo.collider.gameObject;
            if (hitZombie.tag == "Zombie")
            {
                score = score + 10;
                scoreValue.text = score.ToString();
                Destroy(hitZombie);
            }
        }
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MedKit")
        {
            int healthNeeded = maxHealth - health;
            if (maxMedkitHealth >= healthNeeded)
                health = health + healthNeeded;
            else
                health = health + maxMedkitHealth;

            healthValue.text = health.ToString();
            Debug.Log("Health:" + health);
            Destroy(other.gameObject);

        }
    }

    public void TakeHit(int healthDecrease)
    {
        health = Mathf.Clamp(health - healthDecrease,0,maxHealth);
        healthValue.text = health.ToString();
        print(health);

        if (health <= 0)
        {
            Debug.Log("Player Will Die"); ;
        }
    }
}
