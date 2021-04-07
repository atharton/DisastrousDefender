using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public class ClickController : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] SpriteRenderer weaponDisplay;
    [SerializeField] int maxWeaponInstance = 1;
    Sprite weaponSpriteToDisplay;
    //[SerializeField] Vector2 mouseOffset =Vector2.zero;
    //GameObject weaponInstance;
    //Weapon weapon;
    DamageDealer damageDealer;
    // Start is called before the first frame update
    void Awake()
    {
        damageDealer = GetComponent<DamageDealer>();
        ChangeWeapon(weaponPrefab);
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

        GameObject weaponInstance;
        if (weaponPrefab.TryGetComponent(out Melee weapon)) {
            weaponInstance = Instantiate(weaponPrefab, cursorPos, Quaternion.identity);
            weaponInstance.transform.parent = transform;
        }
        else if (weaponPrefab.TryGetComponent(out Projectile projectile) )
        {
            Debug.Log("3"); 
            weaponInstance = Instantiate(weaponPrefab, projectile.origin, Quaternion.identity);
            Debug.Log(weaponInstance);
            weaponInstance.GetComponent<Projectile>().Initialize(cursorPos);
            weaponInstance.transform.parent = transform;
        }

    }

    
    public void ChangeWeapon(GameObject newWeaponPrefab)
    {
        weaponPrefab = newWeaponPrefab;
        if(weaponPrefab.TryGetComponent(out IWeapon weapon))
        {
           maxWeaponInstance = weapon.GetMaxWeaponInstance();
        }
        weaponDisplay.sprite = weaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public float GetDamage()
    {
        return damageDealer.GetDamage();
    }
}
