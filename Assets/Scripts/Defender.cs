using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float attackSpeed;
    [SerializeField] float goldCost;
    float damageMultiplier = 1;
    float speedMultiplier = 1;

    [Header("Prefabs")]
    [SerializeField] GameObject throwPosition;
    [SerializeField] Health stats;
    [SerializeField] Projectile projectile;

    int laneNo;
    //[SerializeField] GameObject body;

    SpriteRenderer mySpriteRenderer;
    Animator myAnimator;
    //AttackerSpawner laneSpawner;

    void Start()
    {
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        //laneSpawner = GetMyLaneSpawner();
    }

   
    // Update is called once per frame
    void Update()
    {
        AnimationState();
    }
    /*
    private AttackerSpawner GetMyLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawners)
        {
            if (laneNo == spawner.GetLaneNo()) return spawner;
        }
        return null;
    }
    */
    private void AnimationState()
    {
        if (SeeEnemy()) myAnimator.SetBool("isAttacking", true);
        else myAnimator.SetBool("isAttacking", false);
    }

    private bool SeeEnemy()
    {
        //if (laneSpawner.transform.childCount > 0) return true;
        //else return false;
        return false;
    }

    public void Attack()
    {
        Projectile curProjectile = Instantiate(projectile, throwPosition.transform.position, transform.rotation);
        curProjectile.SetLaneNo(laneNo);
        curProjectile.GetComponent<Rigidbody2D>().velocity += new Vector2(curProjectile.GetProjectileSpeed()*speedMultiplier, 0);
    }

    public float GetCost()
    {
        return goldCost;
    }
    public int GetLaneNo()
    {
        return laneNo;
    }

    public void SetLaneNo(int lane)
    {
        laneNo = lane;
        if (!mySpriteRenderer) mySpriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
        mySpriteRenderer.sortingOrder = lane;
    }
    public void TakeDamage(float damage)
    {
        stats.reduceHealth(damage);
        //StartCoroutine(BlinkColor(Color.yellow));
        if (stats.GetCurrentHealth() == 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            myAnimator.SetTrigger("Death");
        }
    }
    private void OnMouseDown()
    {
        TakeDamage(25);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private IEnumerator BlinkColor(Color color)
    {
        Color origColor = mySpriteRenderer.color;
        mySpriteRenderer.color = color;
        yield return new WaitForSeconds(0.1f);
        mySpriteRenderer.color = origColor;
    }

}
