using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour, IDamageable
{    private void Awake()
    {
        //TakeDamage(1);
    }
    public virtual void TakeDamage(int damage)
    {
        Debug.Log("original func");
    }
}

public class testingDerived : testing
{
    private void Awake()
    {
        //TakeDamage(1);
    }
    public override void TakeDamage(int damage)
    {
        Debug.Log("derived func");
    }
}
