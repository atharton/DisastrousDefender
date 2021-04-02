using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;
    [Range(1, 5)] [SerializeField] int laneNo;
    [SerializeField] float startingWaitTime = 2f;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float waitTimeVariance = 1f;
    [SerializeField] Attacker[] attackerPrefabArray;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(startingWaitTime);
        while (spawn)
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
        newAttacker.SetLaneNo(laneNo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StopSpawning()
    {
        spawn = false;
    }
    public int GetLaneNo()
    {
        return laneNo;
    }
}

