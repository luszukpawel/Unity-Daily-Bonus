using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public enum PrizeType { Coins, Level };
    public enum BoxType { Closed, Today, Open };
    public String Day;
    public PrizeType prizeType;
    public BoxType boxType;
    public int CoinsAmount;
    public int LevelsAmount;
    [SerializeField] private Text DayText;
    [SerializeField] private Text CoinsText;
    [SerializeField] private GameObject CoinsPanel;
    [SerializeField] private GameObject LevelPanel;

    [SerializeField] private GameObject ClosedBoxImage;
    [SerializeField] private GameObject TodayBoxImage;
    [SerializeField] private GameObject OpenBoxImage;

    // init boxes colors and types
    void Start()
    {
        DayText.text = Day;
        CoinsText.text = CoinsAmount.ToString();

        if (prizeType == PrizeType.Coins) SetPrizePanelCoins();
        if (prizeType == PrizeType.Level) SetPrizePanelLevel();
        if (boxType == BoxType.Open) SetOpenBox();
        if (boxType == BoxType.Today) SetTodayBox();
        if (boxType == BoxType.Closed) SetClosedBox();
    }

    public void SetPrizePanelCoins()
    {
        CoinsPanel.SetActive(true);
        LevelPanel.SetActive(false);
    }

    public void SetPrizePanelLevel()
    {
        CoinsPanel.SetActive(false);
        LevelPanel.SetActive(true);
    }

    public void SetOpenBox()
    {
        DayText.text = Day;
        boxType = BoxType.Open;
        ClosedBoxImage.SetActive(false);
        TodayBoxImage.SetActive(false);
        OpenBoxImage.SetActive(true);
    }

    public void SetTodayBox()
    {
        DayText.text = "DZIŚ";
        boxType = BoxType.Today;
        ClosedBoxImage.SetActive(false);
        TodayBoxImage.SetActive(true);
        OpenBoxImage.SetActive(false);
    }
    public void SetClosedBox()
    {
        DayText.text = Day;
        boxType = BoxType.Closed;
        ClosedBoxImage.SetActive(true);
        TodayBoxImage.SetActive(false);
        OpenBoxImage.SetActive(false);
    }





}
