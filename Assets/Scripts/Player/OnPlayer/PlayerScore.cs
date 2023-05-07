using Mirror;
using System;

public class PlayerScore : NetworkBehaviour
{
    [SyncVar(hook = nameof(SyncScore))]
    private int _score = 0;

    [SyncVar(hook = nameof(SyncId))]
    private int _id = 0;

    public int Id => _id;
    public Action<int> ScoreChanged;

    [Command]
    public void CmdAddScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void ResetScore()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }

    public void SetId(int id)
    {
        _id = id;
    }

    private void SyncScore(int oldValue, int newValue)
    {
        _score = newValue;
        ScoreChanged?.Invoke(_score);
    }

    private void SyncId(int oldValue, int newValue)
    {
        _id = newValue;
    }
}
