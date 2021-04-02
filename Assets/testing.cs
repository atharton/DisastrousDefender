using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    private Rigidbody2D myRigidBody2D;
    [SerializeField] float forcePower;
    private void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 v2TP = transform.position;
        myRigidBody2D.MovePosition(v2TP+(Vector2.left*1*Time.deltaTime));
    }
    private void OnMouseDown()
    {
        myRigidBody2D.AddForce(Vector2.right*forcePower);
    }
}
