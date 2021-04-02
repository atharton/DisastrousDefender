using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArt : MonoBehaviour
{
    [SerializeField] Vector2 moveDistance;
    [SerializeField] float moveSpeed = 100f;
    [SerializeField] Canvas myCanvas;
    Vector2 startPos;
    bool startMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        myCanvas = FindObjectOfType<Canvas>();
        StartCoroutine(WaitAndMove(1f));
    }
    private void Update()
    {
        if(startMoving)Move();
    }
    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPos+moveDistance, moveSpeed*Time.deltaTime);
    }

    public void Return()
    {
        transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
    }
    IEnumerator WaitAndMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        startMoving = true;
    }
}
