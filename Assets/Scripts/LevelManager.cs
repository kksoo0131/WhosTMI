using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    public TMP_Text LevelText;
    public TMP_Text BestScoreText;
    public int Level { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Level = 1;
            //DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("stage" + 1 + "Score"))
        {
            float bestScore = PlayerPrefs.GetFloat("stage" + 1 + "Score");
            BestScoreText.text = bestScore.ToString("F2");
        }
        else
            BestScoreText.text = "0.00";
    }

    public void LevelUp()
    {
        if(PlayerPrefs.HasKey("stage" + (Level+1).ToString() + "Score"))
        {
            float bestScore = PlayerPrefs.GetFloat("stage" + (Level + 1).ToString() + "Score");
            SetLevel(Level + 1);
            BestScoreText.text = bestScore.ToString("F2");
        }
        else if (PlayerPrefs.HasKey("stage" + Level.ToString() + "Score"))
        {
            SetLevel(Level + 1);
            BestScoreText.text = "0.00";
        }
    }

    public void LevelDown()
    {
        if (Level > 1 && PlayerPrefs.HasKey("stage" + (Level - 1).ToString() + "Score"))
        {
            float bestScore = PlayerPrefs.GetFloat("stage" + (Level - 1).ToString() + "Score");
            SetLevel(Level - 1);
            BestScoreText.text = bestScore.ToString("F2");
        }
    }

    private void SetLevel(int _level)
    {
        LevelText.text = _level.ToString();
        Level = _level;
    }
}
