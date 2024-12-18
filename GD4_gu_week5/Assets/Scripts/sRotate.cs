using UnityEngine;

public class sRotate : MonoBehaviour
{

    [SerializeField] float vRotSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, vRotSpeed* Time.deltaTime, 0);
    }
}
