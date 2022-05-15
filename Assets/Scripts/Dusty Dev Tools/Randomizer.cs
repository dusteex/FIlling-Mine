using System.Collections.Generic;
using UnityEngine;

namespace DustyDevTools
{

public class Randomizer
{
    public IEnumerable<CellData> _cells;

    public Randomizer(IEnumerable<CellData> cells)
    {
        _cells = cells;
    }

    
    public CellData RandomCell()
    {
        CellData newCell = null;
        int maxSum = 0;
        foreach(CellData cell in _cells)
            maxSum += cell.Probability;

        int randomNum = Random.Range(0,maxSum);
        int s = 0;
        foreach(CellData cell in _cells)
        {
            s+=cell.Probability;
            if(newCell == null && randomNum <= s)
            {
                newCell = cell;
                return newCell;
            }
        }
        return newCell;
    }

    public static UnityEngine.Object RandomObject(UnityEngine.Object[] objects)
    {
        int randomNum = Random.Range(0,objects.Length);
        return objects[randomNum];

    }   
}

}