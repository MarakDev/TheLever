using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeverManager : MonoBehaviour
{
    public static LeverManager Instance { get; private set; }

    //#region Constantes
    //public const string RARITY_COMMON = "Comun";
    //public const string RARITY_RARE = "Raro";
    //public const string RARITY_EXOTIC = "Exotica";
    //public const string RARITY_LEGENDARY = "Legendaria";

    //public const string TYPE_SCORE = "Score";
    //public const string TYPE_UPGRADE = "Upgrade";
    //public const string TYPE_CONSUMIBLE = "Consumable";

    //public const string TRIBE_ANIMAL = "Animal";
    //public const string TRIBE_MILITARY = "Militar";
    //public const string TRIBE_HUMAN = "Humano";


    //#endregion

    #region GameObjects

    [SerializeField] private GameObject lever_helpersMenuList;
    [SerializeField] private RectTransform lever_handler;
    [SerializeField] private Button lever_button;
    [SerializeField] private Slider lever_slider;
    [SerializeField] private TMP_Text lever_level_TXT;
    [SerializeField] private TMP_Text lever_score_TXT;

    [SerializeField] private List<GameObject> helpersOptionsButtons;

    [SerializeField] private int maxHelpersAvaliable;

    #endregion

    #region VariablesInternas

    [HideInInspector] public List<Helpers> helpersTotalList;
    [HideInInspector] public List<HelpersLevelUp> levelUpTotalList; 

    [HideInInspector] public float lever_level;
    [HideInInspector] public double lever_score;
    [HideInInspector] public double lever_totalScore;
    [HideInInspector] public List<H_Logica> currentHelpersList;
    [HideInInspector] public Dictionary<string, int> currentHelpersList_QuantityCounter;
    [HideInInspector] public Dictionary<string, int> currentHelpersList_LevelCounter;
    [HideInInspector] public Dictionary<string, GameObject> currentHelpersListCounter_visualList;


    private float cooldownClick = 0;
    [Header("")]
    [SerializeField] public float cooldownClickAmount; 
    [SerializeField] public float scalateValue;

    #endregion


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            NewGameStart();

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        NewLeverLevel();

        UpdateVisuals();
    }

    private void NewGameStart()
    {
        cooldownClick = Time.time + (cooldownClickAmount / 100);
        lever_handler.localEulerAngles = new Vector3(0, 0, 60);
        lever_slider.minValue = 0;
        lever_slider.maxValue = 100;
        lever_slider.value = 0;
        lever_level = 0;
        lever_score = 0;

        currentHelpersList = new List<H_Logica>();

        currentHelpersList_QuantityCounter = new Dictionary<string, int>();
        currentHelpersList_LevelCounter = new Dictionary<string, int>();

        currentHelpersListCounter_visualList = new Dictionary<string, GameObject>();

        Helpers[] helpersObjects = Resources.LoadAll<Helpers>("ScripteableObjects/Helpers/");
        HelpersLevelUp[] levelUpObjects = Resources.LoadAll<HelpersLevelUp>("ScripteableObjects/LevelUp/");

        helpersTotalList.AddRange(helpersObjects);
        levelUpTotalList.AddRange(levelUpObjects);
    }

    private void NewLeverLevel()
    {
        if(lever_score >= lever_slider.maxValue)
        {
            lever_handler.localEulerAngles = new Vector3(0, 0, 60);
            lever_slider.maxValue += scalateValue; //AQUI PARA CAMBIAR EL MAXIMO VALOR DE LA PALANCA
            lever_slider.value = 0;
            lever_score = 0;
            lever_level += 1;
            onAnimation_HelpersMenuOnClick = true;

            ActivateChoseOptionMenu();
        }
    }

    public void TheLeverOnClick()
    {
        if (cooldownClick <= Time.time)
        {
            cooldownClick = Time.time + (cooldownClickAmount / 100);
            UpdateScore(1);
        }
    }

    //permite utilizar el boton del menu helpers
    private bool onAnimation_HelpersMenuOnClick = false;
    public void HelpersMenuOnClick()
    {
        if (!onAnimation_HelpersMenuOnClick)
        {
            if(lever_helpersMenuList.GetComponent<RectTransform>().anchoredPosition.x >= 510)
                StartCoroutine(HelpersMenuAnimationEnter());
            else
                StartCoroutine(HelpersMenuAnimationExit());
        }
    }

    private IEnumerator HelpersMenuAnimationEnter()
    {
        //permite clickar en el menu de ver los helpers
        onAnimation_HelpersMenuOnClick = true;
        while (true)
        {
            Vector3 position = new Vector3(lever_helpersMenuList.GetComponent<RectTransform>().anchoredPosition.x - 10, 0, 0);

            lever_helpersMenuList.GetComponent<RectTransform>().anchoredPosition = position;
            yield return new WaitForSeconds(0.001f);

            if (lever_helpersMenuList.GetComponent<RectTransform>().anchoredPosition.x <= 280)
            {
                onAnimation_HelpersMenuOnClick = false;
                break;
            }
        }

    }
    private IEnumerator HelpersMenuAnimationExit()
    {
        onAnimation_HelpersMenuOnClick = true;
        while (true)
        {
            Vector3 position = new Vector3(lever_helpersMenuList.GetComponent<RectTransform>().anchoredPosition.x + 10, 0, 0);

            lever_helpersMenuList.GetComponent<RectTransform>().anchoredPosition = position;
            yield return new WaitForSeconds(0.001f);

            if (lever_helpersMenuList.GetComponent<RectTransform>().anchoredPosition.x >= 510)
            {
                onAnimation_HelpersMenuOnClick = false;
                break;
            }

        }
    }

    public void UpdateScore(double score)
    {
        lever_score += score;
        lever_totalScore += score;

        lever_handler.Rotate(0, 0, - (float)score * (120 / lever_slider.maxValue));

    }
    private void UpdateVisuals()
    {
        lever_slider.value = (float)lever_score;
        lever_level_TXT.text = "Actual Lever: " + lever_level.ToString();

        if (lever_totalScore > 100000000)
            lever_score_TXT.text = lever_totalScore.ToString("0.000e0");
        else
            lever_score_TXT.text = lever_totalScore.ToString("#,##0");

        if (lever_handler.localEulerAngles.z < -60)
        {
            lever_handler.localEulerAngles = new Vector3(0, 0, 60);
        }

    }

    private void ActivateChoseOptionMenu()
    {
        Time.timeScale = 0;
        lever_button.enabled = false;

        List<Helpers> listAvalibleHelpers = new List<Helpers>();

        foreach(GameObject button in helpersOptionsButtons)
        {

            //comprueba si se puede sumonear una upgrade si hay como minimo un helper de tipo score
            bool thereIsHelperScore = false;
            foreach (H_Logica thereIsScore in currentHelpersList)
            {
                if (thereIsScore.helper._Type.ToString() == LeverCONSTANTS.TYPE_SCORE){ 
                    thereIsHelperScore = true;
                    break; 
                }
            }

            int helperOrLevelUp = Random.Range(0, 100);
            //Debug.Log("thisroll: " + helperOrLevelUp);
            //se elige un helper 80% helper 20% mejora
            if ((helperOrLevelUp <= 80 || !thereIsHelperScore) && currentHelpersList.Count < maxHelpersAvaliable)
            {
                SeleccionHelper(listAvalibleHelpers, button);
            }
            //se elige un levelUP
            else
            {
                SeleccionLevelUp(button);

            }

            //debug para comprobar la lista de los actuales
            //foreach (Helpers debug in listAvalibleHelpers)
            //    Debug.Log("helper: " + debug._Name + " rarity: " + debug._Rarity + "\n");


            //activa ese boton solo si estan cargados un helper o un helperlevelup
            if (button.GetComponent<HelpersDisplay>().helper != null || button.GetComponent<HelpersDisplay>().helperLevelUp != null)
                button.SetActive(true);
        }
    }

    private void SeleccionHelper(List<Helpers> listAvalibleHelpers, GameObject button)
    {
        //selecciona la Rareza de manera aleatoria - 70% comun 27% raro 3% exotico
        int randomRarity = Random.Range(0, 100);
        string helperRarity = "";

        Debug.Log("rarity roll: " + randomRarity);

        if (randomRarity < 70)
            helperRarity = LeverCONSTANTS.RARITY_COMMON;
        if (randomRarity >= 70 && randomRarity < 97)
            helperRarity = LeverCONSTANTS.RARITY_RARE;
        if (randomRarity >= 97)
            helperRarity = LeverCONSTANTS.RARITY_EXOTIC;

        //comprueba y añade a una lista auxiliar los ayudantes con dicha rareza
        foreach (Helpers h in helpersTotalList)
        {
            //comprueba la rareza                       //quitar esto para mas adelante
            if (helperRarity == h._Rarity.ToString() && h._Type.ToString() != LeverCONSTANTS.TYPE_CONSUMIBLE)
            {
                //comprueba el tipo de helper que es, en caso de ser score lo añade sin mas
                if (h._Type.ToString() == LeverCONSTANTS.TYPE_SCORE)
                    listAvalibleHelpers.Add(h);

                //en caso de ser de tipo upgrade o consumible previamente tienes que tener un tipo score de la misma familia
                else if (h._Type.ToString() == LeverCONSTANTS.TYPE_UPGRADE || h._Type.ToString() == LeverCONSTANTS.TYPE_CONSUMIBLE)
                {
                    //comprueba en la lista actual de helpers de la partida para ver si coinciden en tribu
                    foreach (H_Logica currentHelpers in currentHelpersList)
                    {
                        //en caso de que sea correcto añade el helper upgrade/ consumible
                        if (currentHelpers.helper._Tribe == h._Tribe)
                        {
                            listAvalibleHelpers.Add(h);
                            break;
                        }
                    }
                }
            }
        }

        //toma el tamaño maximo de la lista auxiliar que hemos creado con los parametros anteriores
        int randomRange = Random.Range(0, listAvalibleHelpers.Count);

        //añade el helper al boton a/b/c
        button.GetComponent<HelpersDisplay>().helper = listAvalibleHelpers[randomRange];

        listAvalibleHelpers.Clear();
    }
    private void SeleccionLevelUp(GameObject button)
    {
        List<Helpers> helpersScoreAux = new List<Helpers>();
        //saca una lista auxiliar con todos los helpers de tipo score que pueden subir de nivel
        foreach (H_Logica helpersScore in currentHelpersList)
        {
            if (helpersScore.helper._Type.ToString() == LeverCONSTANTS.TYPE_SCORE)
                helpersScoreAux.Add(helpersScore.helper);
        }

        int randomLevelUp = Random.Range(0, helpersScoreAux.Count);

        foreach (HelpersLevelUp hlu in levelUpTotalList)
        {
            if (helpersScoreAux[randomLevelUp]._Name == hlu._Name)
            {
                //añade el levelup al boton a/b/c
                button.GetComponent<HelpersDisplay>().helperLevelUp = hlu;

                helpersScoreAux.Clear();

                break;
            }
        }
    }
    

    public void DeactivateChoseOptionMenu()
    {
        Time.timeScale = 1;
        lever_button.enabled = true;

        onAnimation_HelpersMenuOnClick = false;

        foreach (GameObject button in helpersOptionsButtons)
        {
            button.GetComponent<HelpersDisplay>().helper = null;
            button.GetComponent<HelpersDisplay>().helperLevelUp = null;

            button.SetActive(false);
        }
    }

    public void TheLeverHelperListSpawn(string nombre)
    {
        //busca en la carpeta prefabs el nombre del helper
        string prefabPath = "Prefabs/HelpersInMenu/" + nombre;
        //Debug.Log(nombre);
        GameObject prefab = Resources.Load<GameObject>(prefabPath);

        //INICIALIZA EL PREFAB Y LO AÑADE A LA LISTA DE HELPERS
        GameObject newHelper = Instantiate(prefab);
        currentHelpersList.Add(newHelper.GetComponent<H_Logica>());

        //Suma 1 en el contador de ayudantes, parte grafica
        lever_helpersMenuList.transform.Find("TotalHelpers").GetComponent<TMP_Text>().text = currentHelpersList.Count + "/" + maxHelpersAvaliable; //+ //maxHelpers;

        //asigna al ayudante el parent de la palanca para que aparezca en la UI
        newHelper.transform.SetParent(lever_helpersMenuList.transform);

        if (currentHelpersList_QuantityCounter.ContainsKey(nombre))
        {
            //añade el helper a la lista contador de helpers, si ya existe añade un +1 a su contador
            currentHelpersList_QuantityCounter[nombre]++;

            //desactiva el resto de componentes visuales.
            newHelper.GetComponent<Image>().enabled = false;
            newHelper.transform.GetChild(0).gameObject.SetActive(false);
            newHelper.transform.GetChild(1).gameObject.SetActive(false);
            newHelper.transform.GetChild(2).gameObject.SetActive(false);

            //actualiza los visuals de la lista de helpers
            currentHelpersListCounter_visualList[nombre].transform.GetChild(1).GetComponent<TMP_Text>().text = "x" + currentHelpersList_QuantityCounter[nombre];
        }
        else
        {
            //añade la primera instancia a la lista de helpers
            currentHelpersList_QuantityCounter[nombre] = 1;
            currentHelpersList_LevelCounter[nombre] = 1;

            //le asigna la posicion mas abajo posible.
            newHelper.GetComponent<RectTransform>().anchoredPosition = new Vector3(-80, 135 - (60 * currentHelpersListCounter_visualList.Count), 0);
            newHelper.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);

            newHelper.transform.GetChild(0).GetComponent<TMP_Text>().text = nombre;
            newHelper.transform.GetChild(1).GetComponent<TMP_Text>().text = "x1";
            newHelper.transform.GetChild(2).GetComponent<TMP_Text>().text = "Lvl: 1";

            currentHelpersListCounter_visualList[nombre] = newHelper;
        }

    }

    public void TheLeverHelperLevelUp(string nombre)
    {
        foreach (H_Logica hlu in currentHelpersList)
        {
            if (hlu.helper._Name == nombre)
            {
                hlu.Upgrade();
                break;
            }
        }

        currentHelpersList_LevelCounter[nombre]++;
        currentHelpersListCounter_visualList[nombre].transform.GetChild(2).GetComponent<TMP_Text>().text = "Lvl: " + currentHelpersList_LevelCounter[nombre];
    }
}
