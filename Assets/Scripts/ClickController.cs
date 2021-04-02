using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] Vector2 mouseOffset =Vector2.zero;
    [SerializeField] float baseKnockback = 1f;
    GameObject weaponInstance;
    Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        weaponInstance = Instantiate(weaponPrefab, cursorPos, Quaternion.identity);
        weapon = weaponInstance.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        weaponInstance.transform.position = cursorPos-mouseOffset;

    }

    public void AttackWithWeapon(Attacker attacker, Rigidbody2D attackerRigidBody2D)
    {
        weapon.Attack();
        attacker.TakeDamage(weapon.GetDamage());
        Vector2 attackerCurrentPos =  attacker.transform.position;
        Debug.Log(attackerCurrentPos);
        Debug.Log(attackerCurrentPos + weapon.GetKnockback());
        attackerRigidBody2D.MovePosition(Vector2.zero);
        Debug.Log(weapon.GetKnockback());
        //attackerRigidBody2D.AddForce(weapon.GetKnockback()* baseKnockback,ForceMode2D.Impulse);
        Debug.Log("I deal " + weapon.GetDamage().ToString());
    }

    private IEnumerator Knockback(Rigidbody2D attackerRigidBody2D,Vector2 startPos, Vector2 endPos, int noOfFrames)
    {
        // somehow lerp the motion into 'noOfFrames' pieces
        yield return new WaitForSeconds(1);
    } 
}
