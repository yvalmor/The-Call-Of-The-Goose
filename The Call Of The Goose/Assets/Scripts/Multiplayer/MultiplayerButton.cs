using UnityEngine;

namespace Multiplayer
{
    public class MultiplayerButton : MonoBehaviour
    {
        public GameObject CreatingRoomMenu, UsernameChoosingMenu;

        public void OnClick_MultiplayerButton()
        {
            Debug.Log(MasterManager.GameSettings.Username);
            if (MasterManager.GameSettings.Username.StartsWith("Username"))
                UsernameChoosingMenu.SetActive(true);
            else
                CreatingRoomMenu.SetActive(true);
        }
    }
}
