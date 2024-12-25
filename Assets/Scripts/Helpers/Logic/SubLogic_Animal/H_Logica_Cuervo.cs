using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Logica_Cuervo : H_Logica_Animal
{
    private float timeStampUT = 0;

    private bool enterBoostMode = false;

    private float timeStampCooldown = 60;
    private float timeStampUpgradeTime = 10;

    private void Start()
    {
        timeStamp = Time.time + helper.score_cooldown;

    }

    public override void Update()
    {
        StartBoostTime();

        ExitBoostTime();
    }

    public void StartBoostTime()
    {
        if (timeStamp <= Time.time)
        {
            enterBoostMode = true;
            timeStamp = Time.time + timeStampCooldown;
            timeStampUT = Time.time + timeStampUpgradeTime;

            Debug.Log("MODO ANIMAL");

            multiplicadorCuervo *= 2;

        }

    }

    public void ExitBoostTime()
    {
        if (timeStampUT <= Time.time && enterBoostMode)
        {
            enterBoostMode = false;
            Debug.Log("MODO EXIT ANIMAL");

            
            multiplicadorCuervo /= 2;

        }
    }
}
