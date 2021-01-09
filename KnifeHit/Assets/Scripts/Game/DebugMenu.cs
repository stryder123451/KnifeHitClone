using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    public Text delayThrow;
    public Text speed;
    public Text bonusChance;
    public BonusSpawn bonus;
    public BallSpin ballSpin;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delayThrow.GetComponent<Text>().text =   bonus.daggerDelay.ToString();
        speed.GetComponent<Text>().text = ballSpin.ballSpeed.ToString();
        bonusChance.GetComponent<Text>().text = bonus.chance.ToString();
    }

    public void DecreaseDelay()
    {
        if (bonus.daggerDelay> 0)
        {
              bonus.daggerDelay--;
        }

       
    }
    public void IncreaseDelay()
    {
          bonus.daggerDelay++;
     
    }
    public void DecreaseChance()
    {
        if (bonus.chance > 0)
        {
            bonus.chance--;
        }
    }
    public void IncreaseChance()
    {
        bonus.chance++;
    }
    public void IncreaseSpeedRotation()
    {
        ballSpin.ballSpeed++;
    }
    public void DecreaseSpeedRotation()
    {
        ballSpin.ballSpeed--;
    }

}
