using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance { get { Init();  return instance; } }

    float timeLimit;
    int stageLeve��;
    int cardNum = 16;

    GameObject cardSlot;
    GameObject card;
    Card card1;
    Card card2;
    AudioManager audioManager;
    UIEffectManager uIEffectManager;

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if(go == null)
            {
                go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            instance = go.GetComponent<GameManager>();
        }    
    }

    void GameInit()
    {
        timeLimit = 60.0f;

    }

    void Awake()
    {
        Init();
    }

    private void Start()
    {
        CardShuffle();
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;

        // ī�� ��Ī ����
        if (card1.Match(card2)) 
        {
            // UI �� ���
            // Audio ���� ���� ���
        }
        // ī�� ��Ī ���� 
        else
        {
            // UI �ذ� ���
            // UI �� �ٲٱ�
            // Audio ���� ���� ���
        }
 
    }

    void CardShuffle()
    {
        int[] cards = new int[cardNum];

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = i / 2; // ���ڸ� n�� ī�带 �ִ´�.
        }

        cards = cards.OrderBy(x => Random.Range(-1.0f, 1.0f)).ToArray();
        
        for(int i =0; i< cards.Length; i++)
        {
            GameObject newcard = Instantiate(card);
            newcard.transform.parent = cardSlot.transform;
            float nextX = cardSlot.transform.position.x + i % 4 * 1.4f;
            float nextY = cardSlot.transform.position.y + i % 4 * 1.4f;
            newcard.transform.position = new Vector3(nextX, nextY, 0);

            newcard.transform.Find("front").GetComponent<SpriteRenderer>().sprite
                = Resources.Load<Sprite>("" + cards[i].ToString());
        }
        
   

    }


    

    
}
