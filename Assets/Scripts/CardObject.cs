using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Chae;

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

    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {

    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }
}