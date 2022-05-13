using UnityEngine;
using UnityEngine.Events;

static public class GameEvents
{
    public static UnityEvent<Vector3> OnCellDeleted = new UnityEvent<Vector3>();
}