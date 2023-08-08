using Chae;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Chae
{
    public interface CardInterface
    {
        public abstract bool Match(Card _card);
    }

    /// <summary>
    /// 
    /// </summary>
    public class Card : CardInterface
    {
#nullable enable
        private string? mName;
        //private int? mId;
        private static int mCount;

        public static int Count { get => mCount; }
        public string? Name { get => mName; }
        //public int? Id { get => mId; }
        public Card(string _cardname)
        {
            ++mCount;
            mName = _cardname;
        }
        //public CardData(int _id)
        //{
        //    ++mCount;
        //    mId = _id;
        //}
        ~Card()
        {
            --mCount;
        }

        public bool Match(Card _card)
        {
            bool ret = false;
            return ret;
        }
        public static bool Match(Card _card1, Card _card2)
        {
            bool ret = false;
            return ret;
        }
#nullable disable
    }
}
/// <summary>
/// gameobject.GetComponent<Card>().data = new Chae.CardData(string _cardname);
/// _cardname은 같은 카드인지 판별할 시에 사용한다.
/// </summary>
    public class CardObject : MonoBehaviour
{
    public Chae.Card data;
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