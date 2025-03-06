using UnityEngine;
using UnityEngine.UI;

public class UpDownSoundOptionBt : MonoBehaviour
{
    public void UpSlider()
    {
        this.GetComponent<Slider>().value += 5f;
        Debug.Log("UpSlider");
    }
    public void DownSlider()
    {
        this.GetComponent<Slider>().value -= 5f;
        Debug.Log("DownSlider");
    }
}
