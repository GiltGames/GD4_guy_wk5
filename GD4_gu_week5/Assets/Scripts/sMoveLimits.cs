using UnityEngine;

public class sMoveLimits : MonoBehaviour
{
    [SerializeField] Vector3 vStartPos;
    [SerializeField] float vOOBLow =-20;
    [SerializeField] float vOOBUp = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vStartPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
        //keep to the floor
        if(transform.position.y > vStartPos.y + vOOBUp)
        {
            transform.position = new Vector3(transform.position.x,vStartPos.y+vOOBUp, transform.position.z);
        }

        


    }
}
