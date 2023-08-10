using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
        mAngleSpeed = 2f;
        mSpeed = 5f;
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
            _object.transform.position = _start.Value;
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
            Debug.Log("Spiral Cancel");
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
        float r1 = (_now - (Vector3)mEnd).magnitude;
        float r2 = r1 - mSpeed * Time.deltaTime;
        float divisor = r2 / r1;
        if ((_now - (Vector3)mEnd).magnitude < 0.1f) { mIsRun = false; }
        else
        {
            ret.x = mEnd.Value.x + ((_now.x - mEnd.Value.x) * (1 - Mathf.Pow(mAngleSpeed * Time.deltaTime / 2, 2)) - (_now.y - mEnd.Value.y) * mAngleSpeed * Time.deltaTime) *divisor;
            ret.y = mEnd.Value.y + ((_now.y - mEnd.Value.y) * (1 - Mathf.Pow(mAngleSpeed * Time.deltaTime / 2, 2)) + (_now.x - mEnd.Value.x) * mAngleSpeed * Time.deltaTime) *divisor;
            ret.z = mEnd.Value.z;
        }
        //Debug.Log(ret);
        return ret;
    }
}
