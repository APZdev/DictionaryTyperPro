using UnityEngine;
using UnityEngine.UI;

public class LanguageItem : MonoBehaviour
{
    [SerializeField] private Text itemText;

    public void SetName(string _text)
    {
        itemText.text = _text;
    }
}
