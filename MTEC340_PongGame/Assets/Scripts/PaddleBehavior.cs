using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
   
   public float Speed = 5.0f;


    public KeyCode UpDirection;
    public KeyCode DownDirection;
    
    

    
    void Start()
    {
        
    }

    
    void Update()
    {
        //Create a movement variable
        Vector3 movement = Vector3.zero;

        //Update variable based on player's input
        if (Input.GetKey(UpDirection))
        {
            Debug.Log("Up");
            movement.y += Speed;
        }

        if (Input.GetKey(DownDirection))
        {
            Debug.Log("Down");
            movement.y -= Speed;
        }

        //Consider frame rate to make game platform agnostic
        movement *= Time.deltaTime;

        //Apply movement to the current position
        transform.position += movement;
    }
}
