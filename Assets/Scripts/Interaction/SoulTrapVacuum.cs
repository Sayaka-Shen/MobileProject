using UnityEngine;

public class SoulTrapVacuum : MonoBehaviour, IInteractable
{
    private SoulPlayer _soulPlayer;

    private void Start()
    {
        _soulPlayer = GameManager.Instance.SoulPlayer;
    }

    public void Interact()
    {
        if(_soulPlayer.AsSoul)
        {
            _soulPlayer.DeleteSoul();
            SoulsManager.Instance.AddSoulsCorrupt();
            GooglePlayAuthentification.Instance.UnlockAchievement("CgkIp4bqwJwIEAIQCQ");
        }
    }
}
