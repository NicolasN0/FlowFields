using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBlock : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform _transform;
    [SerializeField] GridManager _manager;
    [SerializeField] Color _isGoingGoal, _isBuildingBlocks, _isBuildingPaths;
    void Start()
    {
        //Vector3 curPos = _transform.localPosition;
        
        _transform.position = new Vector3(_manager._width +1 , _manager._height+1, -9f);
       // _transform.localScale = new Vector3(5, 1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (_manager.GetIsBuildingBlocks())
        {
            GetComponent<MeshRenderer>().material.color = _isBuildingBlocks;
        }
        else if (_manager.GetIsBuildingPaths()) 
        {
            GetComponent<MeshRenderer>().material.color = _isBuildingPaths;

        }
        else 
        {
            GetComponent<MeshRenderer>().material.color = _isGoingGoal;

        }
    }
}
