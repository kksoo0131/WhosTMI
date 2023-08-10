using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;

public class UIEffectManager : MonoBehaviour
{
    static public UIEffectManager instance;

    public enum UIType
    {
        MoveWave,
        MoveSpiral,
        MoveCenter,
        PopupStar,
        PopupSkull,
        PopupName,
        ReduceTime,
        ChangeColor,
        Flip
    }

    private class EffectData
    {
        System.Type[] types = new Type[] {
            // Effect class 를 나열해서 넣어둠.
            typeof(Wave),
            typeof(Spiral),
            typeof(Center),
            typeof(PopStar),
            typeof(PopSkull),
            typeof(DisplayName),
            typeof(TimeDecayEffect),
            typeof(ChangeColor),
            typeof(Flip),
            };
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
            if (_start != null) mStart = _start.Value;
            if (_end != null) mEnd = _end.Value;
            effectClass = Activator.CreateInstance(types[(int)_type]);
        }
        public EffectData(GameObject _object, UIType _type, Vector3? _position)
        {
            mEffectObject = _object;
            mType = _type;
            if (_position != null) mPosition = _position;
            effectClass = Activator.CreateInstance(types[(int)_type]);
        }
    }
    private List<EffectData> mEffectList;
    /// <summary>
    /// 오브젝트(_object)에 효과(_type)를 Update()에서 계속하여 계산하도록 동작리스트에 넣는다.
    /// </summary>
    /// <param name="_object">효과 적용하는 오브젝트</param>
    /// <param name="_type">효과 종류(MoveWave, MoveSpiral, MoveCenter)에만 사용</param>
    /// <param name="_start">시작점</param>
    /// <param name="_end">도착점</param>
    /// <returns></returns>
    /// <exception cref="System.Exception">필요한 입력 인수가 null인 경우에 예외처리함.</exception>
    public bool StartEffect(GameObject _object, UIType _type, Vector3? _start, Vector3? _end)
    {
        if (_object == null) { throw new System.Exception("null GameObject"); }
        if (((int)_type) > Enum.GetValues(typeof(UIType)).Length) { throw new System.Exception("Unknown UI Effect"); }
        //if (_type > UIType.MoveCenter) { return false; }
        // if (_start == null || _end == null) { return false; }
        mEffectList.Add(new EffectData(_object, _type, _start, _end));
        return true;
    }
    /// <summary>
    /// 오브젝트(_object)에 효과(_type)를 Update()에서 계속하여 계산하도록 동작리스트에 넣는다.
    /// </summary>
    /// <param name="_object">효과 적용하는 오브젝트</param>
    /// <param name="_type">효과 종류(PopupStar, PopupSkull, PopupName, ReduceTime, ChangeColor, Flip)에만 사용</param>
    /// <param name="_position">적용 지점</param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public bool StartEffect(GameObject _object, UIType _type, Vector3? _position)
    {
        if (_object == null) { throw new System.Exception("null GameObject"); }
        if (((int)_type) > Enum.GetValues(typeof(UIType)).Length) { throw new System.Exception("Unknown UI Effect"); }
        //if (_type < UIType.PopupStar) { return false; }
        // if (_position == null) { return false; }

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
        else
            mEffectList.Clear();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int[] index = new int[mEffectList.Count];
        int i = 0;
        foreach (EffectData data in mEffectList)
        {
            if (data.Type <= UIType.MoveCenter)
            {
                var effect = data.effectClass as MoveEffectInterface;
                var run = effect.Run(data.Object, data.Start, data.End);
                if (run == false)
                {
                    index[i++] = mEffectList.IndexOf(data);
                }
            }
            else
            {
                var effect = data.effectClass as FixedEffectInterface;
                var run = effect.Run(data.Object, data.Position);
                if (run == false)
                {
                    index[i++] = mEffectList.IndexOf(data);
                }
            }
        }
        --i;
        while (i >= 0)
        {
            mEffectList.RemoveAt(index[i]);
            --i;
        }
    }
}
