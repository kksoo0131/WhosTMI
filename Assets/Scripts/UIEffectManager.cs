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
    /// ������Ʈ(_object)�� ȿ��(_type)�� Update()���� ����Ͽ� ����ϵ��� ���۸���Ʈ�� �ִ´�.
    /// </summary>
    /// <param name="_object">ȿ�� �����ϴ� ������Ʈ</param>
    /// <param name="_type">ȿ�� ����(MoveWave, MoveSpiral, MoveCenter, PopupStar, PopupSkull, PopupName, ReduceTime, ChangeColor, Flip)</param>
    /// <param name="_start">������, null�� �� ����</param>
    /// <param name="_end">������, null�� �� ����</param>
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
