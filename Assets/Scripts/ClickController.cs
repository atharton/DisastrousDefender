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
        GameObject weaponInstance = Instantiate(weaponPrefab, cursorPos, Quaternion.identity);
        weaponInstance.transform.parent = transform;
    }

    public void ChangeWeapon(GameObject newWeaponPrefab)
    {
        weaponPrefab = newWeaponPrefab;
        weaponDisplay.sprite = weaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public float GetDamage()
    {
        return damageDealer.GetDamage();
    }
}
