using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon : MonoBehaviour, IWeapon
{
    //private int maxWeaponInstance;
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
