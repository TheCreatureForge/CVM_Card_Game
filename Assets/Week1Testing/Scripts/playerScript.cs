using System;
using TMPro;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Collections;
using Unity.Collections;


public class playerScript : NetworkBehaviour
{

    [SerializeField] 
    public NetworkVariable<int> Health = new NetworkVariable<int>(10);
    public int PlayerNumber;
    public TextMeshPro HPText;
    public GameObject Enemy;
    public GameObject UI;
    Button attackButton;
    Button blockButton;
    Button ParryButton;

    public GameObject readyUI;

    public NetworkVariable<FixedString32Bytes> intent = new NetworkVariable<FixedString32Bytes>();
    public NetworkVariable<bool> isReady = new NetworkVariable<bool>(false);


    TurnDirector TurnDirector;


    public override void OnNetworkSpawn()
    {
        if(IsServer){
            TurnDirector = GameObject.Find("TurnDirector").GetComponent<TurnDirector>();
            TurnDirector.players.Add(this);
        }   

        HPText.text = Health.Value.ToString();
        readyUI = transform.Find("ReadyText").gameObject;
        isReady.OnValueChanged += (oldVal, newVal) =>
        {
            if (readyUI != null)
            readyUI.SetActive(newVal);
        };

        if (OwnerClientId == 0)
        {
            gameObject.transform.Find("Bubo").gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("Mubo").gameObject.SetActive(true);
        }

        if (IsOwner)
        {
            //this gives the gameobject the name Player_[id]; I don't know if this needed
            gameObject.name = $"Player_{OwnerClientId}";
            GameObject playerButtons = Instantiate(UI);
            attackButton = playerButtons.transform.Find("AttackButton")?.GetComponent<Button>();
            blockButton = playerButtons.transform.Find("BlockButton")?.GetComponent<Button>();
            ParryButton = playerButtons.transform.Find("ParryButton")?.GetComponent<Button>();
            
            

            NetworkManager.Singleton.OnClientConnectedCallback += OnAnyClientConnected;

           
            attackButton.onClick.AddListener(() =>
            {
                SetIntentServerRpc("Attack");
                readyUI.SetActive(true);
                attackButton.interactable = false;
                blockButton.interactable = false;
                ParryButton.interactable = false;
    
            });
        
            blockButton.onClick.AddListener(() => {
                SetIntentServerRpc("Block");
                readyUI.SetActive(true);
                attackButton.interactable = false;
                blockButton.interactable = false;
                ParryButton.interactable = false;
            });
            
            ParryButton.onClick.AddListener(() => {
                SetIntentServerRpc("Parry");
                readyUI.SetActive(true);
                attackButton.interactable = false;
                blockButton.interactable = false;
                ParryButton.interactable = false;
            });

            AssignEnemy();

        }
        else
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
        }
    }

     void OnAnyClientConnected(ulong clientId)
    {
        AssignEnemy();
    }


    [ServerRpc]
    void SetIntentServerRpc(string action){
        if(intent.Value== ""){
            intent.Value = new FixedString32Bytes(action);
            isReady.Value = true;
            TurnDirector.CheckTurnReady();
        }
    }

    void Update()
    {
        HPText.text = Health.Value.ToString();
    }

    public void Unready(){
        intent.Value = new FixedString32Bytes();
        isReady.Value = false;
        readyUI.SetActive(false);
        EnableButtonsClientRpc();
        
    } 
 
    void AssignEnemy(){
        if (Enemy != null) return;
        
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.ClientId == OwnerClientId) continue;
           
            var otherPlayer = client.PlayerObject;
            if (otherPlayer != null && otherPlayer.IsSpawned && otherPlayer.gameObject.activeInHierarchy)
            {
                Enemy = otherPlayer.gameObject;
                break;
            }
        }
    }



    public void TakeDamage(int damage){
        Health.Value -= damage;
        if (IsServer && Health.Value <= 0){
            Destroy(gameObject);
        }
    }

    [ClientRpc]
    void EnableButtonsClientRpc(){
        attackButton.interactable = true;
        blockButton.interactable = true;
        ParryButton.interactable = true;
    }




}
