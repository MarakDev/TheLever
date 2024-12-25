using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Logica_Militar : H_Logica
{
    protected Helper_Militar helperMilitar;

    private void Start()
    {
        helperMilitar = (Helper_Militar)helper;

        timeStamp = Time.time + helperMilitar.score_cooldown;
    }

    public override void UpdateScore()
    {
        if (timeStamp <= Time.time)
        {
            int random = Random.Range(0, 100);

            timeStamp = Time.time + helperMilitar.score_cooldown;

            double currentPoints;
            if (helperMilitar.score_levelUpgrade < 1)
                currentPoints = helperMilitar.score_points * helperMilitar.score_levelUpgrade * 0.5f;
            else
                currentPoints = helperMilitar.score_points;

            Debug.Log("current %: " + helperMilitar.current_FailProbability);
            if (random >= helperMilitar.current_FailProbability)
                LeverManager.Instance.UpdateScore(currentPoints);
        }
    }

    public override void Upgrade()
    {
        helperMilitar.score_levelUpgrade++;

        if (helperMilitar.current_FailProbability > 0)
        {
            helperMilitar.current_FailProbability -= 1;
        }

    }
}
