using UnityEngine;
using UnityEngine.UI;

public class UpDownSoundOptionBt : MonoBehaviour
{
    public void UpSlider()
    {
        this.GetComponent<Slider>().value += 0.1f;
    }
    public void DownSlider()
    {
        this.GetComponent<Slider>().value -= 0.1f;
    }
}
