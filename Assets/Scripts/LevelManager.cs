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
            // �ִ� ���������� �����.
            // UP ��ư ��Ȱ��ȭ
            UP.SetActive(false);
        }
        else
        {
            // ���� �ִ� ��������
            // UP ��ư ��Ȱ��ȭ
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
            // Level <= 1 �� ���
            // Down ��ư ��Ȱ��ȭ
            DOWN.SetActive(false);
        }
    }

    private void SetLevel(int _level)
    {
        Level += _level;
        LevelText.text = Level.ToString();
        // level�� -1�̸� UP��ư Ȱ��ȭ
        // level�� +1�̸� Down��ư Ȱ��ȭ
        if (_level == 1) { DOWN.SetActive(true); }
        else if(_level == -1) { UP.SetActive(true); }
    }
}
