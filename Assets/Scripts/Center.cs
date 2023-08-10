using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MoveEffectInterface
{
    private bool mIsRun;
    private GameObject mTarget;
    private Vector3? mStart;
    private Vector3? mEnd;
    private Vector2 mDir;
    private float FPS = 200f;
    private float moveX = 0f;
    private float moveY = 0f;

    public static int Count { get; private set; }
    public Center()
    {
        ++Count;
        mIsRun = false;
    }
    ~Center()
    {
        --Count;
    }
    public bool Run(GameObject _object, Vector3? _start, Vector3? _end)
    {
        //��� ��ü�� ���� ù ������ �� ���� �ʱ�ȭ
        if (mTarget == null)
        {
            mTarget = _object;
            mIsRun = true;
            mStart = _start;
            mEnd = _end;
            _object.transform.position = _start.Value;

            moveX = (mEnd.Value.x - _start.Value.x) / FPS;
            moveY = (mEnd.Value.y - _start.Value.y) / FPS;
        }
        mTarget.transform.position = CalculatePosition(mTarget.transform.position);

        if (mIsRun == false)
        {
            mTarget.transform.position = (Vector3)mEnd;
            return false;
        }
        return true;
    }

    public void Cancel()
    {
        if (mEnd != null)
        {
            mTarget.transform.position = (Vector3)mEnd;
            mIsRun = false;
        }
    }

    public bool IsEnd()
    {
        if (mIsRun) { return false; }
        return true;
    }

    //(x, y ��ǥ ���� ������ �������� ������ �� / ���� ����) �� �� ���ึ�� �����ش�. ������������ �����ߴٸ� ���� ��ȣ Ȱ��ȭ.
    private Vector3 CalculatePosition(Vector3 _now)
    {
        Vector3 ret = new Vector3();
        if ((_now - (Vector3)mEnd).magnitude <= 0.1f) { mIsRun = false; }
        else
        {
            ret.x = _now.x + moveX;
            ret.y = _now.y + moveY;
            ret.z = mEnd.Value.z;
        } 
        return ret;
    }
}
