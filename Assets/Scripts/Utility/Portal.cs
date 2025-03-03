using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour, IInteractable
{
   [Header("PortalSettings")] 
   [SerializeField] private Transform _linkPortal;
   [SerializeField] private UnityEvent _onTeleport;

    private ReverseAction _reverseAction = new ReverseAction();

    private void Awake()
    {
        _reverseAction.Holder = gameObject;
        _reverseAction.Type = ReverseActionType.PlayerTp;
        _reverseAction.PositionTarget = transform.position;
    }
    public void Interact()
   {
        RollbackManager.Instance.AddAction( _reverseAction );
        GameManager.Instance.MovementPlayer.TPAt(_linkPortal.position);
        _onTeleport?.Invoke();
   }
}
