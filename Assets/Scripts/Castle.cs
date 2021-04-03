using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    Health myStats;
    SpriteRenderer mySpriteRenderer;
    LevelController levelController;
    [SerializeField] GameObject loseDisplay;
    // Start is called before the first frame update

    void Start()
    {
        myStats = GetComponent<Health>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        levelController = FindObjectOfType<LevelController>();
    }
    private IEnumerator BlinkColor(Color color)
    {
        Color origColor = mySpriteRenderer.color;
        mySpriteRenderer.color = color;
        yield return new WaitForSeconds(0.1f);
        mySpriteRenderer.color = origColor;
    }
    public void TakeDamage(float damage)
    {
        myStats.reduceHealth(damage);
        StartCoroutine(BlinkColor(Color.yellow));
        if (myStats.GetCurrentHealth() == 0)
        {
            LoseGame();
            //myAnimator.SetTrigger("Death");
        }
    }

    private void LoseGame()
    {
        //yield return new WaitForSeconds(3);
        mySpriteRenderer.color = Color.red;   //change to animation
        levelController.HandleLoseCondition();
        
    }
}
