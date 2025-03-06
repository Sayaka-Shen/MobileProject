using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sliderChangeValueTxt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txt;

    public void ChangeValue()
    {
        _txt.text = ((int)(this.GetComponent<Slider>().value * 100)).ToString() + "%";
    }
}
