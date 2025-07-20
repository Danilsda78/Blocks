using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIContainer : MonoBehaviour
{
    [Header("Button")]
    public Button[] ButtonMenu;
    public Button ButtonStart;
    public Button ButtonStop;
    public Button ButtonPlay;
    public Button[] ButtonRestart;
    public Button ButtonADS;
    [Header("Panel")]
    public GameObject UiMenuPanel;
    public GameObject UiLosePanel;
    public GameObject UiGamePanel;
    public GameObject UiStopPanel;
    [Header("Score")]
    public List<TMP_Text> BestScore;
    public List<TMP_Text> TempScore;
    [Header("Timer")]
    public Slider Timer;
    [Header("YG")]
    public LeaderboardYG LeaderboardYG;
}