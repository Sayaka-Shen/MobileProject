using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SoulPlayer : MonoBehaviour
{
    public bool AsSoul {  get; private set; }
    [SerializeField] private Light2D _light;

    private void Awake()
    {
        AsSoul = false;
    }

    public void TakeSoul(int soulState)
    {
        _light.enabled = true;
        _light.color = SoulsManager.Instance.SpritesColor[soulState];
        AsSoul = true;
    }

    public void DeleteSoul()
    {
        _light.enabled = false;
        AsSoul = false;
    }
}
