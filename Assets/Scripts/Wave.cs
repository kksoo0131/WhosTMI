using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MoveEffectInterface
{

    GameObject mTarget;
    float mMaxY;
    float mMinY;
    bool mDir;
    Vector3? mEnd;
    Vector3 mStart;
    float mSpeedX;
    float mSpeedY;
    float mDuration;

    public bool Run(GameObject _object, Vector3? _start, Vector3? _end)
    {
        if (mTarget == null)
        {
            mTarget = _object;
            mEnd = _end;
            
            mMaxY = _end.Value.y + 0.5f;
            mMinY = _end.Value.y - 0.5f;
            mDir = true;
            mSpeedX = 2f;
            mSpeedY = 2f;
            mDuration = 3.0f;
            mTarget.transform.position = new Vector3(_end.Value.x + (mSpeedX) * mDuration , _end.Value.y, 0);
        }
        mDuration -= Time.deltaTime;

        if (mDuration <= 0)
        {
            Cancel();
            return false;
        }
        mTarget.transform.position = CaculatePosition(mTarget.transform.position);

        return true;
    }
    public void Cancel()
    {
        mTarget.transform.position = (Vector3)mEnd;
    }

    public bool IsEnd()
    {
        if (mDuration <=0) { return false; }
        return true;
    }

    Vector3 CaculatePosition(Vector3 _now)
    {
        // mDir == true 일떄 위로이동
        if (mDir)
        {
            Vector3 nextPos = new Vector3(_now.x - mSpeedX *Time.deltaTime, _now.y + mSpeedY * Time.deltaTime, 0);
            if (nextPos.y  < mMaxY)
            {
                return nextPos; 
            }

        }
        else
        {
            Vector3 nextPos = new Vector3(_now.x - mSpeedX * Time.deltaTime, _now.y - mSpeedY * Time.deltaTime, 0);
            if (nextPos.y > mMinY)
            {
                return nextPos;
            }
            
        }
        mDir = !mDir;
        return _now;
    }
}
