using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public class ClickController : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] SpriteRenderer weaponDisplay;
    //[SerializeField] Vector2 mouseOffset =Vector2.zero;
    //GameObject weaponInstance;
    //Weapon weapon;
    DamageDealer damageDealer;
    int maxWeaponInstance = 1 ;
    // Start is called before the first frame update
    void Awake()
    {
        damageDealer = GetComponent<DamageDealer>();
        //Cursor.visible = false;
        //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //weaponInstance = Instantiate(weaponPrefab, cursorPos, Quaternion.identity);
        //weapon = weaponInstance.GetComponent<Weapon>();
        //weaponDisplay.sprite = weapon.GetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //weaponInstance.transform.position = cursorPos-mouseOffset;
    }

    private void OnMouseDown()
    {
        if (transform.childCount > maxWeaponInstance) return;
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject weaponInstance = Instantiate(weaponPrefab, cursorPos, Quaternion.identity);
        weaponInstance.transform.parent = transform;
    }

    public void AttackWithWeapon(Attacker attacker, Rigidbody2D attackerRigidBody2D)
    {
        //weapon.Attack();
        //attacker.TakeDamage(weapon.GetDamage());
        //Debug.Log("I deal " + weapon.GetDamage().ToString());
    }

    private IEnumerator Knockback(Rigidbody2D attackerRigidBody2D,Vector2 startPos, Vector2 endPos, int noOfFrames)
    {
        // somehow lerp the motion into 'noOfFrames' pieces
        yield return new WaitForSeconds(1);
    } 
    public float GetDamage()
    {
        return damageDealer.GetDamage();
    }
}
