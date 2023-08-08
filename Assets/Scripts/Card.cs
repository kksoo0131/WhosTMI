using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chae
{
    public interface CardInterface
    {
        public abstract bool Match(Card _card);
    }

    /// <summary>
    /// 카드의 정보에 대한 클래스.
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
        /// 카드의 ID를 비교해서 동일한 카드인지 확인
        /// </summary>
        /// <param name="_card1">첫번째 카드의 정보</param>
        /// <param name="_card2">두번째 카드의 정보</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">카드의 정보(Card instance)가 null인 경우에 예외 처리</exception>
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
        /// 카드의 ID를 비교해서 동일한 카드인지 확인
        /// </summary>
        /// <param name="_cardObject1">첫번째 카드의 gameobject</param>
        /// <param name="_cardObject2">두번째 카드의 gameobject</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">카드의 정보(Card instance)가 null인 경우에 예외 처리</exception>
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