using System;
using System.Collections;
using UnityEngine;

public class WaveConfig : MonoBehaviour
{
    public static WaveConfig current;
    [SerializeField] Transform[] spawnPositions;
    //[SerializeField] Attacker[] attackerPrefabArray; // get this from attackerwaves
    [SerializeField] AttackerWave[] attackerWaves; // get this
    //[SerializeField] float[] wavesWaitingTimes;

    public event Action IsBossWave;
    public event Action IsFinishedSpawning;
    private void Awake()
    {
        current = this;
    }
    IEnumerator Start()
    {
        //Debug.Log("test");
        int spawnerCount = spawnPositions.Length;
        for (int waveIndex = 0; waveIndex < attackerWaves.Length; waveIndex++)
        {
            //yield return new WaitForSeconds(wavesWaitingTimes[waveIndex]);
            Debug.Log("this is wave " + waveIndex);
            AttackerWave currAttackerWave = attackerWaves[waveIndex];
            float waitTime=0;
            for (int index = 0; index < spawnerCount; index++)
            {
                Transform spawnPosition = spawnPositions[index];
                //Debug.Log("test " + spawnPosition);
                AttackerSpawner spawner = Instantiate(GameAssets.i.AttackerSpawnerPrefab, spawnPosition.position, Quaternion.identity);
                spawner.transform.parent = spawnPosition;
                spawner.SetAttackerPrefabs(currAttackerWave.attackerPrefabArray);
                spawner.SetMaxEnemyCount(currAttackerWave.enemyCountPerPos[index]);
                spawner.SetWaitTime(currAttackerWave.waitTimePerPos[index], currAttackerWave.waitTimeVariancePerPos[index]);
                waitTime += currAttackerWave.waitTimePerPos[index] + currAttackerWave.waitTimeVariancePerPos[index];
            }
            Debug.Log("waiting "+waitTime+" sec for next wave");
            if (waveIndex == attackerWaves.Length - 2) BossWaveTrigger();
            yield return new WaitForSeconds(waitTime+3f);
        }
        FinishSpawningTrigger();
    }
    public void BossWaveTrigger()
    {
        Debug.Log("its Boss Wave!");
        if (IsBossWave != null)
        {
            IsBossWave();
        }
    }
    public void FinishSpawningTrigger()
    {
        Debug.Log("its Last Wave!");
        if (IsFinishedSpawning != null)
        {
            IsFinishedSpawning();
        }
    }
}
