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
        int stageLeveㅣ;
        int cardNum = 16;

        GameObject cardSlot;
        GameObject card;

        Card card1;
        Card card2;

        void GameInit()
        {
            // Audio 배경음악 재생
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
            // 카드 == Button으로 카드가 클릭되면 이벤트로 발생

            // UI 카드 뒤집기 연출 호출
            // Audio 카드 뒤집기 사운드 호출
        }
        void CardMatch()
        {
            if (card1 == null) return;

            selectTime -= Time.deltaTime;

            if (selectTime < 0)
            {
                //선택 시간 초과

                // card1를 다시 뒤집어 원상태로 복구
                card1 = null;
                selectTime = 5.0f;
                return;

            }

            if (card2 == null) return;

            if (card1.Match(card2))
            {
                // UI 별 출력
                // UI 해당 TMI의 이름 출력
                // Audio 성공 사운드 출력

            }
            // 카드 매칭 실패 
            else
            {
                // GM 시간 감소 : 실제로 시간이 감소
                // UI 시간 감소 효과 호출 : UI를 통해서 txt수정?
                // UI 해골 출력
                // UI 색 바꾸기
                // Audio 실패 사운드 출력
                // 실패 메세지 출력
            }

            // 두 카드를 모두 다시 뒤집어 원상태로 복구 

            // 여기까지왔다면 두카드를 선택해서 Match를 시도했기 때문에 selecTime 초기화
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


                // UI 움직임 카드효과 호출
                // 카드 효과에 따라서 startPos을 정하고 효과가 끝나면 endPos에 도달

                newcard.transform.position = endPos;
            }
        }

        void TimerEffect()
        {
            timeLimit -= Time.deltaTime;
            if (timeLimit < 10)
            {
                // Audio 긴박한 배경음으로 배경음을 변경
            }
        }





    }

}
