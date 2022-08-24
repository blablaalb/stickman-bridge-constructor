using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using System.Linq;

public class TrackConstructor : Singleton<TrackConstructor>
{
    private BuildingPool _buildingPool;
    private Building _lastBuilding;
    private Vector3 _firstBuildingPosition = Vector3.zero;
    private int _instantiatedCount;
    [SerializeField]
    private FloatRange _distanceRange;
    [SerializeField]
    private float _generationDistanceThreshold;

    override protected void Awake()
    {
        base.Awake();
        _buildingPool = FindObjectOfType<BuildingPool>();
        _lastBuilding = FindObjectOfType<Building>();
    }

    internal void Update()
    {
        if (Camera.main.transform.position.x >= (_lastBuilding.transform.position.x - _generationDistanceThreshold))
        {
            AddBuildings(5).ToArray();
        }
    }

    internal void Start()
    {
        foreach (var b in AddBuildings(3)) { }
    }

    public Building AddBuilding()
    {
        var building = _buildingPool.Get();
        var distance = _distanceRange.Random();
        var position = _lastBuilding ? _lastBuilding.BuildingEnd.transform.position : _firstBuildingPosition;
        position.x += distance + building.Width * building.transform.localScale.x;
        building.transform.position = position;
        building.gameObject.SetActive(true);
        _lastBuilding = building;
        return building;
    }

    /// <summary>
    /// Lazy evaluated!
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public IEnumerable<Building> AddBuildings(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return AddBuilding();
        }
    }
}