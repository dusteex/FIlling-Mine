using UnityEngine;

public class Floor : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private FloorSpawner _floorSpawner;
    [SerializeField] private FloorGenerator _floorGenerator;
    //[SerializeField] private Fog _fog;

    private Chunk[,] _chunks;
    
    public Chunk this[int i,int j]
    {
        get => _chunks[i,j];
    }

    private void Start()
    {
        InitialSpawn();
    }

    // private void Update()
    // {
        
    // }

    private void InitialSpawn()
    {
        _chunks = _floorGenerator.GenerateFloor();
        _floorSpawner.SpawnChunks(_chunks);
    }
}