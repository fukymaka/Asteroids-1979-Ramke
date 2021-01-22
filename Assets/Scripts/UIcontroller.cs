using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIcontroller : MonoBehaviour
{
    static public UIcontroller S;
    static public int highScore = 0;

    [Header("Set in inspector")]
    public GameObject startUI;
    public GameObject gameUI;
    public GameObject settingsUI;
    public GameObject healthPointPrefab;
    public GameObject healthAnchor;
    public Text scoreText;
    public Text highScoreInGameText;
    public Text highScoreInStartScreenText;
    public Text roundText;
    public Text loseText;
    public Text musicOnOffText;
    public Text effectsOnOffText;

    private GameObject[] healths = new GameObject[20];


    private void Awake()
    {
        S = this;

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        PlayerPrefs.SetInt("HighScore", highScore);
    }


    public void StartNewGame()
    {
        startUI.SetActive(false);
        gameUI.SetActive(true);       

        Main.S.round = 0;
        Main.S.score = 0;
        Main.S.health = 5;
        Main.S.healthUpCount = 1;

        for (int i = 0; i < Main.S.health; i++)
        {
            AddHealth(i);
        }

        Main.S.StartRound();

        SoundsCtrl.S.PlayClickSound();
    }

    public void AddHealth(int i)
    {
        healths[i] = Instantiate(healthPointPrefab);
        healths[i].transform.SetParent(healthAnchor.transform);
        healths[i].transform.localPosition = new Vector2(120 + i * 30, 0);
    }

    public void DelHealth(int i)
    {
        Destroy(healths[i]);
    }


    public void DelayedRestartGame()
    {
        loseText.gameObject.SetActive(true);
        Invoke("RestartGame", 3);
    }


    public void ResetScore()
    {
        SoundsCtrl.S.PlayClickSound();
        PlayerPrefs.SetInt("HighScore", 0);
    }

    public void QuiteGame()
    {
        SoundsCtrl.S.PlayClickSound();
        Application.Quit();
    }


    public void SettingsMenu()
    {
        SoundsCtrl.S.PlayClickSound();

        if (!settingsUI.activeInHierarchy)
        {
            settingsUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            settingsUI.SetActive(false);
            Time.timeScale = 1;
        }
    }


    public void MusicOnOff(bool enabled)
    {
        SoundsCtrl.S.PlayClickSound();
        SoundsCtrl.S.PlayMainThemeMusic(enabled);

        if (enabled)
        {
            musicOnOffText.text = "Music on";            
        }
        else
        {
            musicOnOffText.text = "Music off";
        }
    }


    public void EffectsOnOff(bool enabled)
    {
        SoundsCtrl.S.PlayClickSound();
        SoundsCtrl.S.PlayEffectsSound(enabled);

        if (enabled)
        {
            effectsOnOffText.text = "Effects on";
        }
        else
        {
            effectsOnOffText.text = "Effects off";
        }
    }


    private void RestartGame()
    {
        SceneManager.LoadScene("Scene_0");
    }



    private void Update()
    {
        scoreText.text = "score: " + Main.S.score;
        highScoreInGameText.text = "highscore: " + highScore;
        highScoreInStartScreenText.text = "your highscore: " + highScore;
        roundText.text = "round: " + Main.S.round;

        if (Main.S.score >= PlayerPrefs.GetInt("HighScore"))
        {
            highScore = Main.S.score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        if (Input.GetKeyDown("escape"))  
        {
            SettingsMenu();
        }
    }
}
