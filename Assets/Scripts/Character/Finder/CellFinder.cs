//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DustyDevTools;

public class CellFinder : TargetFinder<Cell>
{
    public CellFinder(Transform user , LayerMask targetLayer):base(user,targetLayer)
    {
        return;   
    }

    public override List<Cell> FindTargets()
    {
        float radius = 1f;
        Collider2D[] overlappedTargets;
        List<Cell> resultTargets = new List<Cell>();
        do{
            Vector2 firstPoint = (_user.position - Vector3.one*radius).To2D();
            Vector2 secondPoint = (_user.position + Vector3.one*radius).To2D();

            overlappedTargets = Physics2D.OverlapAreaAll(firstPoint,secondPoint);
            radius++;

            if(overlappedTargets.Length == 0) continue;
            
            foreach(var target in overlappedTargets)
            {
                Cell resultTarget;
                if(target.TryGetComponent<Cell>(out resultTarget))
                {
                    if(resultTarget.HasFog == false)
                        resultTargets.Add(resultTarget);
                }
            }
            if(resultTargets.Count > 0)
            {
                return resultTargets;
            }
        }
        while(radius < MAX_SEARCH_RADIUS);
        Debug.Log("Radius : " + radius);
        return null;
    }


    public override Cell FindTarget(List<Cell> cells = null)
    {
        if(cells == null) cells = FindTargets();
        if(cells == null) 
        {
            return null;
        }

        Cell[] buffer = new Cell[cells.Count];
        cells.CopyTo(buffer);

        if(cells.Count == 0)
        {
            return null;
        };
        // SORT BY SHORTEST DISTANCE TO PLAYER
        cells.Sort((x,y)=>{
                float distanceToX , distanceToY;
                distanceToX = Vector3.Distance(_user.position,x.transform.position);
                distanceToY = Vector3.Distance(_user.position,y.transform.position);
                if(distanceToX < distanceToY)
                    return -1;
                else if(distanceToY < distanceToX)
                    return 1;
                return 0;
            });

        // GET A RANDOM CELL FROM MOST NEAREST TO PLAYER
        
        Cell targetCell;
        while(cells.Count > 0)
        {
            int upperBound = (int)(cells.Count*.3f);
            if(upperBound == 0) upperBound = 1;
            int randomIndex = Random.Range(0,upperBound);
            targetCell = cells[randomIndex];
            if(targetCell == null)
                cells.Remove(targetCell);
            else
                return targetCell;
        }
        return null;
    }
}   
