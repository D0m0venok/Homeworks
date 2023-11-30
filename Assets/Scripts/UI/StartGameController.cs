using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public sealed class StartGameController : MonoBehaviour
    {
        private const int DELAY_SECONDS = 1;

        [SerializeField] private Button _startButton;
        [SerializeField] private Text _text;
        [SerializeField] private int _timeToStart = 3;
        
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
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
            _gameManager.SetState(GameState.PLAYING);
            gameObject.SetActive(false);
        }
    }
}