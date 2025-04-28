using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class CardBehavior : MonoBehaviour
{
    
    public int playerNumber = 1; //keeps track of which player owns the card. 1 = player 1, 2 = player 2 

    [SerializeField] GameObject gameBoard; //the gameboard, will auto setup in Start ***change when in mult
    [SerializeField] GameObject allyGrid;
    [SerializeField] GameObject enemyGrid;

    Rigidbody rb;

    [SerializeField] bool selected = false; //is the card selected by the player

    Vector3 mousePosition;

    [SerializeField] GameObject highlightedSpot;
    [SerializeField] GameObject spot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameBoard = GameObject.Find("GridHolder");
        rb = gameObject.GetComponent<Rigidbody>();

        if(playerNumber == 1){
            allyGrid = gameBoard.transform.GetChild(0).gameObject;
            enemyGrid = gameBoard.transform.GetChild(1).gameObject;
        }else {
            allyGrid = gameBoard.transform.GetChild(1).gameObject;
            enemyGrid = gameBoard.transform.GetChild(0).gameObject;
        }
    }

    

    void UpdateCardPosition(Vector3 pos){
        transform.DOMove(pos,0.1f);
        //transform.position = pos;
    }


    void OnMouseDown()
    {
        selected = true;
        mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        
    }

    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        pos.y = 2;
        UpdateCardPosition(pos);
        
    }

    void OnMouseUp()
    {
        selected = false;
        if(spot != null) spot.GetComponent<BoardNodeScript>().occupied = false;

        if(highlightedSpot != null){
            spot = highlightedSpot;
            highlightedSpot = null;
        }
        
        
        if(spot != null){
            var newPos = new Vector3(spot.transform.position.x, transform.position.y, spot.transform.position.z);
            spot.GetComponent<BoardNodeScript>().occupied = true;
            transform.DOMove(newPos,0.25f);
        }
        
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CardSlot" && selected){ //checks if the collision is an appropriate object
            var BoardScript = other.GetComponent<BoardNodeScript>();
            if(BoardScript.occupied) return; //returns if the collided spot is taken
            BoardScript.CardHighlighted();
            highlightedSpot = other.gameObject;
            
            
        }
    }

}
