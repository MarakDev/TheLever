using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Logica_Dragon : H_Logica_Animal
{

    private void Start()
    {
        timeStamp = Time.time + helper.score_cooldown;

    }


}
