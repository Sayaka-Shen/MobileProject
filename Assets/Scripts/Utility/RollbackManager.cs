using System;
using System.Collections.Generic;
using UnityEngine;

public enum ReverseActionType { PlayerMove, PlayerTp, SeekerMove, SoulDamage, SoulTake, SoulTaken, Purify, MorePower }
[Serializable]
public struct ReverseAction
{
    public ReverseActionType Type;
    public GameObject Holder;
    public Vector3 PositionTarget;
    public int ValueTarget;
}

public class RollbackManager : MonoBehaviour
{
    private List<List<ReverseAction>> _reverseActions = new List<List<ReverseAction>>(0);
    public List<List<ReverseAction>> ReverseActions { get { return _reverseActions; } }
    private int LastIndex => Instance._reverseActions.Count - 1;
    private int CountAction => Instance._reverseActions.Count;

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
        Instance._reverseActions[LastIndex].Add(action);
    }

    public void AddPlayerAction(ReverseAction playerAction)
    {
        Instance._reverseActions.Add(new List<ReverseAction>() { playerAction });
    }

    private List<Action> _rollback = new List<Action>();

    public void AddRollback()
    {
        if (_rollback.Count == _reverseActions.Count) return;
        _rollback.Add(Instance.RollBack);
        if (_rollback.Count == 1) RollBack();
    }

    public void TryUseRollback()
    {
        _rollback.RemoveAt(_rollback.Count - 1);
        if (Instance._rollback.Count != 0)
        {
            RollBack();
        }
    }

    private void RollBack()
    {
        if (CountAction == 0) return;
        List<ReverseAction> Actions = Instance._reverseActions[LastIndex];
        Actions.Reverse();
        foreach (ReverseAction action in Actions)
        {
            switch (action.Type)
            {
                case ReverseActionType.PlayerMove: // FAIT ?
                    if(action.ValueTarget != 0) GameManager.Instance.MovementPlayer.RerollMove(action.ValueTarget);
                    else{
                        GameManager.Instance.MovementPlayer.AddPosRollBack(action.PositionTarget);
                    }
                    break;
                case ReverseActionType.PlayerTp: //FAIT
                    GameManager.Instance.MovementPlayer.TPAt(action.PositionTarget);
                    break;
                case ReverseActionType.MorePower: //FAIT
                    action.Holder.SetActive(true);
                    break;
                case ReverseActionType.SeekerMove: //FAIT
                    if (action.ValueTarget != 0) action.Holder.GetComponent<Seeker>().RerollMove(action.ValueTarget);
                    else action.Holder.GetComponent<Seeker>().MoveTo(action.PositionTarget);
                    break;
                case ReverseActionType.SoulDamage: //FAIT
                    action.Holder.GetComponent<Soul>().Heal();
                    break;
                case ReverseActionType.SoulTaken: //FAIT
                    GameManager.Instance.SoulPlayer.TakeSoul(action.ValueTarget);
                    SoulsManager.Instance.RemoveSoulsCorrupt();
                    break;
                case ReverseActionType.SoulTake: //FAIT
                    action.Holder.GetComponent<Soul>().Reapere();
                    GameManager.Instance.SoulPlayer.DeleteSoul();
                    break;
                case ReverseActionType.Purify: //FAIT
                    GameManager.Instance.SoulPlayer.TakeSoul(action.ValueTarget);
                    SoulsManager.Instance.RemoveSoulsPurify();
                    break;
            }
        }
        Instance._reverseActions.RemoveAt(LastIndex);
    }
}