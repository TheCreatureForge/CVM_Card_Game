using TMPro;
using UnityEngine;

public class TurnDirector : MonoBehaviour
{

    [SerializeField] int turnCounter; 

    public bool Player1Ready;
    public bool Player2Ready;

    public TextMeshPro P1ReadyText;
    public TextMeshPro P2ReadyText;

    public GameObject Player1;
    public GameObject Player2;

    bool InCombat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnCounter = 0;
        Player1Ready = false;
        Player2Ready = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Player1Ready){
            P1ReadyText.text = "READY";
        }else{
            P1ReadyText.text = "";
        }

        if(Player2Ready){
            P2ReadyText.text = "READY";
        }else{
            P2ReadyText.text = "";            
        }

        if(Player1Ready && Player2Ready && !InCombat){
            InCombat = true;
            Invoke("StartCombat", 1f);
            
        }
    }

    void StartCombat(){

        InCombat = false;
        Player1Ready = false;
        Player2Ready = false;
        turnCounter++;
        Player1.GetComponent<playerScript>().DoAction();
        Player2.GetComponent<playerScript>().DoAction();
        
    }



}
