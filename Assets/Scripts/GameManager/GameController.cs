using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class GameController : MonoBehaviour
    {
        private const int DELAY_SECONDS = 1;
        
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Text _text;
        [SerializeField] private string _finishText = "Game over!";
        [SerializeField] private int _timeToStart = 3;

        private bool _isPause;

        private void Awake()
        {
            _startButton.onClick.AddListener(StartGame);
            _pauseButton.onClick.AddListener(() =>
            {
                if (_isPause)
                    ResumeGame();
                else
                    PauseGame();
            });
            _pauseButton.gameObject.SetActive(false);
            _text.gameObject.SetActive(false);
        }
        
        public void FinishGame()
        {
            _text.color = Color.red;
            _text.text = _finishText; 
            _pauseButton.gameObject.SetActive(false);
            _text.gameObject.SetActive(true);
            _gameManager.FinishGame();
        }

        private async void StartGame()
        {
            _startButton.gameObject.SetActive(false);
            _text.gameObject.SetActive(true);
            for (var i = _timeToStart; i > 0 ; i--)
            {
                _text.text = i.ToString();
                
                await Task.Delay(TimeSpan.FromSeconds(DELAY_SECONDS));
            }
            _pauseButton.gameObject.SetActive(true);
            _text.gameObject.SetActive(false);
            _text.color = Color.blue;
            _gameManager.StartGame();
        }
        private void PauseGame()
        {
            _text.text = "PAUSE";
            _text.gameObject.SetActive(true);
            _gameManager.PauseGame();
            _isPause = true;
        }
        private void ResumeGame()
        {
            _text.gameObject.SetActive(false);
            _gameManager.ResumeGame();
            _isPause = false;
        }
    }
}