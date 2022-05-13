//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DustyDevTools;

public class TargetFinder<T> where T : ITarget
{
    public const int MAX_SEARCH_RADIUS = 30;
    private Transform _user;
    private LayerMask _targetLayer;

    public TargetFinder(Transform user , LayerMask targetLayer)
    {
        _user = user;
        _targetLayer = targetLayer;
    }

    public T FindTarget()
    {
        int radius = 1;
        Collider2D[] targets;
        List<T> resultTargets = new List<T>();
        do{
            Vector2 firstPoint = (_user.position - Vector3.one*radius).To2D();
            Vector2 secondPoint = (_user.position + Vector3.one*radius).To2D();

            targets = Physics2D.OverlapAreaAll(firstPoint,secondPoint);
            if(targets.Length > 0 )
            {
                foreach(var target in targets)
                {
                    T resultTarget;
                    if(target.TryGetComponent<T>(out resultTarget))
                        resultTargets.Add(resultTarget);
                }
                return resultTargets[Random.Range(0,resultTargets.Count)];
            }
            radius++;
        }
        while(targets.Length == 0 && radius < MAX_SEARCH_RADIUS);
        Debug.Log("NOT WORKING");
        return default(T);
    }
}
