using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PopSkull : FixedEffectInterface
{
    private bool mIsRun;
    private GameObject mSkullPrefab;
    private GameObject mSkull;
    private float mTime;
    private SpriteRenderer[] mSpriteRenderer;
    private float mAppearTime = 0.2f;
    private float mLastTime = 0.5f;

    public PopSkull()
    {
        mSkullPrefab = Resources.Load<GameObject>("Prefabs/skull");
        mIsRun = false;
        mTime = 0f;
    }
    /// <summary>
    /// 만약에 position이 있으면 position 위치에 나타낸다. 없는 경우에는 object의 위치에 나타낸다.
    /// 해골 프리펩이 없거나, 동작시간이 만료되면 false를 리턴한다.
    /// </summary>
    /// <param name="_object">표시를 나타내는 오브젝트의 위치를 사용합니다.</param>
    /// <param name="_position">없어도 되며, 있으면 우선적으로 사용합니다.</param>
    /// <returns></returns>
    public bool Run(GameObject _object, Vector3? _position)
    {
        bool ret = true;
        if (mIsRun == false)
        {
            if (mSkullPrefab == null) { return false; }
            Debug.Log("PopSkull Start");
            mSkull = GameObject.Instantiate(mSkullPrefab);

            if (_position != null)
                mSkull.transform.position = _position.Value;
            else
                mSkull.transform.position = _object.transform.position;

            mSpriteRenderer = mSkull.transform.GetComponentsInChildren<SpriteRenderer>();

            foreach (var item in mSpriteRenderer)
            {
                Color tmp = item.color;
                tmp.a = 0f;
                item.color = tmp;
            }

            mIsRun = true;
        }
        else
        {
            mTime += Time.deltaTime;

            foreach (var item in mSpriteRenderer)
            {
                if (mTime <= mAppearTime)
                    Fade(item, mTime*2f); // Fadein
                else if (mTime >= mAppearTime + mLastTime)
                    Fade(item, (mAppearTime + mLastTime + mAppearTime) - (mTime * 2f)); // Fadeout
                // mAppearTime 동안 나타나고
                // mLastTime 동안 유지되고
                // mAppearTime 동안 사라진다.
            }
            if (mTime > mAppearTime + mLastTime + mAppearTime)
            {
                Debug.Log("PopSkull Done");
                GameObject.Destroy(mSkull);
                mSkull = null;
                ret = false;
            }
        }
        return ret;
    }

    /// <summary>
    /// delta만큼 item의 alpha값을 변화시킨다.
    /// </summary>
    /// <param name="_item">오브젝트의 스프라이트 랜더러</param>
    /// <param name="_delta">알파 변화값</param>
    private void Fade(SpriteRenderer _item, float _delta)
    {
        Color tmp = _item.color;
        tmp.a = Math.Clamp(tmp.a + _delta, 0f, 1f);
        _item.color = tmp;
    }

    public void Cancel()
    {
        if (mSkull != null) 
        {
            GameObject.Destroy(mSkull);
            mSkull = null;
        }
        Debug.Log("PopSkull Cancel");
    }

    public bool IsEnd()
    {
        bool ret = false;
        return ret;
    }
}
