//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DustyDevTools;

public abstract class TargetFinder<T> where T : ITarget
{
    public const int MAX_SEARCH_RADIUS = 30;
    protected Transform _user;
    protected LayerMask _targetLayer;

    public TargetFinder(Transform user , LayerMask targetLayer)
    {
        _user = user;
        _targetLayer = targetLayer;
    }

    public abstract List<T> FindTargets();

    public abstract T FindTarget(List<T> targets);
}
