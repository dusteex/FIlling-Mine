//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System;

public class MinerWork : CharacterWork
{
    [SerializeField] private float _timeBtwHits;
    private float _damage = 1;
    private Cell _targetCell;
    private Action OnEndWorking;


    public override void StartWork(ITarget target , Action OnEndWorking = null)
    {
        this.OnEndWorking = OnEndWorking;
        if((target is Cell) == false)
            throw new System.Exception("Incorrect type of target");
        
        _targetCell = (Cell) target;
        DelayCaller.CallWithDelay(_timeBtwHits,this,Work);
    }

    protected override void Work()
    {
        if(_targetCell == null)
        {
            StopAllCoroutines();
            OnEndWorking?.Invoke();
            return;
        }
        bool isCellBroken = _targetCell.TakeDamage(_damage);
        if(isCellBroken == false)
            DelayCaller.CallWithDelay(_timeBtwHits,this,Work);
        else
            OnEndWorking?.Invoke();

    }
}
