using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIEffectManager : MonoBehaviour
{
    public enum UIType
    {
        MoveWave, MoveSpiral, MoveCenter, PopupStar, PopupSkull, PopupName, ReduceTime, ChangeColor, Flip
    }
    private class EffectData
    {
        GameObject mEffectObject;
        UIType mType;
        Vector3? mStart;
        Vector3? mEnd;

        public GameObject Object { get { return mEffectObject; } }
        public UIType Type { get { return mType; } }
        public Vector3? Start { get { return mStart; } }
        public Vector3? End { get { return mEnd; } }
        public EffectData(GameObject _object, UIType _type, Vector3? _start, Vector3? _end)
        {
            mEffectObject = _object;
            mType = _type;
            if (_start != null) mStart = _start;
            if (_end != null) mEnd = _end;
        }
    }
    private List<EffectData> mEffectList;
    /// <summary>
    /// 오브젝트(_object)에 효과(_type)를 Update()에서 계속하여 계산하도록 동작리스트에 넣는다.
    /// </summary>
    /// <param name="_object">효과 적용하는 오브젝트</param>
    /// <param name="_type">효과 종류(MoveWave, MoveSpiral, MoveCenter, PopupStar, PopupSkull, PopupName, ReduceTime, ChangeColor, Flip)</param>
    /// <param name="_start">시작점, null일 수 있음</param>
    /// <param name="_end">도착점, null일 수 있음</param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public bool StartEffect(GameObject _object, UIType _type, Vector3? _start, Vector3? _end)
    {
        if (_object == null) { throw new System.Exception("null GameObject"); }
        if (_type > UIType.Flip) { throw new System.Exception("Unknown UI Effect"); }
        if (_type >= UIType.MoveWave && _type <= UIType.MoveCenter)
        {
            if (_start == null && _end == null) { throw new System.Exception("Unknown Position"); }
        }
        mEffectList.Add(new EffectData(_object, _type, _start, _end));
        return true;
    }
    // Start is called before the first frame update
    void Start()
    {
        mEffectList.Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
