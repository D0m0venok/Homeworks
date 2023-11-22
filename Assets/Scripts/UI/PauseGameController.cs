using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public sealed class PauseGameController : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Text _text;
        
        private bool _isPause;
        
        private void Awake()
        {
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
            _gameManager.PauseGame();
        }
        private void ResumeGame()
        {
            _text.gameObject.SetActive(false);
            _isPause = false;
            _gameManager.ResumeGame();
        }
    }
}