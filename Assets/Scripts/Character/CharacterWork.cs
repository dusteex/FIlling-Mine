//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CharacterWork : MonoBehaviour
{
    public abstract void StartWork(ITarget target , Action OnEndWorking = null);

    protected abstract void Work();
}
