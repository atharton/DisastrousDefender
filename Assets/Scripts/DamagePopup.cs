using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    private TextMeshPro text;
    private static int sortingOrder = 0;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount)
    {
        //transform.position = new Vector2(transform.position.x+a,transform.position.y);
        //a++;
        text.SetText(damageAmount.ToString());
        sortingOrder++;
        text.sortingOrder = sortingOrder;
    }
    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);
        return damagePopup;
    }

    private void Start()
    {
        Destroy(gameObject, 3f);
    }
}
