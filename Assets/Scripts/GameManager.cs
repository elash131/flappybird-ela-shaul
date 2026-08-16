using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string BestScoreKey = "HighScore";

    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _birdPrefab;
    [SerializeField] private Vector3 _birdStartPosition = Vector3.zero;
    [SerializeField] private PipeSpawner _pipeSpawner;
    [SerializeField] private GameObject _startCanvas;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private GameObject _gameOverImage;
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    private GameObject _bird;
    private int _score;
    private GameState _gameState = GameState.PreGame;

    public bool IsPlaying => _gameState == GameState.Game;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GameReset();
        ShowStartScreen();
    }

    private void Update()
    {
        if (_gameState == GameState.GameOver && WasMenuPressed())
        {
            RestartGame();
        }
    }

    private bool WasMenuPressed()
    {
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
    }

    private void GameReset()
    {
        SetScore(0);

        if (_bird != null)
        {
            Destroy(_bird);
            _bird = null;
        }

        if (_pipeSpawner != null)
        {
            _pipeSpawner.ResetSpawner();
        }
    }

    private void ShowStartScreen()
    {
        _gameState = GameState.PreGame;

        if (_startCanvas != null)
        {
            _startCanvas.SetActive(true);
        }

        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(false);
        }
    }

    private void ShowGameOverScreen()
    {
        if (_startCanvas != null)
        {
            _startCanvas.SetActive(false);
        }

        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(true);
        }

        if (_gameOverImage != null)
        {
            _gameOverImage.SetActive(true);
        }
    }

    private void HideMenuScreens()
    {
        if (_startCanvas != null)
        {
            _startCanvas.SetActive(false);
        }

        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(false);
        }
    }

    public void StartGame()
    {
        if (_gameState != GameState.PreGame)
        {
            return;
        }

        if (!CreateBird())
        {
            return;
        }

        _gameState = GameState.Game;
        HideMenuScreens();
    }

    private bool CreateBird()
    {
        if (_birdPrefab == null)
        {
            Debug.LogError("Bird prefab is missing on GameManager.");
            return false;
        }

        _bird = Instantiate(_birdPrefab, _birdStartPosition, Quaternion.identity);
        return true;
    }

    public void GameOver()
    {
        if (_gameState == GameState.GameOver)
        {
            return;
        }

        _gameState = GameState.GameOver;
        ShowGameOverScreen();
    }

    public void RestartGame()
    {
        GameReset();
        ShowStartScreen();
    }

    public void PressMenuButton()
    {
        if (_gameState == GameState.PreGame)
        {
            StartGame();
        }
        else if (_gameState == GameState.GameOver)
        {
            RestartGame();
        }
    }

    public void AddScore()
    {
        if (_gameState != GameState.Game)
        {
            return;
        }

        SetScore(_score + 1);
    }

    private void SetScore(int score)
    {
        _score = score;

        if (_currentScoreText != null)
        {
            _currentScoreText.text = _score.ToString();
        }

        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        int bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);

        if (_score > bestScore)
        {
            bestScore = _score;
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
        }

        if (_bestScoreText != null)
        {
            _bestScoreText.text = bestScore.ToString();
        }
    }
}
