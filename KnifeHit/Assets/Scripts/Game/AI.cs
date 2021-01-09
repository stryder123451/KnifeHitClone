using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour
{

    public GameObject pos1;
    Vector3 origPos;
    public bool changeL;
    public bool changeR;
    float nextTime = 0.0f;
    float modifier = 0;
    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = 3f * Time.deltaTime;

        if (modifier == 0)
        {
            modifier = Random.Range(1, 5);
            nextTime = Time.time + modifier;
        }
        if (Time.time > nextTime)
        {
            step = Random.Range(3, 10);
            modifier = 0;
        }

        if (transform.position == pos1.transform.position)
        {
            changeL = true;
            changeR = false;
        }
        if (transform.position == origPos)
        {
            changeR = true;
            changeL = false;
        }
        if (!changeL || !changeR)
        {
            if (changeR)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos1.transform.position, step);
            }
            if (changeL)
            {
                transform.position = Vector3.MoveTowards(transform.position, origPos, step);
            }
        }
       


    }
}

   
