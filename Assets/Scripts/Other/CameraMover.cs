using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private float _speed = 5;

    private void Update()
    {
        transform.Translate(Vector3.back * _speed * Time.deltaTime);
    }
}
