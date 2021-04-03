using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Attacker : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask attackLayer;
    float currentSpeed;
    Vector2 force;
    //[SerializeField] float damage = 40;
    [SerializeField]GameObject currentTarget;
    SpriteRenderer mySpriteRenderer;
    BoxCollider2D myBoxCollider2D;
    Rigidbody2D myRigidBody2D;
    RaycastHit2D myAttackRaycast;
    DamageDealer myDamageDealer;
    Health myStats;
    Drops myDrops;
    ClickController clickController;

    Color origColor;
    int laneNo;

    Animator myAnimator;

    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();
    }

    private void OnDestroy()
    {

        LevelController levelController = FindObjectOfType<LevelController>();
        if(levelController != null)levelController.AttackerKilled();

    }
    private void Start()
    {
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myStats = GetComponent<Health>();
        myDrops = GetComponent<Drops>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myDamageDealer = GetComponent<DamageDealer>();
        origColor = mySpriteRenderer.color;
        clickController = FindObjectOfType<ClickController>();

    }

    void FixedUpdate()
    {
        //transform.Translate(Vector2.left * Time.deltaTime* currentSpeed);
        //myRigidBody2D.velocity = new Vector2(-currentSpeed, 0);
        Vector2 v2Pos = transform.position;
        myRigidBody2D.MovePosition(v2Pos+force*Time.deltaTime);
        
        //UpdateAnimationState();
    }

    private void Update()
    {
        DetectDefender();
    }
    private void UpdateAnimationState()
    {
        /*
        if (currentTarget == null)
        {
            myAnimator.SetBool("isAttacking", false);
        }
        */
    }



    private void SetMovementSpeed(float speed)
    {
        //Debug.Log(speed);
        currentSpeed = speed;
        force = new Vector2(-currentSpeed, 0);
        //Debug.Log(Time.deltaTime);
        Debug.Log(force);
    }

    private void StopMoving()
    {
        Debug.Log("I am called to stop");
        SetMovementSpeed(0);
    }

    private void StartMoving()
    {
        SetMovementSpeed(movementSpeed);
    }

    private void OnMouseDown()
    {
        clickController.AttackWithWeapon(this,myRigidBody2D);
    }
    public void TakeDamage(float damage)
    {
        myStats.reduceHealth(damage);
        StartCoroutine(BlinkColor(Color.red));
        Debug.Log(0);
        if (myStats.GetCurrentHealth() == 0)
        {
            myRigidBody2D.gravityScale=0;
            GetComponent<BoxCollider2D>().enabled = false;
            GiveGold();
            myAnimator.SetTrigger("Death");
        }
    }

    private void GiveGold()
    {
        var currentGameSession = FindObjectOfType<GameSession>();
        int goldDrop = myDrops.getGoldDrop();
        currentGameSession.AddGold(goldDrop);
        // add gold + animation
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    private IEnumerator BlinkColor(Color color)
    {
        mySpriteRenderer.color = color;
        yield return new WaitForSeconds(0.1f);
        mySpriteRenderer.color = origColor;
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


    private void DetectDefender()
    {
        myAttackRaycast = Physics2D.Raycast(transform.position, Vector2.left, attackRange,attackLayer);
        if (myAttackRaycast.collider != null) AttackTriggered(myAttackRaycast.collider.gameObject);
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        currentTarget = null;
        myAnimator.SetBool("isAttacking", false);
    }
    private void AttackTriggered(GameObject target)
    {
        currentTarget = target;
        myAnimator.SetBool("isAttacking", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
    }

    public void Attack()
    {
        if (currentTarget == null)
        {
            myAnimator.SetBool("isAttacking", false);
            return;
        }

        Defender defender = currentTarget.GetComponent<Defender>();
        Castle castle = currentTarget.GetComponent<Castle>();
        if (defender != null && defender.GetLaneNo()==laneNo)
        {
            defender.TakeDamage(myDamageDealer.GetDamage());
        }
        else if (castle != null)
        {
            castle.TakeDamage(myDamageDealer.GetDamage());
        }
        //else myAnimator.SetBool("isAttacking", false);
    }
 
}

