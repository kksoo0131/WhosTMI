using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopStar : FixedEffectInterface
{
    private bool mIsRun;
    private GameObject mStar;

    public PopStar()
    {
        mStar = Resources.Load<GameObject>("Prefabs/star");
        mIsRun = false;
    }
    public bool Run(GameObject _object, Vector3? _position)
    {
        if (mIsRun == false)
        {

        }
        mIsRun = true;
        return false;
    }

    public void Cancel()
    {
        
    }

    public bool IsEnd()
    {
        return false;
    }
}
