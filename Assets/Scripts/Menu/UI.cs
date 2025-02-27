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
        _movementPlayer.OnEndMove += ChangeMoveCaseUI;
        Purify.OnPurify += ChangeScore;
        
        ChangeMoveCaseUI();
    }

    private void ChangeMoveCaseUI()
    {
        string nbCaseRemaining = (_soulsManager.CountForHurt - (_movementPlayer.NbCaseMouv % _soulsManager.CountForHurt)).ToString();
        _textMovement.text = $"{nbCaseRemaining}";
    }

    private void ChangeScore()
    {
         _textScore.text = $"{_soulsManager.CountSoulsPurify.ToString()}"; 
    }
}
