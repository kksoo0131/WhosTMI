using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MoveEffectInterface
{
    public abstract bool Run(GameObject _object, Vector3? _start, Vector3? _end);
    public abstract void Cancel();
    public abstract bool IsEnd();
}

public interface FixedEffectInterface
{
    public abstract bool Run(GameObject _object, Vector3? _position);
    public abstract void Cancel();
    public abstract bool IsEnd();
}