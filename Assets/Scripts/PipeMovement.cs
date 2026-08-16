using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 0.65f;
    [SerializeField] private float _destroyXPosition = -2.5f;

    private void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying)
        {
            return;
        }

        transform.position += Vector3.left * _speed * Time.deltaTime;

        if (transform.position.x < _destroyXPosition)
        {
            Destroy(gameObject);
        }
    }
}
