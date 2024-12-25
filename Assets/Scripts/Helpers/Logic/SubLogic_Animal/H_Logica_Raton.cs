using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Logica_Raton : H_Logica_Animal
{
    private float timeStampNegative = 0;
    private bool cheeseWasConsumed;

    private void Start()
    {
        timeStamp = Time.time + helper.score_cooldown;
        timeStampNegative = Time.time + 0.5f;
        cheeseWasConsumed = false;
    }

    public override void UpdateScore()
    {
        if (cheeseWasConsumed)
        {
            if (timeStamp <= Time.time)
            {
                timeStamp = Time.time + helper.score_cooldown;

                double currentPoints = helper.score_points * helper.score_levelUpgrade;
                currentPoints *= multiplicadorCuervo;

                LeverManager.Instance.UpdateScore(currentPoints);
            }
        }
        else
        {
            if(timeStampNegative <= Time.time)
            {
                timeStampNegative = Time.time + 0.5f;
                LeverManager.Instance.UpdateScore(-1);
            }
        }
    }

    public override void Upgrade()
    {
        helper.score_levelUpgrade++;
    }
}
