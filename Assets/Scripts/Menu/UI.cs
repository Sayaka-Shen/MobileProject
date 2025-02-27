using System;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [Header("UI Settings Movement")]
    [SerializeField] private TextMeshProUGUI _textMovement;
    [SerializeField] private TextMeshProUGUI _textScore;
    private MovementPlayer _movementPlayer;
    private SoulsManager _soulsManager;

    private void Start()
    {
        // Get the components
        _movementPlayer = GameManager.Instance.MovementPlayer;
        _soulsManager = SoulsManager.Instance;
        
        // Add the event
        _movementPlayer.NbCaseMoveLastChage += ChangeMoveCaseUI;
        Purify.OnPurify += ChangeScore;
        
        ChangeMoveCaseUI();
    }

    private void ChangeMoveCaseUI()
    {
        _textMovement.text = $"{_movementPlayer.NbCaseMouvLast.ToString()}";
    }

    private void ChangeScore()
    {
         _textScore.text = $"{_soulsManager.CountSoulsPurify.ToString()}"; 
    }
}
