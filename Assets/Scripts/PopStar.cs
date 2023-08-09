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
    /// ���࿡ position�� ������ position ��ġ�� ��Ÿ����. ���� ��쿡�� object�� ��ġ�� ��Ÿ����.
    /// �ذ� �������� ���ų�, ���۽ð��� ����Ǹ� false�� �����Ѵ�.
    /// </summary>
    /// <param name="_object">ǥ�ø� ��Ÿ���� ������Ʈ�� ��ġ�� ����մϴ�.</param>
    /// <param name="_position">��� �Ǹ�, ������ �켱������ ����մϴ�.</param>
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
                // mAppearTime ���� ��Ÿ����
                // mLastTime ���� �����ǰ�
                // mAppearTime ���� �������.
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
    /// delta��ŭ item�� alpha���� ��ȭ��Ų��.
    /// </summary>
    /// <param name="_item">������Ʈ�� ��������Ʈ ������</param>
    /// <param name="_alpha">���� ��ȭ��</param>
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
