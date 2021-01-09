using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public GameObject coin;
    bool move;
    bool hit;
    // Start is called before the first frame update
    void Start()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move && !hit)
        {
            transform.position = Vector3.MoveTowards(transform.position, coin.transform.position, 15 * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("main"))
        {
            Debug.Log("Hit!!!");
            move = false;
            hit = true;
        }
    }
}
