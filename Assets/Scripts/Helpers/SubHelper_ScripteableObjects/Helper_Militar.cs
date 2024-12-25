using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Helper", menuName = "Helpers/Helper_Militar")]
public class Helper_Militar : Helpers
{
    [Header("Parametros Militar")]
    [SerializeField] public float failProbability;
    [HideInInspector] public float current_FailProbability;

    private void OnEnable()
    {
        current_FailProbability = failProbability;
    }
}
