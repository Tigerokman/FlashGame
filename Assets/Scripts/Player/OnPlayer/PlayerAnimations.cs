using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerCharge))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerAnimations : NetworkBehaviour
{
    [SerializeField] private Animator _animator;

    private PlayerCharge _playerCharge;
    private PlayerMovement _playerMovement;
    private PlayerHealth _playerHealth;

    #region BuiltInMethods
    private void Awake()
    {
        _playerCharge = GetComponent<PlayerCharge>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        _playerCharge.Charged += ChangeChargeAnim;
        _playerMovement.RunChanged += ChangeRunAnim;
        _playerHealth.DamageTaken += TakeDamageAnim;
    }

    private void OnDisable()
    {
        _playerCharge.Charged -= ChangeChargeAnim;
        _playerMovement.RunChanged -= ChangeRunAnim;
        _playerHealth.DamageTaken -= TakeDamageAnim;
    }
    #endregion

    private void ChangeChargeAnim(bool isOn)
    {
        string isCharge = "IsCharge";
        _animator.SetBool(isCharge, isOn);
    }

    private void ChangeRunAnim(bool isOn)
    {
        string isRun = "IsRun";
        _animator.SetBool(isRun, isOn);
    }

    private void TakeDamageAnim()
    {
        string takeDamage = "Damaged";
        _animator.SetTrigger(takeDamage);
    }
}
