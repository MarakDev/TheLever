using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Helper", menuName = "Helpers/Helper_Default")]
public class Helpers : ScriptableObject
{
    [Header("Datos")]
    [SerializeField] public string _Name;
    [TextArea(1,10)]
    [SerializeField] public string _Description;
    [TextArea(1, 10)]
    [SerializeField] public string _LevelUpgrade;

    public enum Rarity
    {
        Comun,
        Rare,
        Exotic,
        Legendary
    };

    [SerializeField] public Rarity _Rarity;

    public enum Type
    {
        Score,
        Upgrade,
        Consumable
    };

    [SerializeField] public Type _Type;
    public enum Tribe
    {
        Animal,
        Human,
        Militar
    };

    [SerializeField] public Tribe _Tribe;

    [SerializeField] public Sprite _Artwork;

    [Header("Stats")]
    [SerializeField] public double score_points;
    [SerializeField] public float score_cooldown;

    [SerializeField] public int score_levelUpgrade;

    private void OnEnable()
    {
        score_levelUpgrade = 1;
    }
}
