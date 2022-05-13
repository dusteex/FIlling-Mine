using System.Collections.Generic;
using UnityEngine;
using DustyDevTools;

public class Fog : MonoBehaviour
{
    [SerializeField] private FogCell _fogCellPrefab;
    [SerializeField] private HashSet<Vector2> _cellsWithoutFog = new HashSet<Vector2>();
    [SerializeField] private FloorData _floorData;

    private Dictionary<Vector2,FogCell> _fogCells = new Dictionary<Vector2, FogCell>();
    private HashSet<Vector2> _cellsWithoutFogPositions;

    private void Awake()
    {
        _cellsWithoutFogPositions = _floorData.GetCellsWithoutFogPositions();
        GameEvents.OnCellDeleted.AddListener(position=>RemoveNeighborFogs(position.To2D()));
    }

    public void TrySpawnFog(Vector3 position)
    {
        if(_cellsWithoutFogPositions.Contains(position.To2D()))
            return;
        FogCell newCell = Instantiate(_fogCellPrefab,position,Quaternion.identity);
        newCell.transform.parent = transform;
        _fogCells[position.To2D()] = newCell;
    }


    public void RemoveNeighborFogs(Vector2 position)
    {
        Vector2[] coeffs = new Vector2[]{new Vector2(1,0),new Vector2(-1,0),new Vector2(0,1),new Vector2(0,-1),};
        foreach(Vector2 coeff in coeffs)
            RemoveFog(position + coeff);
    }

    public void RemoveFog(Vector2 position)
    {
        FogCell removingFogCell;
        _fogCells.TryGetValue(position ,out removingFogCell);
        if(removingFogCell)
            removingFogCell.Remove();

    }
}
