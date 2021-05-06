using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}
public interface IDamageableByAlly : IDamageable
{
}

public interface IDamageableByEnemy : IDamageable
{
}


