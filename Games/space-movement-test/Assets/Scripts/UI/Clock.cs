using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private string time;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        time = System.DateTime.Now.ToString("HH:mm");
        text.text = string.Format(time);
    }
}