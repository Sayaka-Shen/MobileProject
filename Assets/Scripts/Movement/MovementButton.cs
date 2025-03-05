using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementButton : MonoBehaviour
{
    private MovementPlayer _mouvementPlayer;

    private ReverseAction _reverseAction = new ReverseAction();

    // Swipe Movement
    private Vector3 _firstPosition;
    private Vector3 _lastPosition;
    private float _dragDistance;

    // Timer 
    [SerializeField] private float _maxTimer = 0.5f;
    private float _timer = 0;
    private bool _isClicking = false;

    [Header("Components")]
    [SerializeField] private Collider2D[] _collidersUp;
    [SerializeField] private Collider2D[] _collidersDown;
    [SerializeField] private Collider2D[] _collidersLeft;
    [SerializeField] private Collider2D[] _collidersRight;

    private void Awake()
    {
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.PlayerMove;
    }

    private void Start()
    {
        _mouvementPlayer = GameManager.Instance.MovementPlayer;
        
        // Calculate Drag Distance
        _dragDistance = Screen.height * 15 / 100;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _maxTimer) { return; }

        if (!isPlaying(GameManager.Instance.AnimPlayer, "anim_idle") || GameManager.Instance.Pause) return;

        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (IsPositionOverUIObject(touch.position))
            {
                Debug.Log("Hover Gameobject");
                _isClicking = false;
                return;
            }
            else
            {
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    _isClicking = true;
                    _firstPosition = touch.position;
                    _lastPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved && _isClicking) // update the last position based on where they moved
                {
                    _lastPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended && _isClicking) //check if the finger is removed from the screen
                {
                    _lastPosition = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 20% of the screen height
                    if (Mathf.Abs(_lastPosition.x - _firstPosition.x) > _dragDistance || Mathf.Abs(_lastPosition.y - _firstPosition.y) > _dragDistance)
                    {//It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(_lastPosition.x - _firstPosition.x) > Mathf.Abs(_lastPosition.y - _firstPosition.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            if ((_lastPosition.x > _firstPosition.x))  //If the movement was to the right)
                            {   //Right swipe
                                Debug.Log("Right Swipe");
                                TryMove(Vector3.right);
                            }
                            else
                            {   //Left swipe
                                Debug.Log("Left Swipe");
                                TryMove(Vector3.left);
                            }
                        }
                        else
                        {   //the vertical movement is greater than the horizontal movement
                            if (_lastPosition.y > _firstPosition.y)  //If the movement was up
                            {   //Up swipe
                                Debug.Log("Up Swipe");
                                TryMove(Vector3.up);
                            }
                            else
                            {   //Down swipe
                                Debug.Log("Down Swipe");
                                TryMove(Vector3.down);
                            }
                        }
                    }
                    else if (CheckCollider(_collidersUp, _firstPosition))
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        Debug.Log("Tap");
                        TryMove(Vector3.up);
                    }
                    else if (CheckCollider(_collidersDown, _firstPosition))
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        Debug.Log("Tap");
                        TryMove(Vector3.down);
                    }
                    else if (CheckCollider(_collidersLeft, _firstPosition))
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        Debug.Log("Tap");
                        TryMove(Vector3.left);
                    }
                    else if (CheckCollider(_collidersRight, _firstPosition))
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        Debug.Log("Tap");
                        TryMove(Vector3.right);
                    }

                    _timer = 0;
                    _isClicking = false;
                }
            }
        }

    }

    private void TryMove(Vector3 position)
    {
        Vector3 newPos = _mouvementPlayer.transform.position + position;
        newPos.z = 0;

        if (CollisionManager.Instance.GetObstacleAt(newPos)) return;

        _reverseAction.PositionTarget = _mouvementPlayer.transform.position;
        RollbackManager.Instance.AddPlayerAction(_reverseAction);
        _mouvementPlayer.AddPos(newPos);
        _mouvementPlayer.StartMoving();
    }

    private bool CheckCollider(Collider2D[] _colliders, Vector3 position)
    {
        foreach (Collider2D collider in _colliders)
        {
            if(collider.OverlapPoint(Camera.main.ScreenToWorldPoint(position)))
            {
                return true;
            }
        }

        return false;
    }

    private bool IsPositionOverUIObject(Vector3 position)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(position.x, position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            return true;
        else
            return false;
    }

}
