using Unity.Netcode;
using UnityEngine;

public class CustomSpawnManager : NetworkBehaviour
{
    public Transform[] spawnPoints;
    public GameObject playerPrefab;

    private NetworkVariable<int> playerInGame = new NetworkVariable<int>();
    public int PlayerInGame
    {
        get{
            return playerInGame.Value;
        }
    }

 

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if(IsServer){
                GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[id].position, spawnPoints[id].rotation);
                playerInstance.name =$"Player_{id}";
                playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(id);
                Debug.Log(id + " Connected");
                playerInGame.Value++;
            }
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if(IsServer){
                Debug.Log(id  + " Disconnected");
                playerInGame.Value--;
            }
        };
    }
}
