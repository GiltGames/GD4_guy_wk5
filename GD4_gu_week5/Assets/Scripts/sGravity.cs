
using UnityEngine;

public class sGravity : MonoBehaviour
{
    [SerializeField] float vGravity;
    [SerializeField] Vector3 vDirection;
    [SerializeField] float vDistance;
    [SerializeField] Transform[] vOther;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vOther = FindObjectsByType<Transform>(FindObjectsSortMode.None);

    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (Transform t in vOther)
        {

            vDirection = (transform.position - t.position).normalized;
            vDistance = (transform.position - t.position).magnitude;

            Rigidbody rb = t.GetComponent<Rigidbody>();

            if (rb != null)

            {
                rb.AddForce(vDirection*vGravity/(vDistance*vDistance));




            }



        }


    }
}
