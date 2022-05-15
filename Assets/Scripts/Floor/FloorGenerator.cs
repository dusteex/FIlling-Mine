using System.Collections.Generic;
using UnityEngine;
using DustyDevTools;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField] private CellData[] _cellsData;
    [SerializeField] private float _radiusInChunks;
    [SerializeField] private FloorData  _floorData;

    private HashSet<Vector2> _freePositions;

    public Chunk[,] GenerateFloor()
    {   
        _freePositions = _floorData.GetFreePositions();
        int width = (int)_radiusInChunks*2+1;
        Chunk[,] _chunks = new Chunk[width,width];
        Vector3 startPosition = new Vector3(-_radiusInChunks*Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH/2 , -_radiusInChunks*Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH/2,0);

        // GENERATING ALL CHUNKS
        for(int i = 0 ; i < width ; i++)
        {
            for(int j = 0 ; j < width ; j++)
            {
                Vector3 newPosition = startPosition + new Vector3(j*Chunk.CHUNK_WIDTH,i*Chunk.CHUNK_WIDTH,0);
                _chunks[i,j] = GenerateChunk(newPosition);
            }
        }

        return _chunks;
    }

    private Chunk GenerateChunk(Vector3 leftBottomCellPosition)
    {
        Transform chunkContainer = new UnityEngine.GameObject().transform;
        Chunk newChunk = new Chunk(leftBottomCellPosition,chunkContainer);
        Randomizer _rand = new Randomizer(_cellsData);
        int cellsCount = 0;

        // GENERATE ALL CELLS INSIDE CHUNK
        for(int i = 0 ; i < Chunk.CHUNK_WIDTH ; i++)
            for(int j = 0 ; j < Chunk.CHUNK_WIDTH ; j++)
            {
                Vector3 newCellPosition = leftBottomCellPosition + new Vector3(i,j,0);
                // if current cell in free positions , we shouldn`t generate it.
                if(_freePositions.Contains(newCellPosition.To2D()))
                {
                    newChunk[i,j] = null;
                    continue;
                }

                cellsCount++;
                newChunk[i,j] = _rand.RandomCell();
            }
        if(cellsCount == 0) // if our chunk is empty , we should to return null;
            return null;
        return newChunk;
    }

}
