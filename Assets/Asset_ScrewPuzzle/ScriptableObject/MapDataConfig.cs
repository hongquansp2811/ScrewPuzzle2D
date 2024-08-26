using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMapDataConfig", menuName = "ScriptableObjects/MapDataConfig", order = 1)]
public class MapDataConfig : ScriptableObject
{
    public int time;
    public Pendulum pendulum;
}
