using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] GameObject HitVFXPrefab;
    [SerializeField] int usage = 1;
    [SerializeField] int maxWeaponInstance;
    public Vector2 origin = Vector2.zero;
    DamageDealer myDamageDealer;
    Rigidbody2D myRigidBody2D;
    int laneNo;
    public override int GetMaxWeaponInstance()
    {
        return maxWeaponInstance;
    }

    public Projectile(Vector2 targetPos)
    {

    }

    public void Initialize(Vector2 targetPos)
    {
        transform.position = origin;
        Vector3 aimDirection = (targetPos-origin).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        myRigidBody2D.velocity = (targetPos-origin).normalized*projectileSpeed;
    }

    void Awake()
    {
        damageMultiplier = 1; // why do I need this here? it somehow has 0 value
        myDamageDealer = GetComponent<DamageDealer>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        origin = FindObjectOfType<Castle>().transform.position;
    }
    
    private void Start()
    {
        //Initialize();
    }

    public void SetInitialVelocity(Vector2 velocity)
    {
        myRigidBody2D.velocity = velocity;
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
        if (other.TryGetComponent(out Attacker attacker))
        {
            GameObject hitVFX = Instantiate(HitVFXPrefab, other.transform.position, Quaternion.identity);
            Destroy(hitVFX, 1f);
            Debug.Log("1: " + damageMultiplier);
            attacker.TakeDamage(Mathf.RoundToInt(myDamageDealer.GetDamage()*damageMultiplier));
            Debug.Log("2: " + damageMultiplier);
            Hit();
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

