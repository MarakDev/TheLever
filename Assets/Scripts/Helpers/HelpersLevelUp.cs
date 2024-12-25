using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Helper LU", menuName = "HelpersLU/HelperLU_Default")]
public class HelpersLevelUp : ScriptableObject
{
    [Header("Datos")]
    [SerializeField] public string _Name;
    [TextArea(1, 10)]
    [SerializeField] public string _DescriptionLevelUpgrade;

    [SerializeField] public Sprite _ArtworkUpgrade;
    [SerializeField] public Sprite _Artwork;


}
