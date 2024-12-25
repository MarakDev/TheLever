using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpersDisplay : MonoBehaviour
{
    public Helpers helper;
    public HelpersLevelUp helperLevelUp;

    public TMP_Text currentName;
    public TMP_Text description;
    public TMP_Text upgrade;

    public Image artwork;
    public Image artworkUpgrade;

    public void OnEnable()
    {
        if (helper != null)
        {
            currentName.text = helper._Name;
            description.text = helper._Description;
            upgrade.text = helper._LevelUpgrade;

            artwork.sprite = helper._Artwork;
            artworkUpgrade.enabled = false;

        }
        else if (helperLevelUp != null)
        {
            currentName.text = helperLevelUp._Name + "+";
            description.text = helperLevelUp._DescriptionLevelUpgrade;

            artwork.sprite = helperLevelUp._Artwork;
            artworkUpgrade.enabled = true;
            artworkUpgrade.sprite = helperLevelUp._ArtworkUpgrade;
        }

    }

    public void ChoseHelperOnClick()
    {
        if (helper != null)
            LeverManager.Instance.TheLeverHelperListSpawn(helper._Name);
        else if (helperLevelUp != null)
            LeverManager.Instance.TheLeverHelperLevelUp(helperLevelUp._Name);

        LeverManager.Instance.DeactivateChoseOptionMenu();
    }
}
