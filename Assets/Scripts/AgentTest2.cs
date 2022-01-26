using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentTest2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody _rigidBody;
    //[SerializeField] GridManager _manager;
    public GridManager _manager;
    [SerializeField]
    float _moveSpeed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // _rigidBody.velocity = new Vector2(0.1f, -0.1f);

        Vector2 pos2 = new Vector2(transform.position.x, transform.position.y);
        //Tile t = _manager.GetTileAtPosition(pos2);
        //Debug.Log(t);
        //Tile t =  _manager.GetTileFromPos(pos2);
        // Debug.Log(t.GetText());


        //WORKED BUT SETTING VELOCITY DIDNT
        //Debug.Log(_manager.GetVelocityFromPos(pos2));
        // _rigidBody.velocity = _manager.GetVelocityFromPos(pos2);
        //  Debug.Log(_manager.GetVelocityFromPos(pos2));
        _rigidBody.velocity = _manager.GetVelocityFromPos(pos2);
        _rigidBody.velocity *= _moveSpeed;

    }
}
