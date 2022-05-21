using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public bool show = true;
    private string time;
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (!show) { return; }
        time = System.DateTime.Now.ToString("h:mm tt");
        text.text = string.Format(time);
    }
}