using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "new FloorData")]
public class FloorData : ScriptableObject
{
    [SerializeField] private List<Vector2> _freePositions;
    [SerializeField] private List<Vector2> _cellsWithoutFogPositions;


    public HashSet<Vector2> GetFreePositions()
    {
        return new HashSet<Vector2>(_freePositions);
    }   

    public HashSet<Vector2> GetCellsWithoutFogPositions()
    {
        return new HashSet<Vector2>(_cellsWithoutFogPositions);
    }   

    public void AddFreePosition(Vector2 position)
    {
        _freePositions.Add(position);
    }

    public void AddCellWithoutFogPosition(Vector2 position)
    {
        _cellsWithoutFogPositions.Add(position);
    }
}
