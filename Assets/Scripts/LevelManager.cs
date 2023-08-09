using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public TMP_Text LevelText;
    public TMP_Text BestScoreText;
    public int Level { get; private set; }
    private int MaxLevel=2;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Level = 1;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
        // Best Score를 검사해서 MaxLevel를 결정
    }

    public void LevelUp()
    {
        SetLevel(Math.Clamp(Level + 1, 1, MaxLevel));
        LevelText.text = Level.ToString();
        //BestScoreText.text = Level.ToString() + ".00";
        // 난이도에 따른 베스트 스코어 표현하기
    }

    public void LevelDown()
    {
        SetLevel(Math.Clamp(Level - 1, 1, MaxLevel));
        LevelText.text = Level.ToString();
        //BestScoreText.text = Level.ToString() + ".00";
        // 난이도에 따른 베스트 스코어 표현하기
    }

    private void SetLevel(int _level)
    {
        Level = _level;
    }
}
