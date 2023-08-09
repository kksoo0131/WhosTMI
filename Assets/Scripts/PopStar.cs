using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopStar : FixedEffectInterface
{
    private bool mIsRun;
    private GameObject mStarPrefab;
    private GameObject mStar;
    private float mTime;
    private SpriteRenderer[] mSpriteRenderer;
    private float mAppearTime = 0.5f;
    private float mLastTime = 0.2f;

    public PopStar()
    {
        mStarPrefab = Resources.Load<GameObject>("Prefabs/star");
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
            if (mStarPrefab == null) { return false; }
            mStar = GameObject.Instantiate(mStarPrefab);

            if (_position != null)
                mStar.transform.position = _position.Value;
            else
                mStar.transform.position = _object.transform.position;

            mSpriteRenderer = mStar.transform.GetComponentsInChildren<SpriteRenderer>();

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
                    SetAlpha(item, mTime / mAppearTime); // Fadein
                else if (mTime >= mAppearTime + mLastTime)
                    SetAlpha(item, ((mAppearTime + mLastTime + mAppearTime) - (mTime)) / mAppearTime); // Fadeout
                // mAppearTime 동안 나타나고
                // mLastTime 동안 유지되고
                // mAppearTime 동안 사라진다.
            }
            if (mTime > mAppearTime + mLastTime + mAppearTime)
            {
                GameObject.Destroy(mStar);
                mStar = null;
                ret = false;
            }
        }
        return ret;
    }

    /// <summary>
    /// delta만큼 item의 alpha값을 변화시킨다.
    /// </summary>
    /// <param name="_item">오브젝트의 스프라이트 랜더러</param>
    /// <param name="_alpha">알파 변화값</param>
    private void SetAlpha(SpriteRenderer _item, float _alpha)
    {
        Color tmp = _item.color;
        tmp.a = Math.Clamp(_alpha, 0f, 1f);
        _item.color = tmp;
    }

    public void Cancel()
    {
        if (mStar != null)
        {
            GameObject.Destroy(mStar);
            mStar = null;
        }
    }

    public bool IsEnd()
    {
        bool ret = false;
        return ret;
    }
}
