using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using KKS;
using Chae;

namespace KKS
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        public int cardNum;
        public bool isMatching = false;

        public GameObject stageUI;
        public GameObject recordUI;
        public GameObject tryUI;
        public GameObject resultUI;

        float timeLimit;
        int stageLevel;
        int tryNum;
        float selectTime;
        float bestTime;
        bool isSelected = false;
        
        GameObject cardSlot;
        GameObject card;
        CardObject selectedCard1;
        CardObject selectedCard2;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            cardSlot = GameObject.Find("cardSlot");
            card = (GameObject)Resources.Load("Prefabs/card");
        }

        private void Start()
        {
            GameInit(1); 
        }

        // Update is called once per frame
        void Update()
        {
                TimerEffect();
                StageClear();
        }

        void GameInit(int _stage)
        {
            AudioManager.instance.CancelMusic(AudioManager.MusicType.backGroundMusic2);
            AudioManager.instance.PlayMusic(AudioManager.MusicType.backGroundMusic1);
            stageLevel = _stage;
            timeLimit = 60.0f - (_stage - 1) * 20.0f;
            tryNum = 0;
            selectTime = 5.0f;
            Time.timeScale = 1.0f;
            CardShuffle();
            bestTime = PlayerPrefs.GetFloat(stageLevel.ToString());
            recordUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = bestTime.ToString("f2");
            // 1스테이지 8개 3/2/3
            // 2스테이지 16개  4 x 4

        }

        public void SelectCard(CardObject _card)
        {
            if (isMatching) return;

            if (!isSelected)
            {
                selectedCard1 = _card;
                isSelected = true;
            }
            else if (selectedCard2 == null)
            {
                selectedCard2 = _card;
                isMatching = true;
                isSelected = false;
                selectTime = 5.0f;
                CardMatch();
            }

            AudioManager.instance.PlayMusic(AudioManager.MusicType.Flip);
            UIEffectManager.instance.StartEffect(_card.gameObject, (UIEffectManager.UIType)5, new Vector3(0, 0, 0), _card.transform.position);
            _card.OpenCard();
            

        }
        void CardMatch()
        {
            tryNum++;
            tryUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tryNum.ToString();

            if (selectedCard1.data.Match(selectedCard2.data))
            {
                // UI 별 출력
                // UI 해당 TMI의 이름 출력
                AudioManager.instance.PlayMusic(AudioManager.MusicType.Success);
                selectedCard1.DestroyCard();
                selectedCard2.DestroyCard();
                selectedCard1 = null;
                selectedCard2 = null;
            }
            // 카드 매칭 실패 
            else
            {
                Invoke("MatchFailInvoke", 0.5f);
            }

            
        }

        void MatchFailInvoke()
        {
            timeLimit -= 1.0f; // 실패할 경우 남은 시간 감소
            UIEffectManager.instance.StartEffect(selectedCard1.gameObject, (UIEffectManager.UIType)3, new Vector3(0, 0, 0), selectedCard1.transform.position);
            UIEffectManager.instance.StartEffect(selectedCard2.gameObject, (UIEffectManager.UIType)3, new Vector3(0, 0, 0), selectedCard1.transform.position);
            // UI 색 바꾸기
            AudioManager.instance.PlayMusic(AudioManager.MusicType.Fail);
            // 실패 메세지 출력
            selectedCard1.CloseCard();
            selectedCard2.CloseCard();

            selectedCard1 = null;
            selectedCard2 = null;
        }

        void CardShuffle()
        {
            if (stageLevel == 1)
            {
                cardNum = 8;
            }
            else
            {
                cardNum = 16;
            }

            int[] cards = new int[cardNum];

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = i / 2;
            }

            cards = cards.OrderBy(x => Random.Range(-1.0f, 1.0f)).ToArray();

            for (int i = 0; i < cards.Length; i++)
            {

                GameObject newcard = Instantiate(card);
                newcard.GetComponent<CardObject>().data = new Card(cards[i].ToString());

                newcard.transform.Find("front").GetComponent<SpriteRenderer>().sprite
                     = Resources.Load<Sprite>("Images/rtan" + cards[i].ToString());

                newcard.transform.parent = cardSlot.transform;
                float endX;
                float endY;

                if (stageLevel == 1)
                {
                    if (i < 3)
                    {
                        endX = cardSlot.transform.position.x + 0.6f + i % 3 * 1.4f;
                        endY = cardSlot.transform.position.y ;
                    }
                    else if (i < 5)
                    {
                        endX = cardSlot.transform.position.x + 1.2f + i % 2 * 1.4f;
                        endY = cardSlot.transform.position.y + 1  * 1.4f;
                    }
                    else
                    {
                        endX = cardSlot.transform.position.x + 0.6f + i % 3 * 1.4f;
                        endY = cardSlot.transform.position.y + 2 * 1.4f;
                    }
                        
                }
                else
                {
                    endX = cardSlot.transform.position.x -0.3f +i % 4 * 1.4f;
                    endY = cardSlot.transform.position.y -0.3f + i / 4 * 1.4f;
                }

                Vector3 endPos = new Vector3(endX, endY, 0); // 카드가 최종적으로 도착할 위치

                //UI 움직임 카드효과 호출
                // 카드 효과에 따라서 startPos을 정하고 효과가 끝나면 endPos에 도달

                MakeCardEffect(newcard, endPos);



            }
        }
        
        void MakeCardEffect(GameObject _object,Vector3 endPos )
        {
            switch (stageLevel)
            {
                case 1:
                    UIEffectManager.instance.StartEffect(_object, (UIEffectManager.UIType)0, new Vector3(0, 0, 0), endPos);
                    break;
                case 2:
                    UIEffectManager.instance.StartEffect(_object, (UIEffectManager.UIType)1, new Vector3(0, 0, 0), endPos);
                    break;
            }        
        }

        void TimerEffect()
        {
            timeLimit -= Time.deltaTime;
            recordUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = timeLimit.ToString("f2");


            if (timeLimit < 10)
            {

                if (timeLimit < 0)
                {
                    GameEnd();
                    return;
                }
                AudioManager.instance.CancelMusic(AudioManager.MusicType.backGroundMusic1);
                AudioManager.instance.PlayMusic(AudioManager.MusicType.backGroundMusic2);
                UIEffectManager.instance.StartEffect(recordUI.transform.GetChild(0).gameObject, (UIEffectManager.UIType)4, recordUI.transform.GetChild(0).transform.position);
            }
             

            if (isSelected)
            {
                selectTime -= Time.deltaTime;

                if (selectTime <= 0)
                {
                    selectedCard1.CloseCard();
                    selectedCard1 = null;
                    isSelected = false;
                    selectTime = 5.0f;
                }
            }
                
        }

        void StageClear()
        {
            AudioManager.instance.CancelMusic(AudioManager.MusicType.backGroundMusic1);
            AudioManager.instance.CancelMusic(AudioManager.MusicType.backGroundMusic2);

            if (stageLevel > 2)
            {
                GameEnd();
                return;
            }

            if (cardNum == 0)
            {
                string stageKey = stageLevel.ToString();
                Time.timeScale = 0.0f;

                if (!PlayerPrefs.HasKey(stageKey) || PlayerPrefs.HasKey(stageKey) && PlayerPrefs.GetFloat(stageKey) < timeLimit)
                {
                    PlayerPrefs.SetFloat(stageKey, timeLimit);
                }
                
                
                GameInit(stageLevel + 1);

                // 현재 스테이지의 기록을 저장
            }
        }

        void GameEnd()
        {
            Time.timeScale = 0.0f;
            resultUI.SetActive(true);
            resultUI.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = timeLimit.ToString("f2");
            resultUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = tryNum.ToString();
        }


    }

}
