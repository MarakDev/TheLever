using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Logica_Human : H_Logica
{

    private void Start()
    {
        timeStamp = Time.time + helper.score_cooldown;
    }

    public override void UpdateScore()
    {
        
        if (timeStamp <= Time.time)
        {
            timeStamp = Time.time + helper.score_cooldown;

            //puntos x 2 ^ nivel
            double currentPoints = helper.score_points * Mathf.Pow(2,helper.score_levelUpgrade-1);

            Debug.Log("puntos currentes: " + currentPoints);

            LeverManager.Instance.UpdateScore(currentPoints);
        }
    }

    public override void Upgrade()
    {
        helper.score_levelUpgrade++;
    }
}
