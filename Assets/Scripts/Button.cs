using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KKS;

public class Button : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ReadyBtn()
    {
        SceneManager.LoadScene("LevelScene");
    }
    public void EndBtn()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void SelectCard()
    { 
        GameManager.Instance.SelectCard(gameObject.GetComponent<CardObject>());
    }
    
}
