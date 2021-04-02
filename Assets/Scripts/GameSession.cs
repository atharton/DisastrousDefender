using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]int currentGold = 0;
    int highScore = 0;
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetGold()
    {
        return currentGold;
    }
    public int GetHighScore()
    {
        return highScore;
    }
    public void AddGold(int gold)
    {
        currentGold += gold;
        //if (highScore < currentGold) highScore = currentGold;
    }
    public bool SpendGold(int gold)
    {
        if (currentGold >= gold)
        {
            currentGold -= gold;
            return true;
        }
        else return false;
        //if (highScore < currentGold) highScore = currentGold;
    }
    public void ResetGold()
    {
        currentGold = 0;
    }
}
