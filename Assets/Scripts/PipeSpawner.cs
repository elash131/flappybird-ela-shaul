using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float _maxTime = 1.5f;
    [SerializeField] private float _heightRange = 0.45f;
    [SerializeField] private GameObject _pipe;

    private readonly List<GameObject> _pipes = new();
    private float _timer;
    private bool _hasSpawnedFirstPipe;

    private void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsPlaying)
        {
            return;
        }

        if (!_hasSpawnedFirstPipe)
        {
            SpawnPipe();
            _hasSpawnedFirstPipe = true;
        }

        _timer += Time.deltaTime;

        if (_timer >= _maxTime)
        {
            SpawnPipe();
            _timer -= _maxTime;
        }
    }

    private void SpawnPipe()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, Random.Range(-_heightRange, _heightRange));
        GameObject pipe = Instantiate(_pipe, spawnPosition, Quaternion.identity);
        _pipes.Add(pipe);
    }

    public void ResetSpawner()
    {
        foreach (GameObject pipe in _pipes)
        {
            if (pipe != null)
            {
                Destroy(pipe);
            }
        }

        _pipes.Clear();
        _timer = 0;
        _hasSpawnedFirstPipe = false;
    }
}
