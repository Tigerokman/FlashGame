using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
public class Player : NetworkBehaviour
{
    #region BuiltInMethods
    private void Start()
    {
        if (isClient && isLocalPlayer)
        {
            SetPlayer();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    #endregion

    private void SetPlayer()
    {
        PlayerInitManager.Instance.SetLocalPlayer(transform);
        CmdSetInfo();
    }

    [Command]
    private void CmdSetInfo()
    {
        PlayerInitManager.Instance.SetPlayerToServer(this.gameObject);
    }
}
