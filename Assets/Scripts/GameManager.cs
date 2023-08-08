using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KKS;
using Chae;

namespace KKS
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance;
        public static GameManager Instance { get { Init(); return instance; } }

        static void Init()
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("GameManager");
                if (go == null)
                {
                    go = new GameObject("GameManager");
                    go.AddComponent<GameManager>();
                }
                instance = go.GetComponent<GameManager>();
            }
        }

        float timeLimit;
        float selectTime = 5.0f;
        int stageLeve��;
        int cardNum = 16;

        GameObject cardSlot;
        GameObject card;

        Card card1;
        Card card2;

        void GameInit()
        {
            // Audio ������� ���
            timeLimit = 60.0f;

        }

        void Awake()
        {
            Init();
            cardSlot = GameObject.Find("cardSlot");
            card = (GameObject)Resources.Load("Prefabs/card");
        }

        private void Start()
        {
            GameInit();
            CardShuffle();
        }

        // Update is called once per frame
        void Update()
        {
            TimerEffect();
            CardMatch();
        }

        public void SelectCard(GameObject go)
        {
            // ī�� == Button���� ī�尡 Ŭ���Ǹ� �̺�Ʈ�� �߻�

            // UI ī�� ������ ���� ȣ��
            // Audio ī�� ������ ���� ȣ��
        }
        void CardMatch()
        {
            if (card1 == null) return;

            selectTime -= Time.deltaTime;

            if (selectTime < 0)
            {
                //���� �ð� �ʰ�

                // card1�� �ٽ� ������ �����·� ����
                card1 = null;
                selectTime = 5.0f;
                return;

            }

            if (card2 == null) return;

            if (card1.Match(card2))
            {
                // UI �� ���
                // UI �ش� TMI�� �̸� ���
                // Audio ���� ���� ���

            }
            // ī�� ��Ī ���� 
            else
            {
                // GM �ð� ���� : ������ �ð��� ����
                // UI �ð� ���� ȿ�� ȣ�� : UI�� ���ؼ� txt����?
                // UI �ذ� ���
                // UI �� �ٲٱ�
                // Audio ���� ���� ���
                // ���� �޼��� ���
            }

            // �� ī�带 ��� �ٽ� ������ �����·� ���� 

            // ��������Դٸ� ��ī�带 �����ؼ� Match�� �õ��߱� ������ selecTime �ʱ�ȭ
            selectTime = 5.0f;
        }

        void CardShuffle()
        {
            int[] cards = new int[cardNum];

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = i / 2;
            }

            cards = cards.OrderBy(x => Random.Range(-1.0f, 1.0f)).ToArray();

            for (int i = 0; i < cards.Length; i++)
            {

                GameObject newcard = Instantiate(card);
                newcard.transform.parent = cardSlot.transform;
                newcard.AddComponent<CardObject>();

                float endX = cardSlot.transform.position.x + i % 4 * 1.2f;
                float endY = cardSlot.transform.position.y + i / 4 * 1.2f;
                Vector3 endPos = new Vector3(endX, endY, 0);

               /* newcard.transform.Find("front").GetComponent<SpriteRenderer>().sprite
                    = Resources.Load<Sprite>("Resources/Images/" + cards[i].ToString());*/

                newcard.GetComponent<CardObject>().data = new Card(cards[i].ToString());


                // UI ������ ī��ȿ�� ȣ��
                // ī�� ȿ���� ���� startPos�� ���ϰ� ȿ���� ������ endPos�� ����

                newcard.transform.position = endPos;
            }
        }

        void TimerEffect()
        {
            timeLimit -= Time.deltaTime;
            if (timeLimit < 10)
            {
                // Audio ����� ��������� ������� ����
            }
        }





    }

}
