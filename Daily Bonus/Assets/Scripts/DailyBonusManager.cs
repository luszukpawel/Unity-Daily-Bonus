using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusManager : MonoBehaviour
{

    private static DailyBonusManager _instance = null;
    [SerializeField] private GameObject DailyBonusPanel;
    [SerializeField] List<GameObject> Boxes = new List<GameObject>();
    [SerializeField] private Text CoinsAmountText;
    [SerializeField] private Text LevelsAmountText;
    [SerializeField] private GameObject CongratzPanel;

    // to be sure that not more that one instance of manager will be on scene
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        DontDestroyOnLoad(gameObject);

        //PlayerPrefs.DeleteAll();

        // Be Sure to not parse empty string 
        if (PlayerPrefs.GetString("DateOfLastPlayed") == String.Empty)
        {
            Debug.Log("First Data Initialization");
            PlayerPrefs.SetString("DateOfLastPlayed", DateTime.Now.ToString());
        }
    }
    void Start()
    {
        Sync();
        UpdateDisplay();
        //  Debug.Log("Day of bonus" + PlayerPrefs.GetInt("DayOfBonus"));
    }

    // to be sure that after pause bonus will be awarded
    void OnApplicationPause()
    {
        Debug.Log("After Pause Sync");
        Sync();
    }

    public void UpdateDisplay()
    {
        //  CoinsAmountText.text = PlayerVariables.CoinsAmount.ToString();
        CoinsAmountText.text = PlayerPrefs.GetInt("CoinAmount").ToString();
        LevelsAmountText.text = PlayerPrefs.GetInt("BonusLVLAmount").ToString();
    }
    public static DailyBonusManager Instance
    {
        get { return _instance; }
    }

    // check 
    void Sync()
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan ts = currentTime - Convert.ToDateTime(PlayerPrefs.GetString("DateOfLastPlayed"));

        if (ts.TotalHours > 24)
        {
            if (ts.TotalHours < 48)
            {
                // SetUpBoxes();
                PlayerPrefs.SetString("DateOfLastPlayed", DateTime.Now.ToString());
                PlayerPrefs.SetInt("DayOfBonus", PlayerPrefs.GetInt("DayOfBonus") + 1);
                DailyBonusPanel.SetActive(true);
                SetUpBoxes();
            }
            else
            {   // reset if is too late
                ResetBonus();
            }
        }

    }
    //set up boxes 
    void SetUpBoxes()
    {
        // reset if bonus > 7
        if (PlayerPrefs.GetInt("DayOfBonus") > 7) ResetBonus();

        // Debug.Log("Day of bonus" + PlayerPrefs.GetInt("DayOfBonus"));

        Box box;

        //set all boxes on the left from today as open
        for (int i = 0; i < PlayerPrefs.GetInt("DayOfBonus") - 1; i++)
        {
            box = Boxes[i].GetComponent<Box>();
            box.SetOpenBox();
        }
        // find first closed and set as Today
        box = Boxes.First(x => x.GetComponent<Box>().boxType == Box.BoxType.Closed).GetComponent<Box>();
        box.SetTodayBox();
    }
    // resets bonus
    public void ResetBonus()
    {
        PlayerPrefs.SetInt("DayOfBonus", 1);
        PlayerPrefs.SetString("DateOfLastPlayed", DateTime.Now.ToString());


        foreach (GameObject box in Boxes)
        {
            box.GetComponent<Box>().SetClosedBox();
        }
    }
    // gets bonus to player prefs
    public void GetBouns()
    {
        Box box = Boxes.First(x => x.GetComponent<Box>().boxType == Box.BoxType.Today).GetComponent<Box>();
        if (box.prizeType == Box.PrizeType.Coins)
        {
            PlayerPrefs.SetInt("CoinAmount", (PlayerPrefs.GetInt("CoinAmount") + box.CoinsAmount));
        }
        else
        {
            PlayerPrefs.SetInt("BonusLVLAmount", (PlayerPrefs.GetInt("BonusLVLAmount") + 1));
        }

        DailyBonusPanel.SetActive(false);
        CongratzPanel.SetActive(true);
        UpdateDisplay();
    }

    // gets bonus to player prefs
    public void GetBounsWithAdd()
    {
        Box box = Boxes.First(x => x.GetComponent<Box>().boxType == Box.BoxType.Today).GetComponent<Box>();
        if (box.prizeType == Box.PrizeType.Coins)
        {
            PlayerPrefs.SetInt("CoinAmount", (PlayerPrefs.GetInt("CoinAmount") + box.CoinsAmount * 2));
        }
        else
        {
            PlayerPrefs.SetInt("BonusLVLAmount", (PlayerPrefs.GetInt("BonusLVLAmount") + 2));
        }
        DailyBonusPanel.SetActive(false);
        CongratzPanel.SetActive(true);
        UpdateDisplay();
    }

    // takes -24h to last login variable
    public void SimulateNextDay()
    {
        DateTime newDateTime = Convert.ToDateTime(PlayerPrefs.GetString("DateOfLastPlayed")).AddHours(-24f);
        PlayerPrefs.SetString("DateOfLastPlayed", newDateTime.ToString());
        Sync();
    }

}



