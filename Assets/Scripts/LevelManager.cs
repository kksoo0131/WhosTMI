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
    private int maxLevel=4;
    public GameObject UP;
    public GameObject DOWN;
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
        {
            BestScoreText.text = "0.00";
            UP.SetActive(false);
        }
        DOWN.SetActive(false);
    }

    public void LevelUp()
    {
        if (PlayerPrefs.HasKey("stage" + (Level + 1).ToString() + "Score"))
        {
            float bestScore = PlayerPrefs.GetFloat("stage" + (Level + 1).ToString() + "Score");
            SetLevel(+1);
            BestScoreText.text = bestScore.ToString("F2");
        }
        else if (Level < maxLevel && PlayerPrefs.HasKey("stage" + Level.ToString() + "Score"))
        {
            SetLevel(+1);
            BestScoreText.text = "0.00";
            // 최대 스테이지로 변경됨.
            // UP 버튼 비활성화
            UP.SetActive(false);
        }
        else
        {
            // 현재 최대 스테이지
            // UP 버튼 비활성화
            UP.SetActive(false);
        }
    }

    public void LevelDown()
    {
        if (Level > 1 && PlayerPrefs.HasKey("stage" + (Level - 1).ToString() + "Score"))
        {
            float bestScore = PlayerPrefs.GetFloat("stage" + (Level - 1).ToString() + "Score");
            SetLevel(-1);
            BestScoreText.text = bestScore.ToString("F2");
            if (Level == 1) { DOWN.SetActive(false); }
        }
        else
        {
            // Level <= 1 인 경우
            // Down 버튼 비활성화
            DOWN.SetActive(false);
        }
    }

    private void SetLevel(int _level)
    {
        Level += _level;
        LevelText.text = Level.ToString();
        // level이 -1이면 UP버튼 활성화
        // level이 +1이면 Down버튼 활성화
        if (_level == 1) { DOWN.SetActive(true); }
        else if(_level == -1) { UP.SetActive(true); }
    }
}
