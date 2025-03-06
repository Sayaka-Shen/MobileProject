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

        OptionManager.OptionChange += ShowCorrectButton;
        _mouvementPlayer.OnEndMove += ShowCorrectButton;
        ShowCorrectButton();
    }

    public void ShowCorrectButton()
    {
        if(ShowAllButton) _sprite.enabled = !CollisionManager.Instance.GetObstacleAt(transform.position);
        else _sprite.enabled = false;
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
