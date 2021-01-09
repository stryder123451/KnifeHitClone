using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeKnife : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 200f;
    GameObject target;
    bool hit;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite =transform.GetChild(0).GetComponent<SpriteRenderer>();
        target = GameObject.Find("Coin");
        rb = GetComponent<Rigidbody2D>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {  

        if (collision.collider.gameObject.CompareTag("main"))
        {
            Debug.Log("Hit!!!");          
            hit = true;
            sprite.enabled = true;
        }

    }

    private void FixedUpdate()
    {
        if (target.activeSelf)
        {
            if (!hit)
            {
                Vector2 direction = (Vector2)target.transform.position - rb.position;
                direction.Normalize();
                float rotateAmount = Vector3.Cross(direction, transform.up).z;
                rb.angularVelocity = -rotateAmount * rotateSpeed;
                rb.velocity = transform.up * speed*Time.deltaTime;
            }
            if (hit)
            {
                rb.freezeRotation = true;
            }
        }
       

    }

  

}
