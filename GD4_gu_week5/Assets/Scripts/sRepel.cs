using UnityEngine;

public class sRepel : MonoBehaviour
{
    public float vGravity;
    [SerializeField] Vector3 vDirection;
    [SerializeField] float vDistance;
    [SerializeField] GameObject[] vOther;
    [SerializeField] sPlayer sPlayer;
    [SerializeField] Transform Player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (sPlayer.fGameStart)
        {


            vOther = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject t in vOther)
            {

                vDistance = (transform.position - t.transform.position).magnitude;

                vDirection = (transform.position - t.transform.position).normalized;


                Rigidbody rb = t.GetComponent<Rigidbody>();






                if (rb != null)

                {
                    rb.AddForce(vDirection * vGravity / (vDistance * vDistance), ForceMode.Acceleration);




                }



            }
        }

    }
}
