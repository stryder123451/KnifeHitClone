using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int stage;
    public int bonus;
    public int score;
    public int record;
    // Start is called before the first frame update
    public PlayerData(UI player)
    {
        stage = player.stageMax;
        bonus = player.bonusAmount;
        score = player.scoreAmount;
        record = player.record;
    }
}
