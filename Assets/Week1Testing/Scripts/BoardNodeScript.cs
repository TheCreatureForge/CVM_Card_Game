using Unity.VisualScripting;
using UnityEngine;

public class BoardNodeScript : MonoBehaviour
{

    public int side;
    public int column;
    public int row;


    public bool occupied;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardHighlighted(){
        if(occupied) return;
        Debug.Log("Highlighted");
        
    }

}
