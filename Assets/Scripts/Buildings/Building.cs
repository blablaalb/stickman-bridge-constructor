using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField]
    private GameObject _buildingEnd;
    [SerializeField]
    private float _distanceThrehsold;
    private PlayerController _character;
    private bool _endReached;


    public GameObject BuildingEnd => _buildingEnd;


    internal void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() is PlayerController player)
        {
            _character = player;
        }
    }


    internal void OnCollisionExit2D(Collision2D other)
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
    }


}
