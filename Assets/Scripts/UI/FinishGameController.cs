using UnityEngine;
using VG.Utilites;


namespace ShootEmUp
{
    public sealed class FinishGameController : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;
        
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