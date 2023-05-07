using Mirror;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(PlayerCharge))]
public class PlayerMovement : NetworkBehaviour
{
    private float _speed = 5;
    private Vector3 _lastMove = Vector3.zero;
    private Transform _camera;
    private bool _canMove = true;

    public Action<bool> RunChanged;

    private void OnEnable()
    {
        this.GetComponent<PlayerCharge>().Charged += CanMoveChanger;
    }

    private void OnDisable()
    {
        this.GetComponent<PlayerCharge>().Charged -= CanMoveChanger;
    }

    public void Init(Transform camera)
    {
        _camera = camera;
    }

    public void Move(Vector2 direction)
    {
        if (_canMove == false)
            return;

        Vector3 move = new Vector3(direction.x * _speed, 0, direction.y * _speed);

        if (_lastMove != move && _lastMove == Vector3.zero)
        {
            RunChanged?.Invoke(true);
        }
        else if (_lastMove != move && move == Vector3.zero)
        {
            RunChanged?.Invoke(false);
        }


        _lastMove = move;

        if (direction == Vector2.zero)
            return;

        transform.rotation = new Quaternion(0, _camera.rotation.y, 0, _camera.rotation.w);
        transform.Translate(move * Time.deltaTime);
    }

    private void CanMoveChanger(bool isStarted)
    {
        _canMove = !isStarted;
    }
}
