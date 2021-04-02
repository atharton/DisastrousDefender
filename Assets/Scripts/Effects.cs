using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] bool rotating = false;
    [SerializeField] float rotatingSpeed = 360f;

    // Update is called once per frame
    void Update()
    {
        if (rotating) transform.Rotate(0,0,rotatingSpeed * Time.deltaTime);
    }
}
