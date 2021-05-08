using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;
    //[Range(1, 5)] [SerializeField] int laneNo;
    [SerializeField] float startingWaitTime = 2f;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float waitTimeVariance = 1f;
    [SerializeField] int maxEnemyCount = 5;
    [SerializeField] Attacker[] attackerPrefabArray;
    int currEnemyCount = 0;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(startingWaitTime);
        while (spawn && currEnemyCount<maxEnemyCount)
        {
            yield return new WaitForSeconds(Random.Range(waitTime-waitTimeVariance,  waitTime+waitTimeVariance));
            SpawnAttacker();
        }
    }
    private void SpawnAttacker()
    {
        Attacker currentAttacker = attackerPrefabArray[Random.Range(0,attackerPrefabArray.Length)];
        Spawn(currentAttacker);
    }

    private void Spawn(Attacker attackerPrefab)
    {
        Attacker newAttacker = Instantiate(attackerPrefab, transform.position, transform.rotation) as Attacker;
        newAttacker.transform.parent = transform;
        currEnemyCount++;
        //newAttacker.SetLaneNo(laneNo);
    }

    public void SetAttackerPrefabs(Attacker[] attackerPrefabs)
    {
        attackerPrefabArray = attackerPrefabs;
    }
    public void SetMaxEnemyCount(int enemyCountPerWave)
    {
        maxEnemyCount = enemyCountPerWave;
    }
    public void SetWaitTime(float waitTime, float variance=0)
    {
        this.waitTime = waitTime;
        this.waitTimeVariance = variance;
    }
    // Update is called once per frame
    void Update()
    {
        if (currEnemyCount != maxEnemyCount) return;
        else if (transform.childCount == 0) Destroy(gameObject, 1f);
    }
    public void StopSpawning()
    {
        spawn = false;
    }
}

