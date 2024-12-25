using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Logica : MonoBehaviour, H_Logica_INTERFACE
{
    [SerializeField] public Helpers helper;
    protected float timeStamp = 0;
    public virtual void UpdateScore()
    {
    }

    public virtual void Upgrade()
    {
    }

    public virtual void Update()
    {
        UpdateScore();
    }
}
