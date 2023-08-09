using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Chae;
using KKS;

/// <summary>
/// newcard.GetComponent<CardObject>().data = new Chae.Card(string _cardname);
/// _cardname은 같은 카드인지 판별할 시에 사용한다.
/// </summary>
public class CardObject : MonoBehaviour
{
    public Card data;
    public Animator anim;

    public void OpenCard()
    {
        Invoke("OpenCardInvoke", 0.3f); 
    }

    void OpenCardInvoke()
    {
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        transform.Find("front").gameObject.SetActive(false);
        transform.Find("back").gameObject.SetActive(true);
        GameManager.Instance.isMatching = false;
     
    }

    void DestroyCardInvoke()
    {
        GameManager.Instance.cardNum -= 1;
        GameManager.Instance.isMatching = false;
        Destroy(gameObject);
    }
}