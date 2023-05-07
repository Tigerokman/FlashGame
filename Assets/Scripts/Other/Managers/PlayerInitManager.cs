using Cinemachine;
using Mirror;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BattleFieldStarter))]
[RequireComponent(typeof(PlayerSkillsCanvas))]
public class PlayerInitManager : NetworkBehaviour
{
    #region Singletone
    private static PlayerInitManager _instance;

    public static PlayerInitManager Instance
    {
        get { return _instance; }
    }
    #endregion

    [SerializeField] private CinemachineFreeLook _cinemachine;
    [SerializeField] private Camera _camera;


    private BattleFieldStarter _battleField;
    private GameObject _currentPlayer;


    private void Awake()
    {
        _instance = this;
        _battleField = GetComponent<BattleFieldStarter>();
    }

    public void SetPlayerToServer(GameObject player)
    {
        _battleField.AddPlayer(player);
    }

    public void SetLocalPlayer(Transform player)
    {
        float delayToStart = 3;
        _currentPlayer = player.gameObject;
        StartCoroutine(StartDelay(delayToStart));
        _currentPlayer.GetComponent<PlayerMovement>().Init(_camera.transform);
        this.gameObject.GetComponent<PlayerSkillsCanvas>().SetSkill(_currentPlayer.GetComponent<PlayerCooldownUI>());
    }

    public void ChangeControlPlayer(bool isOn)
    {
        _currentPlayer.GetComponent<PlayerInputManager>().enabled = isOn;
        _battleField.StartSpawnPlayers();
        RpcChangeControlPlayer(isOn);
    }

    [ClientRpc]
    private void RpcChangeControlPlayer(bool isOn)
    {
        _currentPlayer.GetComponent<PlayerInputManager>().enabled = isOn;
    }

    private IEnumerator StartDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _cinemachine.LookAt = _currentPlayer.transform;
        _cinemachine.Follow = _currentPlayer.transform;
        _cinemachine.GetComponent<CameraMover>().enabled = false;

        if (isServer)
            ChangeControlPlayer(true);
    }
}
