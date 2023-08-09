using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDecayEffect : FixedEffectInterface
{
    // 대상 오브젝트? Canvas -> recordUI -> nowScoreTxt ( 남은 시간 UI)

    public void Cancel()
    {
        throw new System.NotImplementedException();
    }

    public bool IsEnd()
    {
        throw new System.NotImplementedException();
    }

    public bool Run(GameObject _object, Vector3? _position)
    {
        TextMeshProUGUI target = _object.GetComponent<TextMeshProUGUI>();

        if (target == null) return false;

        target.color = Color.red;

        return false;
        
    }

}
