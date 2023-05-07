using Mirror;
using UnityEngine;

public class PanelUI : NetworkBehaviour
{
    [SerializeField] private GameObject _panel;

    protected void OpenPanel()
    {
        _panel.SetActive(true);
    }

    protected void ClosePanel()
    {
        _panel.SetActive(false);
    }
}
