using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed = 1;
    private int _nbCaseMouv = 0;
    public int NbCaseMouv { get => _nbCaseMouv; }
    public int NbCaseMouvLast { get; private set; }
    public event Action NbCaseMoveLastChage;
    [SerializeField] private List<Vector3> _pathList = new List<Vector3>();
    public List<Vector3> PathList { get => _pathList; set => _pathList = value; }
    [SerializeField] UnityEvent  _onStartMove = new UnityEvent();
    public event Action OnStartMove;
    [SerializeField] UnityEvent  _onEndMove = new UnityEvent();
    public event Action OnEndMove;
    [SerializeField] UnityEvent  _onTP = new UnityEvent();
    public event Action OnStop;
    public event Action OnStepEnd;
    private bool _isMoving = false;
    public bool IsMoving { get => _isMoving; set => _isMoving = value; }
    private Vector3 _nextPos;

    private void Awake()
    {
        NbCaseMouvLast = SoulsManager.Instance.CountForHurt;
    }

    void Update()
    {
        if (_isMoving)
        {
            Mouv();
        }
    }

    void Mouv()
    {
        float deltaTime = Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _nextPos, _speed * deltaTime);
        if (transform.position == _nextPos)
        {
            NextPos(); 
        }
    }

    void NextPos()
    {
        if (_pathList.Count > 0)
        {
            _nextPos = _pathList[0];
            _pathList.RemoveAt(0);
            if (_nextPos.x > transform.position.x)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_nextPos.x < transform.position.x)
            {
                _spriteRenderer.flipX = true;
            }
        }
        else
        {
            _isMoving = false;
            NbCaseMouvLast--;
            _nbCaseMouv++;
            NbCaseMoveLastChage?.Invoke();
            _onEndMove?.Invoke();
            OnEndMove?.Invoke();
            OnStop?.Invoke();
            if (NbCaseMouvLast <= 0)
            {
                OnStepEnd?.Invoke();
                NbCaseMouvLast = SoulsManager.Instance.CountForHurt;
            }
            NbCaseMoveLastChage?.Invoke();
            return;
        }
    }

    public void AddPos(Vector3 pos)
    {
        _pathList.Add(pos);
    }
    public void AddPos(List<Vector3> pos)
    {
        foreach (Vector3 p in pos)
        {
            _pathList.Add(p);
        }
    }

    public void StartMoving()
    {
        _isMoving = true;
        _onStartMove?.Invoke();
        OnStartMove?.Invoke();
        NextPos();
    }

    public void TPAt(Vector3 pos)
    {
        transform.position = pos;
        OnEndMove?.Invoke();
        _onTP?.Invoke();
    }

    public void AddCaseMov(int nb)
    {
        NbCaseMouvLast += nb;
    }
}
