using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public bool flag; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //use flag to determine which statement to print
        if (flag)
        {
            Debug.Log("Boolean flag is set.");
        }
        else
        {
            Debug.Log("Boolean flag isn't set.");
        }

        //Print first ten powers of 2 using a for loop
        for (int x = 1; x <=10; x++)
        {
            int y = (int)Mathf.Pow(2,x);
            Debug.Log($"The {x} power of 2 is {y}.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
