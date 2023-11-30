using UnityEngine;
using Zenject;


namespace ShootEmUp
{
    public sealed class FinishGameController : MonoBehaviour
    {
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public void FinishGame()
        {
            gameObject.SetActive(true);
            _gameManager.SetState(GameState.FINISHED);
        }
    }
}