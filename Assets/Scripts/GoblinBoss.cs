using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBoss : Attacker
{
    int skillUsage = 1;
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Debug.Log("curhealth : " + myHealth.GetCurrentHealth());
        Debug.Log("0.5 maxhealth : " + 0.5f * myHealth.GetMaxHealth());
        if (myHealth.GetCurrentHealth()<= 0.5f*myHealth.GetMaxHealth())
        {
            Debug.Log("Started a skill");
            if (skillUsage > 0) CallReinforcement();
        }
    }

    void CallReinforcement()
    {
        // summon some goblins
        Debug.Log("CALL GOBLINS");
        skillUsage--;
    }
}
