using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor, _notPassibleColor, _roughColor,_goalColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _sprite;
    // Start is called before the first frame update
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _upArrow, _downArrow, _leftArrow, _rightArrow, _NEArrow, _NWArrow, _SEArrow, _SWArrow,_Goal;
    //[SerializeField] private Vector2 _velocity;

    private bool _isInPassable;
    private bool _isRough;
    public bool _isOffset;
    public bool _isGoal;
    public Vector2 _direction;

    private Vector2 _up,_down,_left,_right,_northEast,_northWest,_southEast,_southWest,_goalDirection;
    //public int size;

    private void Start()
    {
        // RectTransform rt = 
        _isInPassable = false;
        _isRough = false;
       InitializeVectors();
        _sprite.GetComponent<SpriteRenderer>().enabled = false;
    }
    private void Update()
    {
        ColorIn(_isOffset);
       SetSprite();
    }

    private void InitializeVectors()
    {
        _up = new Vector2(0f, 1f);
        _down = new Vector2(0f, -1f);
        _left = new Vector2(-1f, 0f);
        _right = new Vector2(1f, 0f);
        _northEast = new Vector2(1f, 1f);
        _northWest = new Vector2(-1f, 1f);
        _southEast = new Vector2(1f, -1f);
        _southWest = new Vector2(-1f, -1f);
        _goalDirection = new Vector2(0, 0);
    }
    public void ColorIn(bool isOffset)
    {
        if (_isGoal == true) 
        {
            _renderer.color = _goalColor;
        }
        else if (_isInPassable == true) 
        {
            _renderer.color = _notPassibleColor;
        }
        else if(_isRough == true)
        {
            _renderer.color = _roughColor;
        }
        else 
        {
            _renderer.color = isOffset ? _offsetColor : _baseColor;
        } 
       
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
        // _text.GetComponent<TextMesh>().text = "XdXp";
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    private void OnMouseUp()
    {

    }

    public string GetText()
    {
        return _text.GetComponent<TextMesh>().text;
    }

    public void SetString(string text)
    {
        _text.GetComponent<TextMesh>().text = text;
    }

    public void SetInPassable(bool isPassable)
    {
        _isInPassable = isPassable;
    }

    public void SetIsRough(bool isRough) 
    {
        _isRough = isRough;
    }

    private void SetSprite()
    {
        if (_direction == _up)
        {
            _sprite.GetComponent<SpriteRenderer>().sprite = _upArrow;
        }
        else if (_direction == _down)
        {
            _sprite.GetComponent<SpriteRenderer>().sprite = _downArrow;
        }
        else if (_direction == _left)
        {
            _sprite.GetComponent<SpriteRenderer>().sprite = _leftArrow;
        }
        else if (_direction == _right)
        {
            _sprite.GetComponent<SpriteRenderer>().sprite = _rightArrow;
        }
        else if (_direction == _northEast)
        {
            _sprite.GetComponent<SpriteRenderer>().sprite = _NEArrow;
        }
        else if (_direction == _northWest)
        {
            _sprite.GetComponent<SpriteRenderer>().sprite = _NWArrow;
        }
        else if (_direction == _southEast)
        {
            _sprite.GetComponent<SpriteRenderer>().sprite = _SEArrow;
        }
        else if (_direction == _southWest)
        {
            _sprite.GetComponent<SpriteRenderer>().sprite = _SWArrow;
        }
        else if(_direction == _goalDirection)
        {
            _sprite.GetComponent<SpriteRenderer>().sprite = _Goal;

        }


    }

    public void ShowSprites(bool b)
    {
        if (b == true)
        {
            //_text.SetActive(false);
            //_sprite.SetActive(true);
            //_text.GetComponent<TextMesh>().
            _sprite.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            //_text.SetActive(true);
            //_sprite.SetActive(false);
             _sprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
