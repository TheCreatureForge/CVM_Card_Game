using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TestingNetcodeUI : MonoBehaviour
{
   [SerializeField] 
   private Button startHostButton;

   [SerializeField] 
   private Button startClientButton;


    private void Awake()
    {
        startHostButton.onClick.AddListener(() =>{
            Debug.Log("HOST");
            NetworkManager.Singleton.StartHost();
            Hide();
        });

        startClientButton.onClick.AddListener(() =>{
            Debug.Log("Client");
            NetworkManager.Singleton.StartClient();
            Hide();
        });
    }
    void Update(){
        //Debug.Log("Players that joined:" + )
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
