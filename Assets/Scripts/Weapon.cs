using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite[] spriteList;
    [SerializeField] int baseDamage = 10;
    [SerializeField] int damageIncrement = 15;
    [SerializeField] Vector2 knockback;
    [SerializeField] float knockbackDuration;
    int level = 1;
    int maxLevel;

    public void Start()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        UpdateSprite();
        maxLevel = spriteList.Length;
    }
    // Start is called before the first frame update
    public void Attack()
    {
        myAnimator.SetBool("isAttacking", true);
    }

    public void StopAttacking()
    {

        myAnimator.SetBool("isAttacking", false);
    }

    public void LevelUp()
    {
        if (level< maxLevel)
        {
            level++;
            UpdateSprite();
        }
    }
    public void UpdateSprite()
    {
        mySpriteRenderer.sprite = spriteList[level - 1];
    }

    public int GetDamage()
    {
        return baseDamage + (level * damageIncrement);
    }
    public Vector2 GetKnockback()
    {
        return knockback;
    }
    public float GetKnockbackDuration()
    {
        return knockbackDuration;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            LevelUp();
        }
    }

}
