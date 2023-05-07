using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkRoomManager
{
    public static new MyNetworkManager singleton { get; private set; }

    public override void Awake()
    {
        base.Awake();
        singleton = this;
    }

    public override void OnRoomServerSceneChanged(string sceneName)
    {
    }

    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
    {
        return true;
    }

    public override void OnRoomStopClient()
    {
        base.OnRoomStopClient();
    }

    public override void OnRoomStopServer()
    {
        base.OnRoomStopServer();
    }

    bool showStartButton;

    public override void OnRoomServerPlayersReady()
    {
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
        showStartButton = true;
#endif
    }

    public override void OnGUI()
    {
        base.OnGUI();

        if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
        {
            showStartButton = false;

            ServerChangeScene(GameplayScene);
        }
    }
}
