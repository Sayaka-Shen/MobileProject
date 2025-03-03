using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class Level : MonoBehaviour
{
    [SerializeField] GameObject _obstacle;
    [SerializeField] GameObject _obstacleHolder;
    public void ChooseObstacle() { _choosen = _obstacle; _choosenHolder = _obstacleHolder; }
    [SerializeField] GameObject _oneWay;
    [SerializeField] GameObject _oneWayHolder;
    public void ChooseOneWay() { _choosen = _oneWay; _choosenHolder = _oneWayHolder; }
    [SerializeField] GameObject _river;
    [SerializeField] GameObject _riverHolder;
    public void ChooseRiver() { _choosen = _river; _choosenHolder = _riverHolder; }
    [SerializeField] GameObject _portal;
    [SerializeField] GameObject _portalHolder;
    public void ChoosePortal() { _choosen = _portal; _choosenHolder = _portalHolder; }
    [SerializeField] GameObject _altar;
    [SerializeField] GameObject _altarHolder;
    public void ChooseAddMorePower() { _choosen = _addMorePower; _choosenHolder = _addMorePowerHolder; }
    [SerializeField] GameObject _addMorePower;
    [SerializeField] GameObject _addMorePowerHolder;
    public void ChooseAltar() { _choosen = _altar; _choosenHolder = _altarHolder; }
    [SerializeField] GameObject _soul;
    [SerializeField] GameObject _soulHolder;
    public void ChooseSoul() { _choosen = _soul; _choosenHolder = _soulHolder; }
    [SerializeField] GameObject _soulSeeker;
    [SerializeField] GameObject _soulSeekerHolder;
    public void ChooseSoulSeeker() { _choosen = _soulSeeker; _choosenHolder = _soulSeekerHolder; }
    [SerializeField] GameObject _soulTrapper;
    [SerializeField] GameObject _soulTrapperHolder;
    public void ChooseSoulTrapper() { _choosen = _soulTrapper; _choosenHolder = _soulTrapperHolder; }
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _playerHolder;
    public void ChoosePlayer() {_choosen = _player; _choosenHolder = _playerHolder;}

    GameObject _choosen;
    GameObject _choosenHolder;
    public bool AsChoose => _choosen != null;
    public string ChoosenName => _choosen.name;
    public void ChooseNothing() => _choosen = null;

    [SerializeField] LayerMask ObstacleLayers;

    private void Awake()
    {
        if(!PrefabUtility.IsPartOfPrefabInstance(this)) enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && AsChoose)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(!Physics2D.Raycast(pos, Vector3.forward, 1,ObstacleLayers))
            {
                GameObject instance = PrefabUtility.InstantiatePrefab(_choosen, _choosenHolder.transform) as GameObject;
                if (pos.x < 0) pos.x = (int)pos.x - 0.5f;
                else pos.x = (int)pos.x + 0.5f;
                if (pos.y < 0) pos.y = (int)pos.y - 0.5f;
                else pos.y = (int)pos.y + 0.5f;
                instance.transform.position = new Vector3(pos.x, pos.y, 0);
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (enabled) PrefabUtility.ApplyPrefabInstance(gameObject, InteractionMode.UserAction);
    }

    [SerializeField] private float maxX = 10.5f;
    [SerializeField] private float maxY = 5.5f;
    void OnDrawGizmos()
    {
        if(!enabled) return;
        Gizmos.color = Color.yellow;

        Vector3 pos0 = new Vector3();
        Vector3 pos1 = new Vector3();
        for (float i = -maxX; i < maxX; i++)
        {
            pos0.x = i;
            pos0.y = -maxY;
            pos1.x = i;
            pos1.y = maxY;
            Gizmos.DrawLine(
                pos0,
                pos1
            );
        }

        for (float i = -maxY; i < maxY; i++)
        {
            pos0.x = -maxX;
            pos0.y = i;
            pos1.x = maxX;
            pos1.y = i;
            Gizmos.DrawLine(
                pos0,
                pos1
            );
        }
    }
}
#endif