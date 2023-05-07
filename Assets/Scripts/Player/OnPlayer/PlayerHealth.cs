using Mirror;
using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar(hook = nameof(SyncImmortal))]
    private bool _isImmortal = false;

    [SerializeField] private float _immortalCooldown;

    public Action<bool> IsImmortal;
    public Action DamageTaken;

    public bool TryTakeDamage()
    {
        if (_isImmortal)
        {
            return !_isImmortal;
        }

        CmdTakeDamage();

        if (isServer)
            return _isImmortal;
        else
            return !_isImmortal;
    }

    [Command(requiresAuthority = false)]
    private void CmdTakeDamage()
    {
        StartCoroutine(ImmotalCooldown());
    }

    private void SyncImmortal(bool isImmortalOld, bool isImmortalNew)
    {
        _isImmortal = isImmortalNew;

        if (_isImmortal == true) 
        DamageTaken();
    }

    private IEnumerator ImmotalCooldown()
    {
        _isImmortal = true;
        IsImmortal?.Invoke(_isImmortal);
        yield return new WaitForSeconds(_immortalCooldown);
        _isImmortal = false;
        IsImmortal?.Invoke(_isImmortal);
    }
}
