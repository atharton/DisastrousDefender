
using System.Collections;
using UnityEngine;


public class Attacker : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] GameObject enemyProjectilePrefab;
    [SerializeField] LayerMask attackLayer;
    float currentSpeed;
    Vector2 force;
    [SerializeField]GameObject currentTarget;
    SpriteRenderer mySpriteRenderer;
    //BoxCollider2D myBoxCollider2D;
    Rigidbody2D myRigidBody2D;
    RaycastHit2D myAttackRaycast;
    DamageDealer myDamageDealer;
    MaterialTintColor myMaterialTintColor;
    protected Health myHealth;
    Drops myDrops;
    //ClickController clickController;
    AudioSource myAudioSource;
    RectTransform myRectTransform;

    Color origColor;
    int laneNo=0;

    Animator myAnimator;

    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();
        myMaterialTintColor = GetComponent<MaterialTintColor>(); 
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myHealth = GetComponent<Health>();
        myDrops = GetComponent<Drops>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myDamageDealer = GetComponent<DamageDealer>();
        origColor = mySpriteRenderer.color;
        //clickController = FindObjectOfType<ClickController>();
        myAudioSource = GetComponent<AudioSource>();
        myRectTransform = GetComponent<RectTransform>();
    }


    private void OnDestroy()
    {

        LevelController levelController = FindObjectOfType<LevelController>();
        if(levelController != null)levelController.AttackerKilled();

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
    }

    private void StopMoving()
    {
        //Debug.Log("I am called to stop");
        SetMovementSpeed(0);
    }

    private void StartMoving()
    {
        SetMovementSpeed(movementSpeed);
    }

    private void OnMouseDown()
    {
        //clickController.AttackWithWeapon(this,myRigidBody2D);
    }
    public virtual void TakeDamage(int damage)
    {
        DamagePopup.Create(transform.position + new Vector3(0.1f,0,0), damage);
        myAudioSource.Play();
        myHealth.reduceHealth(damage);
        myMaterialTintColor.SetTintColor(Color.red);

        myAnimator.SetBool("isTakingDamage",true);
        //StartCoroutine(BlinkColor(Color.red));
        if (myHealth.GetCurrentHealth() == 0)
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
        //Debug.Log("1 "+ transform.position+" "+myAttackRaycast.collider);
        if (myAttackRaycast.collider != null) 
        {
            AttackTriggered(myAttackRaycast.collider.gameObject);
            //Debug.Log("2");
        }
        else StopAttacking();
    }

    private void StopAttacking()
    {
        currentTarget = null;
        myAnimator.SetBool("isAttacking", false);
    }
    private void AttackTriggered(GameObject target)
    {
        currentTarget = target;
        myAnimator.SetBool("isAttacking", true);
    }
    private void FinishTakingDamage()
    { 
        myAnimator.SetBool("isTakingDamage", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject);
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
    public void AttackThrow()
    {
        //Debug.Log("throwing");
        GameObject weaponInstance = Instantiate(enemyProjectilePrefab, transform.position, Quaternion.identity);
        weaponInstance.GetComponent<EnemyProjectile>().Initialize(currentTarget.transform.position);
        weaponInstance.transform.parent = transform;
    }


}

