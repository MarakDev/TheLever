using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Logica_Leviatan : H_Logica_Animal
{
    private float timeStampEclosion = 0;

    private int eclosionMaxScore = 100000;

    private bool eclosion = false;

    private void Start()
    {
        timeStamp = Time.time + helper.score_cooldown;
        timeStampEclosion = Time.time + 0.1f;
    }

    public override void Update()
    {
        if(eclosion)
            UpdateScore();
        else
            PasivePoints();

    }

    public override void Upgrade()
    {
        helper.score_levelUpgrade++;
    }

    public void PasivePoints()
    {
        if(eclosionMaxScore <= 0)
        {
            eclosion = true;
        }

        if(timeStampEclosion <= Time.time && !eclosion)
        {
            timeStamp = Time.time + 0.1f;

            eclosionMaxScore--;
        }
    }

    public void LeviatanEggOnClick()
    {
        if (!eclosion)
        {
            eclosionMaxScore--;
        }
    }
}
