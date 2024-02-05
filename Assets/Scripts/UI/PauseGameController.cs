using UnityEngine;
using UnityEngine.UI;
using VG.Utilites;

namespace ShootEmUp
{
    [InstallMono, InjectTo]
    public sealed class PauseGameController : Entity, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Text _text;
        [Inject] private GameManager _gameManager;

        private bool _isPause;
        
        public void OnStartGame()
        {
            _pauseButton.onClick.AddListener(() =>
            {
                if (_isPause)
                    ResumeGame();
                else
                    PauseGame();
            });
            _text.gameObject.SetActive(false);
            
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