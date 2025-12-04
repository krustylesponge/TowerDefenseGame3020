using System;
using UnityEngine;


//GameObject for putting in shops
[Serializable]
public class Tower
{
    public string name;
    public int cost;
    public GameObject towerPrefab;

    public Tower (string _name, int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        towerPrefab = _prefab;
    }
}
