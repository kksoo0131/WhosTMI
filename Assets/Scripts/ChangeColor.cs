using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : FixedEffectInterface
{
    private bool isRun = false;
    private SpriteRenderer spriteRenderer;
    private GameObject thisObject;
    //Vector3 position;

    //받아온 객체의 색을 회색으로 변경
    public bool Run(GameObject _object, Vector3? _position)
    {
        thisObject = _object;

        if (thisObject == null) { return false; }

        spriteRenderer = thisObject.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = Color.gray;

        return false;
    }

    public void Cancel()
    {
        if (thisObject == null)
        {
            throw new System.Exception("GameObject Dosen't Exist!!!");
        }

        spriteRenderer = thisObject.GetComponent<SpriteRenderer>();

        if (spriteRenderer.material.color == Color.gray){
            spriteRenderer.material.color = Color.white;
        }

    }

    public bool IsEnd()
    {
        return false;
    }
}
