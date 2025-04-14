using System;
using TMPro;
using UnityEngine;

public class playerScript : MonoBehaviour
{

    [SerializeField] int Health = 10;
    [SerializeField] int Strength = 2;

    public int PlayerNumber;

    public TextMeshPro HPText;
    public GameObject Enemy;

    public String intent;

    public TurnDirector TurnDirector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TurnDirector = GameObject.Find("TurnDirector").GetComponent<TurnDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = Health.ToString();
        if (Health <= 0){
            Destroy(gameObject);
        }
    }

    
    
    public void setIntent(String command){
        if(PlayerNumber == 1){
            if(TurnDirector.Player1Ready) return;
            TurnDirector.Player1Ready = true;
        }else if(PlayerNumber == 2){
            if(TurnDirector.Player2Ready) return;
            TurnDirector.Player2Ready = true;
        }
        intent = command;
        Debug.Log(gameObject.name + " intends to " + command);
    }

    public void DoAction(){
        switch(intent){
            case "Attack":
                DealDamage(Enemy);
                break;
            case "Pass":
                Pass();
                break;
            default:
                Debug.Log("WTF");
                break;

        }
    }

    public void DealDamage(GameObject target){
        target.GetComponent<playerScript>().TakeDamage(Strength);
    }

    public void Pass(){
        Debug.Log(gameObject.name + " passes");
    }

    public void TakeDamage(int damage){
        Health -= damage;
    }





}
