using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpawner : NetworkBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;

    private Queue<Transform> _emptySpawnPoints = new Queue<Transform>();

    public void ResetSpawnPoints()
    {
        _emptySpawnPoints.Clear();
        List<Transform> tempPoints = new List<Transform>();

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            tempPoints.Add(_spawnPoints[i]);
        }

        int addedIdPoint;

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            addedIdPoint = Random.Range(0, tempPoints.Count - 1);
            _emptySpawnPoints.Enqueue(tempPoints[addedIdPoint]);
            tempPoints.RemoveAt(addedIdPoint);
        }
    }

    public Transform GetPoint()
    {
        return _emptySpawnPoints.Dequeue();
    }

    [TargetRpc]
    public void SetPlayerInPoint(NetworkConnection client, GameObject player, Transform point )
    {
        player.transform.position = point.position;
    }
}
