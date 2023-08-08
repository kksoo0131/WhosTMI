using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiral : MoveEffectInterface
{
    private bool mIsRun;
    private GameObject mTarget;
    private Vector3? mStart;
    private Vector3? mEnd;
    private Vector2 mDir;
    private float mAngleSpeed;
    private float mSpeed;
    public static int Count { get; private set; }
    public Spiral()
    {
        ++Count;
        mIsRun = false;
        mAngleSpeed = 0.1f;
        mSpeed = 1.0f;
    }
    ~Spiral()
    {
        --Count;
    }
    public bool Run(GameObject _object, Vector3? _start, Vector3? _end)
    {
        if (mTarget == null)
        {
            mTarget = _object;
            mIsRun = true;
            mStart = _start;
            mEnd = _end;
            mDir = (Vector2)(mEnd - mStart);
            mDir.Normalize();
        }
        mTarget.transform.position = CalculatePosition(mTarget.transform.position);

        if (mIsRun)
        {
            Cancel();
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

    private Vector3 CalculatePosition(Vector3 _now)
    {
        Vector3 ret = new Vector3();
        float r1 = (_now - (Vector3)mStart).magnitude;
        float r2 = r1 - mSpeed * Time.deltaTime;
        float divisor = r2 / r1;
        if (r1 < 0.01f) { mIsRun = false; }
        else
        {
            ret.x = (_now.x * (1 - mAngleSpeed * Time.deltaTime / 2) + _now.y * mAngleSpeed * Time.deltaTime) / divisor;
            ret.y = (_now.y * (1 - mAngleSpeed * Time.deltaTime / 2) + _now.x * mAngleSpeed * Time.deltaTime) / divisor;
            ret.z = _now.z;
        }
        return ret;
    }
}
