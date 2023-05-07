using Mirror;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerScore))]
[RequireComponent (typeof(ChargeCooldownUI))]
public class PlayerCharge : NetworkBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private BoxCollider _chargeCollider;

    private ChargeCooldownUI _cooldown;
    private PlayerScore _score;
    private Coroutine _chargeCoroutine;
    private bool _canCharge = true;
    private float _chargeTime = 0.20f;
    private float _chargeCooldown = 1.5f;

    public Action<bool> Charged;

    #region BuiltInMethods
    private void Awake()
    {
        _score = GetComponent<PlayerScore>();
        _cooldown = GetComponent<ChargeCooldownUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_chargeCollider.enabled == false)
            return;

        CheckCollider(other);
    }
    #endregion

    public void Charge()
    {
        if (_canCharge)
            _chargeCoroutine = StartCoroutine(ChargeRoutine());
    }

    private void CheckCollider(Collider other)
    {
        StopCharge();
        other.TryGetComponent(out PlayerHealth playerHealth);

        if(playerHealth != null)
        {
            bool isSucces = playerHealth.TryTakeDamage();

            if(isSucces) 
            {
                _score.CmdAddScore();
            }
        }
    }

    private void StopCharge()
    {
        StopCoroutine(_chargeCoroutine);
        OffChargeSettings();
    }

    private void OffChargeSettings()
    {
        Charged?.Invoke(false);
        _chargeCollider.enabled = false;
        _chargeCoroutine = null;
    }

    private IEnumerator ChargeCooldown()
    {
        _canCharge = false;
        yield return new WaitForSeconds(_chargeCooldown);
        _canCharge = true;
    }

    private IEnumerator ChargeRoutine()
    {
        _cooldown.StartCooldownUI(_chargeCooldown);
        _chargeCollider.enabled = true;
        StartCoroutine(ChargeCooldown());
        Charged?.Invoke(true);

        float expiredTime = _chargeTime;

        while(expiredTime >= 0)
        {
            Vector3 move = new Vector3(0, 0, 1);
            Vector3 velocity = move * _range * Time.deltaTime;
            transform.Translate(velocity);

            expiredTime -= Time.deltaTime;
            yield return null;
        }

        OffChargeSettings();
    }
}
