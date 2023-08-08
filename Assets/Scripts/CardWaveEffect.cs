using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWavingEffect : MonoBehaviour
{
    // 카드 슬롯이 왼쪽 끝에서 제자리까지 움직이고
    // 그 안에 있는 카드들이 물결치듯이 움직인다.
    // 연출 시간 동안 타이머는 정지해야된다.

    public bool Run(GameObject _object, Vector3 _start, Vector3 _end)
    {
        return false;
    }

    public void Cancel()
    {

    }

    public bool isEnd()
    {
        return false;
    }

  
}
