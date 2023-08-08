using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIEffectManager : MonoBehaviour
{
    public static UIEffectManager instance;
    public enum UIType
    {
        MoveWave, MoveSpiral, MoveCenter, PopupStar, PopupSkull, PopupName, ReduceTime, ChangeColor, Flip
    }

    //Type[] types = 

    private class EffectData
    {
        GameObject mEffectObject;
        UIType mType;
        Vector3? mStart;
        Vector3? mEnd;
        Vector3? mPosition;
        public dynamic effectClass;
        public GameObject Object { get { return mEffectObject; } }
        public UIType Type { get { return mType; } }
        public Vector3? Start { get { return mStart; } }
        public Vector3? End { get { return mEnd; } }
        public Vector3? Position { get { return mPosition; } }
        public EffectData(GameObject _object, UIType _type, Vector3? _start, Vector3? _end)
        {
            mEffectObject = _object;
            mType = _type;
            if (_start != null) mStart = _start;
            if (_end != null) mEnd = _end;

        }
        public EffectData(GameObject _object, UIType _type, Vector3? _position)
        {
            mEffectObject = _object;
            mType = _type;
            if (_position != null) mPosition = _position;
            
        }
    }
    private List<EffectData> mEffectList;
    /// <summary>
    /// ������Ʈ(_object)�� ȿ��(_type)�� Update()���� ����Ͽ� ����ϵ��� ���۸���Ʈ�� �ִ´�.
    /// </summary>
    /// <param name="_object">ȿ�� �����ϴ� ������Ʈ</param>
    /// <param name="_type">ȿ�� ����(MoveWave, MoveSpiral, MoveCenter)���� ���</param>
    /// <param name="_start">������</param>
    /// <param name="_end">������</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">�ʿ��� �Է� �μ��� null�� ��쿡 ����ó����.</exception>
    public bool StartEffect(GameObject _object, UIType _type, Vector3? _start, Vector3? _end)
    {
        if (_object == null) { throw new System.Exception("null GameObject"); }
        if (_type > UIType.Flip) { throw new System.Exception("Unknown UI Effect"); }
        if (_type > UIType.MoveCenter) { return false; }
        if (_start == null || _end == null) { return false; }

        mEffectList.Add(new EffectData(_object, _type, _start, _end));
        return true;
    }
    /// <summary>
    /// ������Ʈ(_object)�� ȿ��(_type)�� Update()���� ����Ͽ� ����ϵ��� ���۸���Ʈ�� �ִ´�.
    /// </summary>
    /// <param name="_object">ȿ�� �����ϴ� ������Ʈ</param>
    /// <param name="_type">ȿ�� ����(PopupStar, PopupSkull, PopupName, ReduceTime, ChangeColor, Flip)���� ���</param>
    /// <param name="_position">���� ����</param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public bool StartEffect(GameObject _object, UIType _type, Vector3? _position)
    {
        if (_object == null) { throw new System.Exception("null GameObject"); }
        if (_type > UIType.Flip) { throw new System.Exception("Unknown UI Effect"); }
        if (_type < UIType.PopupStar) { return false; }
        if (_position == null) { return false; }

        mEffectList.Add(new EffectData(_object, _type, _position));
        return true;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            mEffectList = new List<EffectData>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mEffectList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (EffectData data in mEffectList)
        {
            if (data.Type <= UIType.MoveCenter)
            {
                //data.effectClass.Run(data.Object, data.Start, data.End);
            }
            else
            {
                //data.effectClass.Run(data.Object);
            }
        }
    }
}
