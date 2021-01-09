using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throw : MonoBehaviour
{
    public GameObject coin;
    public GameObject knifePos;
    bool move;
    bool hit;
    public UI ui;
    public Text stageTextOver;
    public Collider2D[] colliders;
    public BonusSpawn bonus;
    // Start is called before the first frame update
    void Start()
    {
        Vibration.Init();
        // scoreText.GetComponent<Text>().text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

      
        if (move&&!hit)
        {
            transform.position = Vector3.MoveTowards(transform.position, coin.transform.position, 15 * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0)&&ui.knifeCount>=0)
        {
            if (Input.mousePosition.x < Screen.height / 2)
            {
                StartCoroutine(throwDelay());
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("main"))
        {
            Debug.Log("Hit!!!");
            move = false;
            hit = true;
            Vibration.VibratePop();
        }
        if (collision.collider.gameObject.CompareTag("Bonus"))
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, 100);
            for (int i = 0; i < colliders.Length; i++)
            {
                Collider2D x = colliders[i];
                x.SendMessage("Bonus", SendMessageOptions.DontRequireReceiver);
            }
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.gameObject.CompareTag("knife") || (collision.collider.gameObject.CompareTag("obstacle"))|| (collision.collider.gameObject.CompareTag("Enemy")))
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, 100);
            for (int i = 0; i < colliders.Length; i++)
            {
                Collider2D x = colliders[i];
                x.SendMessage("GameOver", SendMessageOptions.DontRequireReceiver);
            }
            Time.timeScale = 0;
            Vibration.VibratePop();
        }
       
    }

    IEnumerator throwDelay()
    {
        yield return new WaitForSeconds(bonus.daggerDelay);
        move = true;
       

    }




}
