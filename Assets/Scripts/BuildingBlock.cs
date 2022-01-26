using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public GridManager _manager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (_manager.GetIsBuildingBlocks()) 
            {
                Destroy(gameObject);
            }
        }
    }

    
}
