using Mirror;
using UnityEngine;

public class NetworkStart : MonoBehaviour
{
    private void Awake()
    {
        NetworkManager.singleton.StartHost();
    }
}
