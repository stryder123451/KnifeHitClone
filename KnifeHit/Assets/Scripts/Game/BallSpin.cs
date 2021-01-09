using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpin : MonoBehaviour
{
    public GameObject knife;
    public GameObject fakeKnife;
    public GameObject bonus;
    GameObject knifePos;
    public float ballSpeed = 3.0f;
    public GameObject scoreText;
    public GameObject maxScore;
    public int score;
    public bool win;
    public UI ui;
    public GameObject gameOverScreen;
    float nextTime = 0.0f;
    float modifier = 0;
    public GameObject bonusText;
  //  public GameObject recordText;
    public GameObject maxStage;
    public BonusSpawn bonSpawn;
    // Start is called before the first frame update
    void Start()
    {
       
        knifePos = GameObject.FindGameObjectWithTag("Respawn");
       
        gameOverScreen.SetActive(false);
        for (int i = 0; i < Random.Range(1,3); i++)
        {
            var position = new Vector2(transform.GetComponent<Renderer>().bounds.min.x, transform.GetComponent<Renderer>().bounds.min.y);
            Instantiate(fakeKnife, position*1.5f,Quaternion.identity);            
        }
        if (Random.value < bonSpawn.chance/100) //%25 percent chance
        {
            var posBonus = new Vector2(transform.GetComponent<Renderer>().bounds.max.x, transform.GetComponent<Renderer>().bounds.max.y);
            Instantiate(bonus, posBonus, Quaternion.identity);
        }     
    }

    // Update is called once per frame
    void Update()
    {
        maxStage.GetComponent<Text>().text = "Max stage : " + ui.stageMax;
        maxScore.GetComponent<Text>().text = "Record : " + ui.record;
        bonusText.GetComponent<Text>().text = ui.bonusAmount.ToString();
        if (modifier == 0)
        {
            modifier = Random.Range(5, 10);
            nextTime = Time.time + modifier;
        }
        if (Time.time > nextTime)
        {
            ballSpeed = Random.Range(-500, 500);
            modifier = 0;
        }
        transform.Rotate(Vector3.forward * ballSpeed * Time.deltaTime);
        if (win)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.CompareTag("Boss")&& !transform.GetChild(i).gameObject.CompareTag("HitBox"))
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
        }
        if (!win)
        {
           
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
        if (collision.collider.gameObject.CompareTag("knife"))
        {
            score++;
            scoreText.GetComponent<Text>().text ="Score : "+ score.ToString();
            if (ui.knifeCount == 0 )
            {
                win = true;           
            }
            else
            {
                Instantiate(knife, knifePos.transform.position, Quaternion.identity); 
            }
            
        }
    }

    public void GameOver()
    {
        
        Save.SavePlayer(ui);
        gameOverScreen.SetActive(true);
    }

    public void Bonus()
    {
        ui.bonusAmount++;
        bonusText.GetComponent<Text>().text = ui.bonusAmount.ToString();
    }

   
}
