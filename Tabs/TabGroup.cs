using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    //[SerializeField] private Text titleText;
    [SerializeField] private List<TabButtonUI> tabButtons;
    [SerializeField] private List<GameObject> objectsToSwap;

    public void OnTabSelected(TabButtonUI button)
    {
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            //objectsToSwap[0].SetActive(true); < might fix a bug?
            if (i == index)
            {
                //titleText.text = objectsToSwap[i].name;
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }
}