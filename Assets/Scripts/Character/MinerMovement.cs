//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class MinerMovement : CharacterMovement
{
    [SerializeField] private LayerMask _cellsLayer;
    [SerializeField] private MinerWork _minerWork;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _minDistanceToCell;

    private CellFinder _cellFinder;
    private Cell _targetCell;
    private Vector3 _direction;
    private bool _isMoving;
    private bool _isFindingTarget;


    private void Awake()
    {
        _cellFinder = new CellFinder(user:transform , _cellsLayer);
        FloorSpawner.OnLevelLoaded += SetTarget;
    }


    private void Update()
    {
        if(_targetCell && _isMoving)
            Move();
        else if(_isMoving && !_isFindingTarget)
        {
            SetTarget();
        }
    }

    public void SetTarget()
    {
        _isFindingTarget = true;
        int c = 0;
        do {
            _targetCell = _cellFinder.FindTarget(); 
            c++;
        } 
        while(_targetCell == null && c < 30);


        _isMoving = true;
        _isFindingTarget = false;
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
