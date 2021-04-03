using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    [SerializeField] Sprite[] spriteList;
    [SerializeField] int baseDamage = 10;
    [SerializeField] int damageIncrement = 15;
    int level = 1;
    int maxLevel;

    public void Awake()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        UpdateSprite();
        maxLevel = spriteList.Length;
    }
    // Start is called before the first frame update

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

    public Sprite GetSprite()
    {
        return mySpriteRenderer.sprite;
    }
    public int GetDamage()
    {
        return baseDamage + (level * damageIncrement);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            LevelUp();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
