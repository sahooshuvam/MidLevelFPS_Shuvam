using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    public void TakeHit(int healthDecrease)
    {
        health -= healthDecrease;
        print("Health:" +health);
    }
}
