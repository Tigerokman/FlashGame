using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;

public class WinnerUI : PanelUI
{
    [SerializeField] private TMP_Text _winner;

    public void ShowWinner(string name, float restartDelay)
    {
        OpenPanel();
        _winner.text = name;
        RpcShowWinner(name);
        StartCoroutine(RestartDelay(restartDelay));
    }

    [ClientRpc]
    private void RpcCloseWinner()
    {
        ClosePanel();
    }

    [ClientRpc]
    private void RpcShowWinner(string name)
    {
        OpenPanel();
        _winner.text = name;
    }

    private IEnumerator RestartDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClosePanel();
        RpcCloseWinner();
    }
}
