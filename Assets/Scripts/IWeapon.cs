using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon : MonoBehaviour, IWeapon
{

    protected int damageMultiplier = 1;
    public Weapon()
    {
    }

    
     public virtual int GetMaxWeaponInstance()
    {
        return 1;
    }
    
}
public interface IWeapon 
{
    public int GetMaxWeaponInstance();

}
