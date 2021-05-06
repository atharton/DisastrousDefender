using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour, IDamageableByEnemy
{
    Health myHealth;
    SpriteRenderer mySpriteRenderer;
    LevelController levelController;
    MaterialTintColor myMaterialTintColor;
    [SerializeField] SpriteState spriteState;
    [SerializeField] GameObject loseDisplay;
    AudioSource myAudioSource;
    // Start is called before the first frame update

    void Awake()
    {
        myHealth = GetComponent<Health>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        levelController = FindObjectOfType<LevelController>();
        myMaterialTintColor = GetComponent<MaterialTintColor>();
        myAudioSource = GetComponent<AudioSource>();
    }
    private IEnumerator BlinkColor(Color color)
    {
        Color origColor = mySpriteRenderer.color;
        mySpriteRenderer.color = color;
        yield return new WaitForSeconds(0.1f);
        mySpriteRenderer.color = origColor;
    }
    public void TakeDamage(int damage)
    {
        myAudioSource.Play();
        myHealth.reduceHealth(damage);
        //StartCoroutine(BlinkColor(Color.yellow));
        myMaterialTintColor.SetTintColor(Color.yellow);
        SpriteStateChange();
        if (myHealth.GetCurrentHealth() <= 0)
        {
            LoseGame();
            //myAnimator.SetTrigger("Death");
        }
    }

    private void SpriteStateChange()
    {
        float currHealth = myHealth.GetCurrentHealth();
        float maxHealth = myHealth.GetMaxHealth();
        if (currHealth <= 0.2 * maxHealth)
        {
            spriteState.SetCurrentSprite(2);
        }
        else if (currHealth <= 0.5 * maxHealth)
        {
            spriteState.SetCurrentSprite(1);
        }
        else
        {
            spriteState.SetCurrentSprite(0);
        }
        mySpriteRenderer.sprite = spriteState.GetCurrentSprite();
    }

    private void LoseGame()
    {
        //yield return new WaitForSeconds(3);
        mySpriteRenderer.color = Color.red;   //change to animation
        levelController.HandleLoseCondition();
        
    }
}
