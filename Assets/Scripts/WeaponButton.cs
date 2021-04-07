using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponButton : MonoBehaviour
{
    [SerializeField] GameObject WeaponPrefab;
    private void OnMouseDown()
    {
        var buttons = FindObjectsOfType<WeaponButton>();
        foreach (WeaponButton button in buttons)
        {
            button.GetComponentInChildren<SpriteRenderer>().color = new Color32(67,67,67,255);
        }
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
        FindObjectOfType<ClickController>().ChangeWeapon(WeaponPrefab);
    }
}
