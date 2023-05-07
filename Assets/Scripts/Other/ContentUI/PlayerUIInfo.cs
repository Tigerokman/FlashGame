using System;
using TMPro;
using UnityEngine;

public class PlayerUIInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;

    private PlayerScore currentPlayer;

    public string Name => _name.text;
    public int Score => Int32.Parse(_score.text);

    private void OnDisable()
    {
        currentPlayer.ScoreChanged -= SetScore;
    }

    public void Init(string name, PlayerScore playerScore)
    {
        _name.text = name;
        playerScore.ScoreChanged += SetScore;
        currentPlayer = playerScore;
    }

    private void SetScore(int score)
    {
        _score.text = score.ToString();
    }
}
