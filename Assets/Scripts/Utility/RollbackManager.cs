using System;
using System.Collections.Generic;
using UnityEngine;

public enum ReverseActionType { PlayerMove, PlayerTp, SeekerMove, SoulDamage, SoulTake, SoulTaken, Purify, MorePower }
public struct ReverseAction
{
    public ReverseActionType Type;
    public GameObject Holder;
    public Vector3 PositionTarget;
    public int ValueTarget;
}

public class RollbackManager : MonoBehaviour
{
    [SerializeField] List<List<ReverseAction>> _reverseActions = new List<List<ReverseAction>>(0);

    public static RollbackManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Plus d'une Instance de RerollManager dans la scene !");
        }
    }

    public void Setup()
    {
        _reverseActions.Clear();
    }

    public void AddAction(ReverseAction action)
    {
        Instance._reverseActions[-1].Add(action);
    }

    public void AddPlayerAction(ReverseAction playerAction)
    {
        Instance._reverseActions.Add(new List<ReverseAction>() { playerAction });
    }

    public void Reroll()
    {
        List<ReverseAction> Actions = Instance._reverseActions[-1];
        Actions.Reverse();
        foreach (ReverseAction action in Actions)
        {
            switch (action.Type)
            {
                case ReverseActionType.PlayerMove: // FAIT ?
                    if(action.ValueTarget != 0) GameManager.Instance.MovementPlayer.RerollMove(action.ValueTarget);
                    if(action.PositionTarget != null) GameManager.Instance.MovementPlayer.AddPos(action.PositionTarget); ;
                    break;
                case ReverseActionType.PlayerTp: //FAIT
                    GameManager.Instance.MovementPlayer.TPAt(action.PositionTarget);
                    break;
                case ReverseActionType.MorePower: //FAIT
                    action.Holder.SetActive(true);
                    break;
                case ReverseActionType.SeekerMove: //FAIT
                    action.Holder.GetComponent<Seeker>().MoveTo(action.PositionTarget);
                    break;
                case ReverseActionType.SoulDamage: //FAIT
                    action.Holder.GetComponent<Soul>().Heal();
                    break;
                case ReverseActionType.SoulTaken: //FAIT
                    GameManager.Instance.SoulPlayer.TakeSoul();
                    SoulsManager.Instance.RemoveSoulsCorrupt();
                    break;
                case ReverseActionType.SoulTake: //FAIT
                    action.Holder.SetActive(true);
                    GameManager.Instance.SoulPlayer.DeleteSoul();
                    break;
                case ReverseActionType.Purify: //FAIT
                    GameManager.Instance.SoulPlayer.TakeSoul();
                    SoulsManager.Instance.RemoveSoulsPurify();
                    break;
            }
        }
        Instance._reverseActions.RemoveAt(-1);
    }
}