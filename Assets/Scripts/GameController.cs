using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<MoleController> _moles = new();

    [SerializeField]
    private GameObject _gameOverPanel;

    public int GameTime { get => (int)_gameSessionTime; }

    private float _gameSessionTime = 300f;

    private float _spawnTime = 2f;

    private bool _isGameOver = false;

    private void Start()
    {
        StartCoroutine(MoleSpawnCoroutine());
    }

    private void Update()
    {
        _gameSessionTime -= Time.deltaTime;

        if (_gameSessionTime < 0 )
        {
            //Debug.Log("Game over!");
            _isGameOver = true;

            _gameOverPanel.SetActive(true);
        }
    }

    private IEnumerator MoleSpawnCoroutine()
    {
        while (!_isGameOver)
        {
            //int moleIndex = 0;
            int moleIndex = Random.Range(0, 5);

            _moles[moleIndex].SpawnMole();
            yield return new WaitForSeconds(_spawnTime);
        }
    }
}
