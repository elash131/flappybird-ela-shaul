using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private bool _hasScored;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (_hasScored || GameManager.Instance == null || !GameManager.Instance.IsPlaying)
        {
            return;
        }

        if (collider2D.gameObject.CompareTag("Player"))
        {
            _hasScored = true;
            GameManager.Instance.AddScore();
        }
    }
}
