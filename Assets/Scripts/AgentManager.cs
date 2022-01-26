using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField]
    int _numberAgents;
    [SerializeField]
    Tile _tilePrefab;
    [SerializeField]
    Agent _agentPrefab;
    [SerializeField]
    AgentTest2 _agent2Prefab;
    [SerializeField]
    int _gridWidth;
    [SerializeField]
    int _gridHeight;
    float _cellSize;
    [SerializeField]
    GridManager _gridManager;
    // Start is called before the first frame update
    void Start()
    {
        _cellSize = _tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        SpawnAgents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnAgents() 
    {
        for (int i = 0; i < _numberAgents; i++) 
        {
            
            Vector3 randomPos = new Vector3(Random.Range(0, _gridWidth * _cellSize), Random.Range(0, _gridHeight * _cellSize),-7f);
            //Agent a = Instantiate(_agentPrefab,randomPos,Quaternion.identity);
            AgentTest2 a = Instantiate(_agent2Prefab, randomPos, Quaternion.identity);
            a._manager = _gridManager;
        }
    }
}
