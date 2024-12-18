using UnityEngine;

public class sTrack : MonoBehaviour
{
    [SerializeField] Transform target;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
        

    }
}
