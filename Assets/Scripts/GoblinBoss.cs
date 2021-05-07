using UnityEngine;

public class GoblinBoss : Attacker
{
    [SerializeField] GameObject reinforcementPrefab;
    [SerializeField] int reinforcementAmount=5;
    int skillUsage = 1;
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (myHealth.GetCurrentHealth()<= 0.5f*myHealth.GetMaxHealth())
        {
            Debug.Log("Started a skill");
            if (skillUsage > 0) SetSkillAnimatorTrue();
        }
    }

    void CallReinforcement()
    {
        // summon some goblins
        int midAmount = (reinforcementAmount + 1) / 2;
        for (int i = 1; i <= reinforcementAmount; i++) 
        {
            float offsetX = 2+Random.Range(0,1.5f);
            float offsetY = (i - midAmount)*0.65f;
            Vector2 goblinPos = new Vector2(transform.position.x+offsetX, transform.position.y+offsetY);
            GameObject minion = Instantiate(reinforcementPrefab,goblinPos,Quaternion.identity);
            minion.transform.parent = transform.parent;
        }
        skillUsage--;
    }

    void SetSkillAnimator(bool cond)
    {
        myAnimator.SetBool("isUsingSkill", cond);
    }
    void SetSkillAnimatorTrue()
    {
        SetSkillAnimator(true);
    }
    void SetSkillAnimatorFalse()
    {
        SetSkillAnimator(false);
    }
}
