using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class TurnDirector : MonoBehaviour
{

    public List<playerScript> players = new List<playerScript>();

    public void CheckTurnReady(){
        if(players.Count < 2) return;

        if(players.All(p => !string.IsNullOrEmpty(p.intent.Value.ToString()))){
            Invoke("doActions", 1f);
        }
    }
    
    void doActions(){
        
        var p1 = players[0];
        var p2 = players[1];


        if(p1.intent.Value == p2.intent.Value){
            p1.TakeDamage(2);
            p2.TakeDamage(2);
        }else if (p1.intent.Value == "Attack" && p2.intent.Value == "Parry"){
            p1.TakeDamage(4);
        }else if (p1.intent.Value == "Parry" && p2.intent.Value == "Attack"){
            p2.TakeDamage(4);
        }else if (p1.intent.Value == "Block" && p2.intent.Value == "Parry"){
            p2.TakeDamage(1);
        }else if (p1.intent.Value == "Parry" && p2.intent.Value == "Block"){
            p1.TakeDamage(1);
        }else if (p1.intent.Value == "Parry" && p2.intent.Value == "Parry"){
            p1.TakeDamage(2);
            p2.TakeDamage(2);
        }else if (p1.intent.Value == "Attack" && p2.intent.Value == "Block"){
            p2.TakeDamage(1);
        }else if (p1.intent.Value == "Block" && p2.intent.Value == "Attack"){
            p1.TakeDamage(1);
        }

        p1.Unready();
        p2.Unready();

    }



}
