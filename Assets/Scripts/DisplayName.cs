using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : FixedEffectInterface
{
    private bool isRun = false;
    private GameObject dnPrefab;
    private GameObject dn;
    private Animator displayName;
    private float mTime;
    private SpriteRenderer[] mSpriteRenderer;
    private float mAppearTime = 0.2f;
    private float mLastTime = 0.5f;

    //Vector3 position;
    public DisplayName()
    {
        dnPrefab = Resources.Load<GameObject>("Prefabs/nameDisplay");
        isRun = false;
        mTime = 0f;
    }

    //포지션을 입력받았다면 해당 위치로, 포지션을 입력받지 못 했다면 해당 오브젝트의 기본 위치로.
    public bool Run(GameObject _object, Vector3? _position)
    {
        bool running = true; 

        //실행 중이 아니라면
        if (!isRun)
        {
            if (dnPrefab == null) { return false; }
            dn = GameObject.Instantiate(dnPrefab);

            if (_position != null)
            {
                dn.transform.position = _position.Value;
            }
            else
            {
                dn.transform.position = _object.transform.position;
            }

            mSpriteRenderer = dn.transform.GetComponentsInChildren<SpriteRenderer>();

            //알파값을 0으로 == 투명하게 보이도록
            foreach (var item in mSpriteRenderer)
            {
                Color tmp = item.color;
                tmp.a = 0f;
                item.color = tmp;
            }
            var txt = _object.GetComponent<CardObject>().data.Name;
            dn.transform.GetChild(0).GetComponent<Text>().text = Convert.ToInt32(txt) < 3 ? "김광수" : Convert.ToInt32(txt) < 6 ? "채이환" : "조영오";
            isRun = true;
        }
        else
        {
            mTime += Time.deltaTime;

            foreach (var item in mSpriteRenderer)
            {
                if (mTime <= mAppearTime)
                    SetAlpha(item, mTime/ mAppearTime); // Fadein
                else if (mTime >= mAppearTime + mLastTime)
                    SetAlpha(item, ((mAppearTime + mLastTime + mAppearTime) - (mTime))/ mAppearTime); // Fadeout
                // mAppearTime 동안 나타나고
                // mLastTime 동안 유지되고
                // mAppearTime 동안 사라진다.
            }
            if (mTime > mAppearTime + mLastTime + mAppearTime)
            {
                GameObject.Destroy(dn);
                dn = null;
                running = false;
            }
        }
        
        return running;
    }

    private void SetAlpha(SpriteRenderer _item, float _delta)
    {
        Color tmp = _item.color;
        tmp.a = Math.Clamp(_delta, 0f, 1f);
        _item.color = tmp;
    }

    public void Cancel()
    {
        if (dn != null)
        {
            GameObject.Destroy(dn);
            dn = null;
        }
    }

    public bool IsEnd()
    {
        return false;
    }
}
