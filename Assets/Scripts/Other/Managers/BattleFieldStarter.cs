using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattleInfo))]
[RequireComponent (typeof(PlayerInitManager))]
[RequireComponent(typeof(BattleSpawner))]
public class BattleFieldStarter : NetworkBehaviour
{
    private List<GameObject> _players = new List<GameObject>();
    private BattleInfo _battleInfo;
    private PlayerInitManager _playerInitManager;
    private BattleSpawner _battleSpawner;
    private float _restartDelay = 5;
    private int _scoreToWin = 3;

    private void Awake()
    {
        _battleInfo = GetComponent<BattleInfo>();
        _playerInitManager = GetComponent<PlayerInitManager>();
        _battleSpawner = GetComponent<BattleSpawner>();
    }

    public void AddPlayer(GameObject player)
    {
        _players.Add(player);
        PlayerScore score = player.GetComponent<PlayerScore>();
        score.SetId(_players.Count);
        _battleInfo.AddNewPlayer(score);
        score.ScoreChanged += CheckWinner;
    }

    public void StartSpawnPlayers()
    {
        _battleSpawner.ResetSpawnPoints();
        Transform spawnPoint;

        for (int i = 0; i < _players.Count; i++)
        {
            spawnPoint = _battleSpawner.GetPoint();
            _battleSpawner.SetPlayerInPoint(_players[i].GetComponent<NetworkIdentity>().connectionToClient, _players[i],spawnPoint);
        }
    }

    private void CheckWinner(int score)
    {
        if (_scoreToWin > score)
            return;

        _battleInfo.ShowWinner(_scoreToWin, _restartDelay);
        StartCoroutine(RestartRoutine());
        _playerInitManager.ChangeControlPlayer(false);
    }

    private void RestartScore()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].GetComponent<PlayerScore>().ResetScore();
        }
    }

    private IEnumerator RestartRoutine()
    {
        yield return new WaitForSeconds(_restartDelay);
        RestartScore();
        StartSpawnPlayers();
        _playerInitManager.ChangeControlPlayer(true);
    }
}
