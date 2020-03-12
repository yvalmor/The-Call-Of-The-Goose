using UnityEngine;
using UnityEngine.Networking;

public class Host : MonoBehaviour
{
    [SerializeField] 
    private uint roomSize = 4;
    
    private string roomName;

    private NetworkManager managedNetwork;

    private void Start()
    {
        managedNetwork = NetworkManager.singleton;
        if (managedNetwork.matchMaker == null)
        {
            managedNetwork.StartMatchMaker();
        }
    }

    public void SetRoomName(string name)
    {
        roomName = name;
    }

    public void CreateRoom()
    {
        if (roomName != "" && roomName != null)
        {
            Debug.Log("Creating Room : " + roomName + " with room for " + roomSize + " players");
            //managedNetwork.matchMaker.CreateMatch(roomName, roomSize, true,"", managedNetwork.OnMatchCreate)
        }
    }
}
