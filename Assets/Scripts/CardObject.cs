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
    /// ī���� ������ ���� Ŭ����.
    /// </summary>
    public class Card : CardInterface
    {
#nullable enable
        private string? mId;
        private static int mCount;

        public static int Count { get => mCount; }
        public string? Name { get => mId; }
        public Card(string _cardname)
        {
            ++mCount;
            mId = _cardname;
        }
        ~Card()
        {
            --mCount;
        }

        public bool Match(Card _card)
        {
            bool ret = false;
            if (mId != null && _card != null)
            {
                if (mId == _card.Name) { ret = true; }
            }
            else
                throw new System.Exception("Null Name or Card Data");
            return ret;
        }
        /// <summary>
        /// ī���� ID�� ���ؼ� ������ ī������ Ȯ��
        /// </summary>
        /// <param name="_card1">ù��° ī���� ����</param>
        /// <param name="_card2">�ι�° ī���� ����</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">ī���� ����(Card instance)�� null�� ��쿡 ���� ó��</exception>
        public static bool Match(Card _card1, Card _card2)
        {
            bool ret = false;
            if (_card1 != null && _card2 != null)
            {
                if (_card1.Name == _card2.Name) { ret = true; }
            }
            else
                throw new System.Exception("Null Card Data");
            return ret;
        }
        /// <summary>
        /// ī���� ID�� ���ؼ� ������ ī������ Ȯ��
        /// </summary>
        /// <param name="_cardObject1">ù��° ī���� gameobject</param>
        /// <param name="_cardObject2">�ι�° ī���� gameobject</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">ī���� ����(Card instance)�� null�� ��쿡 ���� ó��</exception>
        public static bool Match(CardObject _cardObject1, CardObject _cardObject2)
        {
            bool ret = false;
            if (_cardObject1.data != null && _cardObject2.data != null)
            {
                if (_cardObject1.data.Name == _cardObject2.data.Name) { ret = true; }
            }
            else
                throw new System.Exception("Null Card Data");
            return ret;
        }
#nullable disable
    }
}
/// <summary>
/// newcard.GetComponent<CardObject>().data = new Chae.Card(string _cardname);
/// _cardname�� ���� ī������ �Ǻ��� �ÿ� ����Ѵ�.
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