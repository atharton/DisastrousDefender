using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] int usage = 1;
    DamageDealer myDamageDealer;
    int laneNo;
    // Start is called before the first frame update
    void Start()
    {
        myDamageDealer = GetComponent<DamageDealer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }

    // Called by collided object
    private void OnTriggerEnter2D(Collider2D other)
    {
        Attacker attacker = other.GetComponent<Attacker>();
        if (attacker != null && laneNo == attacker.GetLaneNo())
        {
            Hit();
            attacker.TakeDamage(myDamageDealer.GetDamage());
        }
        else return;
    }
    private void Hit()
    {
        usage--;
        if (usage <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    public int GetLaneNo()
    {
        return laneNo;
    }

    public void SetLaneNo(int lane)
    {
        laneNo = lane;
    }
}

