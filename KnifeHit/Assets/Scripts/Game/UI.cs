using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public BallSpin spin;
    public GameObject scoreText;
    public GameObject knifeCountText;
    public GameObject coin;
    public int knifeCount;
    public List<GameObject> counters;
    public GameObject knife;
    public GameObject fakeKnife;
    public GameObject stageCounter;
    public GameObject stageText;
    public GameObject boss;
    public GameObject bonus;
    public GameObject enemy;
    public GameObject enemy1;
    public GameObject stageMenuText;
    public int localStage;
    public int lvlChange = 0;
    public bool bossBattle = false;
    int index = 0;
    public int bonusAmount;
    public int scoreAmount;
    public int stageMax;
    public int record;
    public BonusSpawn bonusSpawn;
    public GameObject debugScreen;
    public bool debugState;
    

    // Start is called before the first frame update
    void Start()
    {
       
        debugScreen.SetActive(false);
        string path = Application.persistentDataPath + "/progress.bin";
        if (File.Exists(path))
        {
            stageMax = Save.LoadPlayer().stage;
            bonusAmount = Save.LoadPlayer().bonus;
            record = Save.LoadPlayer().record;
           
        }
            Vibration.Init();
            enemy1.SetActive(false);
            enemy.SetActive(false);
            knifeCount = Random.Range(1, 5);
            boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

       
        scoreText.GetComponent<Text>().text = spin.score.ToString();
        knifeCountText.GetComponent<Text>().text = knifeCount.ToString();
        if (Input.GetMouseButtonDown(0)&&!debugState)
        {
            if (knifeCount > 0&&Input.mousePosition.x < Screen.height / 2)
            {
                --knifeCount;
            }
        }
    }

    private void FixedUpdate()
    {
      //  HPBar.GetComponent<Slider>().value = bossHp;

        if (spin.win)
        {
            
            if (index == 4)
            {
                if (bossBattle == false)
                {
                  
                  //  coin.GetComponent<Collider2D>().enabled = false;
                    spin.win = false;
                    knifeCount = 10;
                    boss.SetActive(true);
                   
                    coin.GetComponent<SpriteRenderer>().color = Color.red;
                    Instantiate(knife, transform.position, Quaternion.identity);
                    bossBattle = true;
                    if (Random.value > 0.5f)
                    {
                        enemy.SetActive(true);
                        enemy1.SetActive(true);
                    }
                    else
                    {
                        enemy.SetActive(true);
                    }
                   // bossHp = 100;
                }
                if (bossBattle&&knifeCount==0)
                {
                    coin.GetComponent<Animator>().SetBool("win", true);
                  //  coin.GetComponent<Collider2D>().enabled = true;
                    StartCoroutine(nextStage());
                    StartCoroutine(explosion());
                    bossBattle = false;
                    for (int i = 0; i <counters.Count; i++)
                    {
                        counters[i].GetComponent<Image>().color = Color.white;
                    }
                    index = 0;
                    
                }

            }
            else
            {
                coin.GetComponent<Animator>().SetBool("win", true);
                StartCoroutine(nextStage());
                StartCoroutine(explosion());
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    IEnumerator nextStage()
    {
        Vibration.VibratePop();
        scoreAmount = spin.score;
        if (scoreAmount > record)
        {
            record = scoreAmount;
        }
        
        boss.SetActive(false);
        coin.GetComponent<SpriteRenderer>().color = Color.white;
        enemy.SetActive(false);
        enemy1.SetActive(false);
        //
        if (!bossBattle)
        {
            counters[index].GetComponent<Image>().color = Color.green;
        }
        ++localStage;
        ++index;
        if (localStage > stageMax)
        {
            stageMax = localStage;
        }
        stageText.GetComponent<Text>().text = "Stage - " + localStage.ToString();
        stageMenuText.GetComponent<Text>().text = localStage.ToString();
        spin.win = false;
        Save.SavePlayer(this);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            var position = new Vector2(coin.transform.GetComponent<Renderer>().bounds.min.x, coin.transform.GetComponent<Renderer>().bounds.min.y);
            Instantiate(fakeKnife, position , Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
        if (Random.value < bonusSpawn.chance/100) //%25 percent chance
        {
            var position = new Vector2(Random.Range(coin.transform.GetComponent<Renderer>().bounds.min.x, coin.transform.GetComponent<Renderer>().bounds.max.x), Random.Range(coin.transform.GetComponent<Renderer>().bounds.min.y, coin.transform.GetComponent<Renderer>().bounds.max.y));
            Instantiate(bonus, position, Quaternion.identity);
        }

        Instantiate(knife, transform.position, Quaternion.identity);
        knifeCount = Random.Range(1, 7);
        yield return null;
    }

    IEnumerator explosion()
    {
        yield return new WaitForSeconds(0.8f);
        coin.GetComponent<Animator>().SetBool("win", false);
        yield return null;
    }

    public void DebugOn()
    {
        debugState = true;
        debugScreen.SetActive(true);
        Time.timeScale = 0;
       
    }

    public void DebugOff()
    {
        debugState = false;
        debugScreen.SetActive(false);
        Time.timeScale = 1;
        
    }

    public void DebugMenu()
    {
        if (!debugState)
        {
            DebugOn();
        }
        else
        {
            DebugOff();
        }
    }
 



}