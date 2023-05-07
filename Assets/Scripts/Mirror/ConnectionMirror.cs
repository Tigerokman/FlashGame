using UnityEngine;
using Mirror;

public class ConnectionMirror : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;

    private void Start()
    {
        if (!Application.isBatchMode)
        {
            _networkManager.StartClient();
        }
    }

    public void JoinClient()
    {
        _networkManager.networkAddress = "localhost";
        _networkManager.StartClient();
    }
}
