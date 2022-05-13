using UnityEngine;

namespace DustyDevTools
{


public static class VectorTools
{
    static public Vector3 To3D(this Vector2 source)
    {
        return new Vector3(source.x,source.y,0);
    }

    static public Vector2 To2D(this Vector3 source)
    {
        return new Vector2(source.x,source.y);
    }

}

}




