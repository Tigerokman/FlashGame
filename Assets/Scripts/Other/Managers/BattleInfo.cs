using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WinnerUI))]
public class BattleInfo : NetworkBehaviour
{
    [SerializeField] private List<string> _names;
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _playerUIInfo;

    private WinnerUI _winnerUi;

    private void Awake()
    {
        _winnerUi = GetComponent<WinnerUI>();
    }

    public void AddNewPlayer(PlayerScore playerScore)
    {
        StartCoroutine(AddPlayerDelay(playerScore));
    }

    public void ShowWinner(int scoreToWin, float restartDelay)
    {
        for (int i = 0; i < _container.transform.childCount; i++)
        {
            if (scoreToWin == _container.transform.GetChild(i).GetComponent<PlayerUIInfo>().Score)
            {
                _winnerUi.ShowWinner(_container.transform.GetChild(i).GetComponent<PlayerUIInfo>().Name, restartDelay);
                break;
            }
        }
    }

    private string GetRandomName(PlayerScore playerScore)
    {
        return _names[Random.Range(0, _names.Count - 1)] + playerScore.Id;
    }

    [ClientRpc]
    private void RpcAddPlayer(string name,PlayerScore playerScore)
    {
        GameObject newPlayer = Instantiate(_playerUIInfo, _container.transform) as GameObject;
        newPlayer.GetComponent<PlayerUIInfo>().Init(name,playerScore);
    }

    private IEnumerator AddPlayerDelay(PlayerScore playerScore)
    {
        yield return new WaitForSeconds(0.5f);
        RpcAddPlayer(GetRandomName(playerScore),playerScore);
    }
}
