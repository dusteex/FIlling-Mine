//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public class Cell : MonoBehaviour , ITarget
{


    private CellData _cellData;
    private SpriteRenderer _renderer;
    private Chunk _parentChunk;
    private float _strength;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Init(CellData cellData , Chunk parentChunk)
    {
        this._cellData = cellData;
        this._parentChunk = parentChunk;
        _renderer.sprite = cellData.Sprite;
        this._strength = cellData.Hardness;
        gameObject.name = $"Cell : {transform.position}";
    }

    public bool TakeDamage(float damage)
    {
        _strength -= damage;
        if(_strength <= 0 )
        {
            Death();
            return true;
        }
        return false;
    }

    private void Death()
    {
        GameEvents.OnCellDeleted?.Invoke(transform.position);
        _parentChunk.RemoveCellByWorldPosition(transform.position);
        Destroy(gameObject);
    }



}
