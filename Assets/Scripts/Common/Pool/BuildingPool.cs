using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPool : GenericPool<Building>
{
    [SerializeField]
    private Building[] _buildingVariants;

    override protected void Awake()
    {
        base.Awake();
        SetRandomBuilding();
    }


    private void SetRandomBuilding()
    {
        var indx = Random.Range(0, _buildingVariants.Length);
        prefab = _buildingVariants[indx].gameObject;
    }

    public override Building Get()
    {
        SetRandomBuilding();
        return base.Get();
    }

}
