using UnityEngine;

public class MovementSprite : MonoBehaviour
{
    private MovementPlayer _mouvementPlayer;

    public static bool ShowAllButton = true;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer _sprite;

    private void Start()
    {
        _mouvementPlayer = GameManager.Instance.MovementPlayer;
        _mouvementPlayer.OnStartMove += Hide;

        _mouvementPlayer.OnEndMove += ShowCorrectButton;
        ShowCorrectButton();
    }

    private void Update()
    {
        if (ShowAllButton != _sprite.enabled) _sprite.enabled = ShowAllButton;
    }

    private void ShowCorrectButton()
    {
        _sprite.enabled = !CollisionManager.Instance.GetObstacleAt(transform.position);
    }

    private void Hide()
    {
        _sprite.enabled = false;
    }

    private void OnDestroy()
    {
        _mouvementPlayer.OnStartMove -= Hide;
        _mouvementPlayer.OnEndMove -= ShowCorrectButton;
    }
}
