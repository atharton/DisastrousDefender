using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] float levelEndWaitTime = 3;
    int numberOfAttackers = 0;
    bool levelTimerFinished = false;

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }

    public void AttackerSpawned()
    {
        numberOfAttackers++;
    }
    public void AttackerKilled()
    {
        numberOfAttackers--;
        if (levelTimerFinished && numberOfAttackers <= 0) {
            StartCoroutine (HandleWinCondition());
        }
    }
    
    public IEnumerator HandleWinCondition()
    {
        SetWinLabel(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(levelEndWaitTime);
        //GetComponent<LevelLoader>().LoadNextScene();
    }
    public void HandleLoseCondition()
    {
        SetLoseLabel(true);
        Time.timeScale = 0;
    }
    private void SetWinLabel(bool setTo)
    {
        if (winLabel) winLabel.SetActive(setTo);
    }
    private void SetLoseLabel(bool setTo)
    {
        if (loseLabel) loseLabel.SetActive(setTo);
    }


    void Start()
    {
        if (winLabel) winLabel.SetActive(false);
        if (loseLabel) loseLabel.SetActive(false);
    }

    void Update()
    {
        
    }
}
