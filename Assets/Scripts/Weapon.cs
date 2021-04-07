using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    [SerializeField] ParticleSystem HitVFX;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Attacker attacker))
        {
            HitVFX.Play();
            attacker.TakeDamage(GetDamage());
        }
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
        return baseDamage + (level * damageIncrement);
    }

    private void Update()
    {

    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
