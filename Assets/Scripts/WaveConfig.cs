using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveConfig : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    //[SerializeField] Attacker[] attackerPrefabArray; // get this from attackerwaves
    [SerializeField] AttackerWave[] attackerWaves; // get this
    [SerializeField] float[] wavesWaitingTimes;

    IEnumerator Start()
    {
        Debug.Log("test");
        int spawnerCount = spawnPositions.Length;
        for (int waveIndex = 0; waveIndex < attackerWaves.Length; waveIndex++)
        {
            yield return new WaitForSeconds(wavesWaitingTimes[waveIndex]);
            AttackerWave currAttackerWave = attackerWaves[waveIndex];
            for (int index = 0; index < spawnerCount; index++)
            {
                Transform spawnPosition = spawnPositions[index];
                Debug.Log("test " + spawnPosition);
                AttackerSpawner spawner = Instantiate(GameAssets.i.AttackerSpawnerPrefab, spawnPosition.position, Quaternion.identity);
                spawner.transform.parent = spawnPosition;
                spawner.SetAttackerPrefabs(currAttackerWave.attackerPrefabArray);
                spawner.SetMaxEnemyCount(currAttackerWave.enemyCountPerPos[index]);
                spawner.SetWaitTime(currAttackerWave.waitTimePerPos[index], currAttackerWave.waitTimeVariancePerPos[index]);
            }
            
        }
    }

}
