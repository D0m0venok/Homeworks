using UnityEngine;
using VG.Utilites;


namespace ShootEmUp
{
    [InjectTo]
    public sealed class FinishGameController : MonoBehaviour
    {
        [Inject] private GameManager _gameManager;
        
        public void FinishGame()
        {
            gameObject.SetActive(true);
            _gameManager.SetState(GameState.FINISHED);
        }
    }
}