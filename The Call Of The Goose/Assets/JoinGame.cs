using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour
{
    private List<GameObject> roomList = new List<GameObject>();

    [SerializeField] 
    private Text status;

    [SerializeField] 
    private GameObject roomListItemPrefab;

    [SerializeField] private Transform roomListParent;
    
    /*private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        RefreshRoomList();
    }

    public void RefreshRoomList()
    {
        networkManager.matchMaker.ListMatches(0, 10, "", OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(ListMatchResponse matchList)
    {
        status.text = "";

        if (matchList == null)
        {
            status.text = "No matches";
            return;
        }

        ClearRoomList();
        foreach (MatchDesc match in matchList.matches)
        {
            GameObject roomListItemGo = Instantiate(roomListItemPrefab);
            roomListItemGo.transform.SetParent(roomListParent);
            
            roomList.Add(roomListItemGo);
        }

        if (roomList.Count == 0)
        {
            status.text = "No rooms at the moment";
        }
    }*/

    void ClearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        
        roomList.Clear();
    }
}
