using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private bool _isLoopStarted;
    [SerializeField] private bool _isPlayerDead;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clips;

    [SerializeField] private Image _blackScreen;
    [SerializeField] private Text _playButton;
    [SerializeField] private Text _restartButton;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _backToMenu;
    [SerializeField] private Text _highScoreText;
    [SerializeField] private GameObject[] _carModels;

    private float _score;
    private int seconds;
    public bool isGameStarted = false;

    private void Start()
    {
        Data.isAlive = true;
        _scoreText.enabled = true;

        _blackScreen.enabled = false;
        _restartButton.enabled = false;
        _highScoreText.enabled = false;
        _backToMenu.enabled = false;

        for (int i = 0; i < _carModels.Length; i++)
        {
            _carModels[i].SetActive(false);
        }

        if (Data.ChosenCar is null)
        {
            _carModels[0].SetActive(true);
        }
        else
        {
            switch (Data.ChosenCar.Name)
            {
                case "InitialD":
                    _carModels[0].SetActive(true);
                    break;
                case "Cyberpunk":
                    _carModels[1].SetActive(true);
                    break;
                case "Retro":
                    _carModels[2].SetActive(true);
                    break;
                default:
                    break;
            }
        }
        int randomNum = Random.Range(0, _clips.Length);
        _audioSource.PlayOneShot(_clips[randomNum]);
    }

    private void Update()
    {
        if (Data.isGameStarted)
        {
            _score += Time.deltaTime;
            DisplayTime(_score);
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        seconds = Mathf.FloorToInt(timeToDisplay % 60);

        _scoreText.text = seconds.ToString();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ReloadLevel()
    {
        _player.Speed = 0.1f;
        Time.timeScale = 1;
        _scoreText.enabled = true;
        _isPlayerDead = false;
        Data.isAlive = true;
        SceneManager.LoadScene(1);
    }


    public void Death()
    {
        Data.isGameStarted = false;
        Data.isAlive = false;
        _player.Speed = 0f;
        _highScoreText.enabled = true;
        if (seconds > Data.HighScore)
        {
            Data.HighScore = seconds;
        }
        _isPlayerDead = true;
        _audioSource.Stop();
        //Time.timeScale = 0;
        _highScoreText.text = "HighScore: " + Data.HighScore.ToString();
        _blackScreen.enabled = true;
        _restartButton.enabled = true;
        _backToMenu.enabled = true;
    }
}
