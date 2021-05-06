using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour

{
    [SerializeField] float projectileSpeed = 5f; 
    int damageMultiplier = 1;
    DamageDealer myDamageDealer;
    Rigidbody2D myRigidBody2D;
    //public Vector2 origin = Vector2.zero;

    private void Awake()
    {
        myDamageDealer = GetComponent<DamageDealer>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("hit a " + other.gameObject);
        if (other.TryGetComponent(out Castle castle))
        {
            //Debug.Log("hit a castle");
            //GameObject hitVFX = Instantiate(HitVFXPrefab, other.transform.position, Quaternion.identity);
            //Destroy(hitVFX, 1f);
            castle.TakeDamage(Mathf.RoundToInt(myDamageDealer.GetDamage() * damageMultiplier));
            Die();
        }
        else return;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    public void Initialize(Vector2 targetPos)
    {
        Vector2 origin = transform.position;
        Vector3 aimDirection = (targetPos - origin).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        myRigidBody2D.velocity = (targetPos - origin).normalized * projectileSpeed;
    }
}
