using UnityEngine;
using System.Collections;
public class sMoveandRespawn : MonoBehaviour
{
    [SerializeField] Vector3 vStartPos;
    [SerializeField] float vOOBLow = -20;
    [SerializeField] float vOOBUp = 0.2f;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject explode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vStartPos = transform.position;
       

    }

    // Update is called once per frame
    void Update()
    {

        //keep to the floor
        if (transform.position.y > vStartPos.y + vOOBUp)
        {
            transform.position = new Vector3(transform.position.x, vStartPos.y + vOOBUp, transform.position.z);
        }

        //respaun if Falls

        if (transform.position.y < vOOBLow)
        {
            
            StartCoroutine(pRespawn());
           


        }



    }

    public IEnumerator pRespawn()
    {
        //restore start position

        GameObject explodeS = Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(explodeS,1f);
        sPlayer sPlayer = GetComponent<sPlayer>();
        sPlayer.fSafe = true;
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(1);
        transform.position = vStartPos;
        transform.localScale = Vector3.one;

        sPlayer.fSafe = false;

        //kill velocity
        transform.rotation = Quaternion.identity;
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;


        }

    }

}
