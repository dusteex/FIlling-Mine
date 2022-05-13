using UnityEngine;

[CreateAssetMenu(menuName = "new Cell Data")]
public class CellData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _probability;
    [SerializeField] private string _name;
    [SerializeField] private float _hardness;

    public Sprite Sprite => _sprite;
    public int Probability => _probability;
    public string Name => _name;
    public float Hardness{get{return (_hardness == 0 ? 1 : _hardness) ;}}
}
