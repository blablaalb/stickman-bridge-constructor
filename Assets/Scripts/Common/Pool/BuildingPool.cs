using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildingPool : GenericPool<Building>
{
    [SerializeField]
    private List<Building> _buildingVariants;

    override protected void Awake()
    {
        base.Awake();
        SetRandomBuilding();
    }


    private void SetRandomBuilding()
    {
        var indx = Random.Range(0, _buildingVariants.Count);
        prefab = _buildingVariants[indx].gameObject;
    }

    public override Building Get()
    {
        SetRandomBuilding();
        var indx = pool.FindIndex(0, x => x.transform.localScale.x == prefab.transform.localScale.x);
        if (indx > -1)
        {
            var building = pool[indx];
            pool.RemoveAt(indx);
            return building;
        }
        return InstantiateNew();
    }

}
