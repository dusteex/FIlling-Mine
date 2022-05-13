using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public delegate void EmptyHandler();
    static public event EmptyHandler OnLevelLoaded;

    [Header("Prefabs")]
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private GameObject _groundPrefab;
    [Header("References")]
    [SerializeField] private Fog _fog;
    private Transform _chunksContainer;

    public void SpawnChunks(Chunk[,] chunks)
    {
        Transform _chunksContainer = new GameObject().transform;
        _chunksContainer.name = "Chunks";

        SpawnGround(chunks.GetLength(1)*Chunk.CHUNK_WIDTH);
        foreach(Chunk chunk in chunks)
        {
            if( chunk!= null) 
            {
                SpawnChunk(chunk);
                chunk.Container.parent = _chunksContainer;
            }

        }
        OnLevelLoaded?.Invoke();
    }

    private void SpawnChunk(Chunk chunk)
    {
        for(int i = 0 ; i < Chunk.CHUNK_WIDTH ; i++)
        {
            for(int j = 0 ; j < Chunk.CHUNK_WIDTH ; j++)
            {
                if(chunk[i,j] == null)
                    continue;
                Cell newCell = SpawnCell(chunk[i,j],chunk.GetCellPosition(j,i) , chunk );
                newCell.transform.parent = chunk.Container;
            }
        }
    }

    private Cell SpawnCell(CellData cellData , Vector3 position , Chunk parentChunk)
    {
        Cell newCell = Instantiate(_cellPrefab,position,Quaternion.identity);
        _fog.TrySpawnFog(position);
        newCell.Init(cellData,parentChunk);
        return newCell;
    }
    
    private void SpawnGround(float width)
    {
        Transform groundTransform = Instantiate(_groundPrefab,Vector3.zero,Quaternion.identity).transform;
        groundTransform.localScale = new Vector3(width,width,1);
    }
}
