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
                // mAppearTime ���� ��Ÿ����
                // mLastTime ���� �����ǰ�
                // mAppearTime ���� �������.
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
    /// delta��ŭ item�� alpha���� ��ȭ��Ų��.
    /// </summary>
    /// <param name="_item">������Ʈ�� ��������Ʈ ������</param>
    /// <param name="_delta">���� ��ȭ��</param>
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
