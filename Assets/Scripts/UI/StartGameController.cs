using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class StartGameController : MonoBehaviour
    {
        private const int DELAY_SECONDS = 1;
        
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Button _startButton;
        [SerializeField] private Text _text;
        [SerializeField] private int _timeToStart = 3;

        private void Awake()
        {
            _startButton.onClick.AddListener(StartGame);
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
            _gameManager.StartGame();
            gameObject.SetActive(false);
        }
    }
}