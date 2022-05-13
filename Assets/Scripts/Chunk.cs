using UnityEngine;

public class Chunk
{
    public const int CHUNK_WIDTH = 7;

    private Vector3 _leftBottomCellPosition;
    private Transform _container;

    public Transform Container => _container;

    public Chunk(Vector3 leftBottomCellPosition , Transform container)
    {
        this._leftBottomCellPosition = leftBottomCellPosition;
        this._container = container;
        
        _container.name = $"Chunk v {_leftBottomCellPosition}";
    }

    private CellData[,] _cells = new CellData[CHUNK_WIDTH,CHUNK_WIDTH];

    public CellData this[int x,int y]
    {
        get => _cells[x,y];
        set => _cells[x,y] = value;
    }

    public Vector3 GetCellPosition(int x , int y)
    {
        return _leftBottomCellPosition + new Vector3(x , y ,0);
    }

    public void RemoveCellByWorldPosition(Vector3 position)
    {
        Vector3 positionInChunk = position - _leftBottomCellPosition ;
        _cells[(int)positionInChunk.x,(int)positionInChunk.y] = null;
    }

    
}
