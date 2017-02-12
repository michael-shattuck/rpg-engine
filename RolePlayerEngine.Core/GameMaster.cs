using UnityEngine;

namespace RolePlayerEngine.Core
{
    [AddComponentMenu("RolePlayerEngine/GameMaster")]
    public class GameMaster : MonoBehaviour
    {
        private static GameMaster _instance;
//        public static GameState CurrentState;

        public static GameMaster Instance => _instance ?? (_instance = new GameMaster());
        public GameObject InventoryPanel;

        public void Quit()
        {
            Application.Quit(); 
        }
    }
}