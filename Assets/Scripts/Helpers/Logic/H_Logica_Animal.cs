using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Logica_Animal : H_Logica
{
    protected static int multiplicadorCuervo = 1;

    private void Start()
    {
        timeStamp = Time.time + helper.score_cooldown;
    }

    public override void UpdateScore()
    {
        if (timeStamp <= Time.time)
        {
            timeStamp = Time.time + helper.score_cooldown;

            double currentPoints = helper.score_points * helper.score_levelUpgrade;
            currentPoints *= multiplicadorCuervo;

            Debug.Log(name + " puntos realizados: " + currentPoints);

            LeverManager.Instance.UpdateScore(currentPoints);
        }
    }

    public override void Upgrade()
    {
        helper.score_levelUpgrade++;
    }
}
