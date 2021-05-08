
using System.Collections;

using UnityEngine;
using DG.Tweening;

public class LevelController : MonoBehaviour
{
    public static LevelController current;

    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] float levelEndWaitTime = 3;
    int numberOfAttackers = 0;
    bool levelTimerFinished = false;





    private void Awake()
    {
        current = this;
        WaveConfig.current.IsFinishedSpawning += LevelTimerFinished;
        SetWinLabel(false);
        SetLoseLabel(false);
    }
    

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
    
    public void Lose()
    {
        StartCoroutine(HandleLoseCondition());
    }

    public IEnumerator HandleWinCondition()
    {
        SetWinLabel(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(levelEndWaitTime);
        //GetComponent<LevelLoader>().LoadNextScene();
    }
    public IEnumerator HandleLoseCondition()
    {
        SetLoseLabel(true);
        yield return new WaitForSeconds(levelEndWaitTime);
        //float myFloat = Time.timeScale;
        //DOTween.To(() => myFloat, x => myFloat = x, 0, 1).OnUpdate(() => Time.timeScale = myFloat);
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
