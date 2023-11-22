using UnityEngine;

namespace ShootEmUp
{
    public sealed class FinishGameController : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        
        public void FinishGame()
        {
            gameObject.SetActive(true);
            _gameManager.FinishGame();
        }
    }
}