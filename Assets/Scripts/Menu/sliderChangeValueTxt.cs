using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sliderChangeValueTxt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txt;

    public void ChangeValue()
    {
        _txt.text = this.GetComponent<Slider>().value.ToString() + "%";
    }
}
