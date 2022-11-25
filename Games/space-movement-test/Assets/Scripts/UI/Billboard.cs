using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(2 * this.transform.position - GameObject.FindWithTag("MainCamera").transform.position);
    }
}