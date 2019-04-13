using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text countText;

    public void SetElementCount(int amount)
    {
        countText.text = "Total live objects: " + amount.ToString();
    }
}