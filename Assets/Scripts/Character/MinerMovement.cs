//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class MinerMovement : CharacterMovement
{
    [SerializeField] private LayerMask _cellsLayer;
    [SerializeField] private MinerWork _minerWork;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minDistanceToCell;

    private TargetFinder<Cell> _cellFinder;
    private Cell _targetCell;
    private Vector3 _direction;
    private bool _isMoving;


    private void Awake()
    {
        _cellFinder = new TargetFinder<Cell>(transform , _cellsLayer);
        FloorSpawner.OnLevelLoaded += SetTarget;
    }



    private void Update()
    {
        if(_targetCell && _isMoving)
            Move();

    }

    public void SetTarget()
    {
        _targetCell = _cellFinder.FindTarget();
        _isMoving = true;
    }

    public override void Move()
    {
        if(_targetCell == null)
            SetTarget();

        if(Vector3.Distance(transform.position,_targetCell.transform.position) <= _minDistanceToCell)
        {
            _isMoving = false;
            _minerWork.StartWork(_targetCell, SetTarget);
            return;
        }

        _isMoving = true;
        _direction = _targetCell.transform.position - transform.position;
        transform.Translate(_direction.normalized * _moveSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        FloorSpawner.OnLevelLoaded -= SetTarget;
    }

}
