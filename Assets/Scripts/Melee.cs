using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    [SerializeField] GameObject HitVFXPrefab;
    [SerializeField] Sprite[] spriteList;
    [SerializeField] int baseDamage = 10;
    [SerializeField] int damageIncrement = 15;
    [SerializeField] int maxWeaponInstance;
    [SerializeField] [Range(0,1)]float subsequentReducedDamagePercentage = 0.2f;
    float currentDamagePercentage = 1f;
    int level = 1;
    int maxLevel;
    public override int GetMaxWeaponInstance()
    {
        return maxWeaponInstance;
    }
    public Melee()
    {

    }

    public void Awake()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        UpdateSprite();
        maxLevel = spriteList.Length;
    }
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentDamagePercentage > 0 && collision.gameObject.TryGetComponent(out Attacker attacker))
        {
            GameObject hitVFX = Instantiate(HitVFXPrefab, collision.transform.position, Quaternion.identity);
            Destroy(hitVFX, 1f);
            attacker.TakeDamage(Mathf.RoundToInt(GetDamage() * currentDamagePercentage));
            ReduceSubsequentDamage();
        }
    }

    private void ReduceSubsequentDamage()
    {
        if (currentDamagePercentage - subsequentReducedDamagePercentage <= 0) currentDamagePercentage = 0;
        else currentDamagePercentage -= subsequentReducedDamagePercentage;
    }

    public void SetLevel(int toLevel)
    {
        if (toLevel<= maxLevel)
        {
            level = toLevel;
            UpdateSprite();
        }
        else { Debug.LogError("Level doesn't exist"); }
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
        Debug.Log(damageMultiplier);
        return (baseDamage + (level * damageIncrement))*damageMultiplier;
    }

    private void Update()
    {

    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
