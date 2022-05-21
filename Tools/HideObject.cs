using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HideObject : MonoBehaviour
{
    [SerializeField] private bool show;

    private void Start()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        List<GameObject> childObjects = new List<GameObject>();
        foreach (Transform child in allChildren)
        {
            if (!show)
            {
                childObjects.Add(child.gameObject);
                child.hideFlags = HideFlags.HideInHierarchy;
                gameObject.hideFlags = HideFlags.None;
            }
            else
            {
                childObjects.Add(child.gameObject);
                child.hideFlags = HideFlags.None;
                gameObject.hideFlags = HideFlags.None;
            }
        }
    }
}