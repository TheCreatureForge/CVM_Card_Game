using UnityEngine;

public class TurnDirector : MonoBehaviour
{

    [SerializeField] int turnCounter; 

    [SerializeField] bool Player1Ready;
    [SerializeField] bool Player2Ready;

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
        if(Player1Ready && Player2Ready){
            turnCounter++;
            Player1Ready = false;
            Player2Ready = false;
        }
    }

    

}
