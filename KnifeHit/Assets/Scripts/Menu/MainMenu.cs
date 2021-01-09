using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject fakeKnife;
    float nextTime = 0.0f;
    float modifier = 0;
    float ballSpeed=300;
    public int stageMax;
    public int record;
    public int bonusAmount;
    public Text stage;
    public Text bonus;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/progress.bin";
        if (File.Exists(path))
        {
            stageMax = Save.LoadPlayer().stage;
            bonusAmount = Save.LoadPlayer().bonus;
            record = Save.LoadPlayer().record;
        }
        stage.text = stageMax.ToString();
        score.text = record.ToString();
        bonus.text = bonusAmount.ToString();
        
        for (int i = 0; i < 3; i++)
        {
            var position = new Vector2(transform.GetComponent<Renderer>().bounds.min.x, transform.GetComponent<Renderer>().bounds.min.y);
            Instantiate(fakeKnife, position * 3f, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * ballSpeed * Time.deltaTime);
        if (modifier == 0)
        {
            modifier = Random.Range(5, 10);
            nextTime = Time.time + modifier;
        }
        if (Time.time > nextTime)
        {
            ballSpeed = Random.Range(-500, 500);
            var position = new Vector2(Random.Range(transform.GetComponent<Renderer>().bounds.min.x,transform.GetComponent<Renderer>().bounds.max.x), Random.Range(transform.GetComponent<Renderer>().bounds.min.y, transform.GetComponent<Renderer>().bounds.max.y));
            Instantiate(fakeKnife, position * 10f, Quaternion.identity);
            modifier = 0;
        }
        if (transform.childCount > 5)
        {
            for (int i = 0; i < transform.childCount; i++)
            {         
              Destroy(transform.GetChild(i).gameObject); 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
