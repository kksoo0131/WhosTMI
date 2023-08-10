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
        public GameObject UI;

        float timeLimit;
        int stageLevel;
        int tryNum;
        float selectTime;
        float bestTime;
        bool isSelected;
        bool isTimeLimit;


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
            GameInit(LevelManager.instance.Level); 
        }

        // Update is called once per frame
        void Update()
        {
            TimerEffect();
            StageClear();
        }

        void GameInit(int _stage)
        {
            AudioManager.instance.PlayMusic(AudioManager.MusicType.backGroundMusic1);
            AudioManager.instance.CancelMusic(AudioManager.MusicType.backGroundMusic2);
            stageLevel = _stage;
            tryNum = 0;
            selectTime = 3.0f;
            CardShuffle();
            isTimeLimit = false;
            isSelected = false;
            stageUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _stage.ToString();
            bestTime = PlayerPrefs.GetFloat("stage"+stageLevel.ToString()+ "Score");
            recordUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = bestTime.ToString("f2");

            Time.timeScale = 1.0f;

            
        }

        public void SelectCard(CardObject _card)
        {
            if (isMatching) return;

            if (selectedCard1 != null && _card == selectedCard1) return;

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
            UIEffectManager.instance.StartEffect(_card.gameObject, UIEffectManager.UIType.Flip, new Vector3(0, 0, 0), _card.transform.position);
            _card.OpenCard();
        }
        
        void CardMatch()
        {
            tryNum++;
            tryUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tryNum.ToString();

            if (selectedCard1.data.Match(selectedCard2.data))
            {
                // UI 해당 TMI의 이름 출력
                Invoke("MatchSuccessInvoke", 0.5f);


            }
            else
            {
                Invoke("MatchFailInvoke", 0.5f);
            }

            
        }
        void MatchSuccessInvoke()
        {
            UIEffectManager.instance.StartEffect(selectedCard1.gameObject, UIEffectManager.UIType.PopupName, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            UIEffectManager.instance.StartEffect(selectedCard1.gameObject, UIEffectManager.UIType.PopupStar, new Vector3(0, 0, 0), selectedCard1.transform.position);
            UIEffectManager.instance.StartEffect(selectedCard2.gameObject, UIEffectManager.UIType.PopupStar, new Vector3(0, 0, 0), selectedCard1.transform.position);
            AudioManager.instance.PlayMusic(AudioManager.MusicType.Success);
            selectedCard1.DestroyCard();
            selectedCard2.DestroyCard();

            selectedCard1 = null;
            selectedCard2 = null;
    
        }

        void MatchFailInvoke()
        {
            timeLimit -= 1.0f;
            // 실패 메세지 출력
            // UI 색 바꾸기
            UIEffectManager.instance.StartEffect(selectedCard1.gameObject, UIEffectManager.UIType.PopupSkull, new Vector3(0, 0, 0), selectedCard1.transform.position);
            UIEffectManager.instance.StartEffect(selectedCard2.gameObject, UIEffectManager.UIType.PopupSkull, new Vector3(0, 0, 0), selectedCard1.transform.position);
            AudioManager.instance.PlayMusic(AudioManager.MusicType.Fail);

            UIEffectManager.instance.StartEffect(selectedCard1.gameObject.transform.GetChild(1).gameObject, UIEffectManager.UIType.ChangeColor, new Vector3(0, 0, 0), selectedCard1.transform.position);
            UIEffectManager.instance.StartEffect(selectedCard2.gameObject.transform.GetChild(1).gameObject, UIEffectManager.UIType.ChangeColor, new Vector3(0, 0, 0), selectedCard2.transform.position);
            selectedCard1.CloseCard();
            selectedCard2.CloseCard();

            selectedCard1 = null;
            selectedCard2 = null;
        }

        

        void CardShuffle()
        {
            switch (stageLevel)
            {
                case 1:
                    cardNum = 8;
                    timeLimit = 60.0f;
                    break;
                case 2:
                    cardNum = 16;
                    timeLimit = 60.0f;
                    break;
                case 3:
                    cardNum = 8;
                    timeLimit = 30.0f;
                    break;
                case 4:
                    cardNum = 16;
                    timeLimit = 30.0f;
                    break;
                default:
                    break;
            }
            
            int[] cards = new int[cardNum];

            List<int> numbers = Enumerable.Range(0, 9).ToList();
            List<int> selected = new List<int>();

            while(selected.Count() < cardNum/2)
            {
                int index = Random.Range(0, numbers.Count);
                int selectedNumber = numbers[index];
                selected.Add(selectedNumber);
                numbers.RemoveAt(index);
            }

            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = selected[i / 2];
            }
            /*
            cards = cards.OrderBy(x => Random.Range(-1.0f, 1.0f)).ToArray();
            */
            //다른 방법으로 카드 섞기.
            int random1, random2, temp;

            for (int i = 0; i < cards.Length ; ++i)
            {
                random1 = Random.Range(0, cards.Length);
                random2 = Random.Range(0, cards.Length);

                temp = cards[random1];
                cards[random1] = cards[random2];
                cards[random2] = temp;
            }

            for (int i = 0; i < cards.Length; i++)
            {

                GameObject newcard = Instantiate(card);
                newcard.GetComponent<CardObject>().data = new Card(cards[i].ToString());

                newcard.transform.Find("front").GetComponent<SpriteRenderer>().sprite
                     = Resources.Load<Sprite>("Images/" + cards[i].ToString());

                newcard.transform.parent = cardSlot.transform;
                float endX;
                float endY;

                if (stageLevel == 1|| stageLevel == 3)
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
                else if(stageLevel == 2 || stageLevel == 4)
                {
                    endX = cardSlot.transform.position.x -0.3f +i % 4 * 1.4f;
                    endY = cardSlot.transform.position.y -0.3f + i / 4 * 1.4f;
                }
                else
                {
                    endX = 0;
                    endY = 0;

                }

                Vector3 endPos = new Vector3(endX, endY, 0);

                //UI 움직임 카드효과 호출
                // 카드 효과에 따라서 startPos을 정하고 효과가 끝나면 endPos에 도달
                switch (stageLevel)
                {
                    case 1:
                        UIEffectManager.instance.StartEffect(newcard, UIEffectManager.UIType.MoveWave, new Vector3(0, 0, 0), endPos);
                        break;
                    case 2:
                        UIEffectManager.instance.StartEffect(newcard, UIEffectManager.UIType.MoveSpiral, new Vector3(0, 0, 0), endPos);
                        break;
                    case 3:
                        UIEffectManager.instance.StartEffect(newcard, UIEffectManager.UIType.MoveCenter, new Vector3(0, 0, 0), endPos);
                        break;
                    case 4:
                        UIEffectManager.instance.StartEffect(newcard, UIEffectManager.UIType.MoveSpiral, new Vector3(0, 0, 0), endPos);
                        break;
                }
            }
        }
       

        void TimerEffect()
        {
            timeLimit -= Time.deltaTime;
            if(timeLimit <= 0)
            {
                recordUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "0.00";
            }
            else
            {
                recordUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = timeLimit.ToString("f2");
            }



            if (timeLimit < 10)
            {

                if (timeLimit <= 0)
                {
                    timeLimit = 0;
                    GameEnd();
                    return;
                }

                if (isTimeLimit == false)
                {
                    isTimeLimit = true;
                    AudioManager.instance.CancelMusic(AudioManager.MusicType.backGroundMusic1);
                    AudioManager.instance.PlayMusic(AudioManager.MusicType.backGroundMusic2);
                    UIEffectManager.instance.StartEffect(recordUI.transform.GetChild(0).gameObject, UIEffectManager.UIType.ReduceTime, recordUI.transform.GetChild(0).transform.position);
                }
                
                
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
            

            if (cardNum == 0)
            {
                AudioManager.instance.CancelMusic(AudioManager.MusicType.backGroundMusic1);
                AudioManager.instance.CancelMusic(AudioManager.MusicType.backGroundMusic2);
                string stageKey = "stage" + stageLevel.ToString() + "Score";
                Time.timeScale = 0.0f;

                if (!PlayerPrefs.HasKey(stageKey) || PlayerPrefs.HasKey(stageKey) && PlayerPrefs.GetFloat(stageKey) < timeLimit)
                {
                    PlayerPrefs.SetFloat(stageKey, timeLimit);
                }
                
                
                GameInit(stageLevel + 1);

                // 현재 스테이지의 기록을 저장
            }

            if (stageLevel > 4)
            {

                GameEnd();
                return;
            }
        }

        void GameEnd()
        {
            AudioManager.instance.CancelMusic(AudioManager.MusicType.backGroundMusic1);
            AudioManager.instance.CancelMusic(AudioManager.MusicType.backGroundMusic2);
            Time.timeScale = 0.0f;
            resultUI.SetActive(true);
            resultUI.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = timeLimit.ToString("f2");
            resultUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = tryNum.ToString();
        }


    }

}
