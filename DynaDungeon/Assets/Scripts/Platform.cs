using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform
{
    public List<bool> _Walls = new List<bool>();

    public List<EnumSpawnebleObjects> _spawnebleObjects = new List<EnumSpawnebleObjects>();

    public bool _completed;

    public Platform(List<bool> walls, List<EnumSpawnebleObjects> spawnebleObjects, bool completed)
    {
        _Walls = walls;
        _spawnebleObjects = spawnebleObjects;
        _completed = completed;
    }
}
