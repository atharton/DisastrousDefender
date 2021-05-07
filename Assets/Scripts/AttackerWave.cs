using UnityEngine;

[CreateAssetMenu(fileName = "AttackerWave", menuName = "Create New Wave", order = 3)]
public class AttackerWave : ScriptableObject
{
    public Attacker[] attackerPrefabArray;
    public int[] enemyCountPerPos;
    public float[] waitTimePerPos;
    public float[] waitTimeVariancePerPos;

}

