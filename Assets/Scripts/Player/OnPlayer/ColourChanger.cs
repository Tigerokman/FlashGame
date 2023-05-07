using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class ColourChanger : NetworkBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    private PlayerHealth _health;
    private Material _material;

    #region BuiltInMethods
    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        _health.IsImmortal += ColourChangeServer;
    }

    private void OnDisable()
    {
        _health.IsImmortal -= ColourChangeServer;
    }

    private void Start()
    {
        _material = _skinnedMeshRenderer.materials[1];
    }
    #endregion

    [Server]
    private void ColourChangeServer(bool isStart)
    {
        ColourChange(isStart);
        RpcColourChange(isStart);
    }

    [ClientRpc]
    private void RpcColourChange(bool isStart)
    {
        ColourChange(isStart);
    }

    private void ColourChange(bool isStart)
    {
        if (isStart)
            _material.color = Color.green;
        else
            _material.color = Color.white;
    }
}
