using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : PoolMember<BuildingPool>
{
    [SerializeField]
    private GameObject _buildingEnd;
    [SerializeField]
    private float _distanceThrehsold;
    private PlayerController _character;
    private bool _endReached;
    private BoxCollider2D _collider;
    private float _width;

    public GameObject BuildingEnd => _buildingEnd;
    public float Width => _width;


    override protected void Awake()
    {
        base.Awake();
        _collider = GetComponent<BoxCollider2D>();
        _width = _collider.size.x;
    }

    internal void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() is PlayerController player)
        {
            _character = player;
        }
    }

    internal void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() is PlayerController player)
        {
            _character = null;
        }
    }

    internal void Update()
    {
        if (_character && !_endReached)
        {
            if (_character.transform.position.MeasureDistanceX(_buildingEnd.transform.position) <= _distanceThrehsold)
            {
                _endReached = true;
                _character.OnBuildingEndReached(this);
            }
        }

        if (transform.position.x < Camera.main.transform.position.x && !_collider.SeenByCamera())
        {
            _endReached = false;
            base.ReturnToPool();
        }
    }


}
