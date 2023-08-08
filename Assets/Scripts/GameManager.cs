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

        void GameInit(int _stage)
        {
            /*AudioManager.audioManager.CancelMusic(AudioManager.MusicType.backGroundMusic2);
            AudioManager.audioManager.PlayMusic(AudioManager.MusicType.backGroundMusic1);*/
            stageLevel = _stage;
            timeLimit = 60.0f - (_stage -1) * 20.0f;
            tryNum = 0;
            selectTime = 5.0f;
            Time.timeScale = 1.0f;
            CardShuffle();
            bestTime = PlayerPrefs.GetFloat(stageLevel.ToString());
            recordUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = bestTime.ToString("f2");
            // 1�������� 8�� 3/2/3
            // 2�������� 16��  4 x 4

        }

        void Awake()
        {
            Init();
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
       
            // ī�� ������
            /*AudioManager.audioManager.PlayMusic(AudioManager.MusicType.Filp);*/
            _card.OpenCard();
            

        }
        void CardMatch()
        {
            tryNum++;
            tryUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tryNum.ToString();

            if (selectedCard1.data.Match(selectedCard2.data))
            {
                // UI �� ���
                // UI �ش� TMI�� �̸� ���
                /*AudioManager.audioManager.PlayMusic(AudioManager.MusicType.Success);*/
                selectedCard1.DestroyCard();
                selectedCard2.DestroyCard();
            }
            // ī�� ��Ī ���� 
            else
            {
                // GM �ð� ���� : ������ �ð��� ����
                // UI �ð� ���� ȿ�� ȣ�� : UI�� ���ؼ� txt����?
                // UI �ذ� ���
                // UI �� �ٲٱ�
                /*AudioManager.audioManager.PlayMusic(AudioManager.MusicType.Fail);*/
                // ���� �޼��� ���
                selectedCard1.CloseCard();
                selectedCard2.CloseCard();
            }

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
                    endX = cardSlot.transform.position.x + i % 4 * 1.4f;
                    endY = cardSlot.transform.position.y + i / 4 * 1.4f;
                }

                Vector3 endPos = new Vector3(endX, endY, 0);

                // ī���� ���������� ī���� ��ġ�� �޶����Ƿ�
                // ī���� position�� �׿����� ����Ǿ����.



                // UI ������ ī��ȿ�� ȣ��
                // ī�� ȿ���� ���� startPos�� ���ϰ� ȿ���� ������ endPos�� ����

                newcard.transform.position = endPos;
            }
        }

        void TimerEffect()
        {
            timeLimit -= Time.deltaTime;
            recordUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = timeLimit.ToString("f2");


            if (timeLimit < 10)
            {
                /*AudioManager.audioManager.CancelMusic(AudioManager.MusicType.backGroundMusic1);
                AudioManager.audioManager.PlayMusic(AudioManager.MusicType.backGroundMusic2);*/

                // UI �ð� ���� ���� ȿ��
                // timeLimit Image�� setTrue�� ����
            }
            else if (timeLimit < 0)
            {
                GameEnd();
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

                // ���� ���������� ����� ����
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
