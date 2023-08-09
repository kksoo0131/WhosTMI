using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : FixedEffectInterface
{
    private Animator filpAnime;
    public bool Run(GameObject _object, Vector3? _position)
    {
        //애니메이션의 대상이 될 게임오브젝트 지정
        filpAnime = _object.transform.GetComponent<Animator>();
        filpAnime.Play("filp");
        return false;
    }

    public void Cancel()
    {
        if (filpAnime == null)
        {
            throw new System.Exception("GameObject Dosen't Exist!!!");
        }
        else
        {
            //filpAnime.Play("unfilp");
        }
    }

    public bool IsEnd()
    {
        throw new System.NotImplementedException();
    }
}


