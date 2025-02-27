using System;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [Header("UI Settings Movement")]
    [SerializeField] private TextMeshProUGUI _textMovement;
    private MovementPlayer _movementPlayer;

    private void Start()
    {
        // Get the components
        _movementPlayer = GameManager.Instance.MovementPlayer;
        
        // Add the event
        _movementPlayer.NbCaseMoveLastChage += ChangeMoveCaseUI;
        
        ChangeMoveCaseUI();
    }

    private void ChangeMoveCaseUI()
    {
        _textMovement.text = $"{_movementPlayer.NbCaseMouvLast.ToString()}";
    }

}
