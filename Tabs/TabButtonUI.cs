using UnityEngine;
using UnityEngine.EventSystems;

public class TabButtonUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TabGroup tabGroup;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }
}