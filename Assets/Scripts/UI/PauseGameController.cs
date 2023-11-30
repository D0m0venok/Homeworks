using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public sealed class PauseGameController : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Text _text;
        
        private GameManager _gameManager;

        private bool _isPause;
        
        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
            
            _pauseButton.onClick.AddListener(() =>
            {
                if (_isPause)
                    ResumeGame();
                else
                    PauseGame();
            });
            _text.gameObject.SetActive(false);
        }
        public void OnStartGame()
        {
            gameObject.SetActive(true);
        }
        public void OnFinishGame()
        {
            gameObject.SetActive(false);
        }
        
        private void PauseGame()
        {
            _text.gameObject.SetActive(true);
            _isPause = true;
            _gameManager.SetState(GameState.PAUSED);
        }
        private void ResumeGame()
        {
            _text.gameObject.SetActive(false);
            _isPause = false;
            _gameManager.SetState(GameState.PLAYING);
        }
    }
}