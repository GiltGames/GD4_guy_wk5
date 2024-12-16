using TMPro;
using UnityEngine;

public class sPlayer : MonoBehaviour
{
    [SerializeField] float vPlayerSpeed=10;
    [SerializeField] Transform gFocalPoint;
    Rigidbody rb;
    [SerializeField] int vLives;
    [SerializeField] int vScore;
    [SerializeField] sMoveandRespawn sMoveandRespawn;
    [SerializeField] TextMeshProUGUI tScore;
    [SerializeField] TextMeshProUGUI tLives;
    [SerializeField] TextMeshProUGUI tGameOver;
    [SerializeField] float vTimeEnd;
    public bool fGameOver;
    public bool fGameStart;
    [SerializeField] GameObject vShotPrefab;
    [SerializeField] GameObject vShot;
    [SerializeField] float vShotForce;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        sMoveandRespawn = GetComponent<sMoveandRespawn>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!fGameStart)
        {
            tGameOver.text = "Press any Key";

            if (Input.anyKeyDown)
            {

                fGameStart = true;
                tGameOver.text = "";

            }

        }

        
        if (!fGameOver && fGameStart)
        {
            pInputForce();

            pFire();



        }




    }



    void pInputForce()
    {

        float vUp = Input.GetAxis("Vertical");
        rb.AddForce(gFocalPoint.forward * vUp* vPlayerSpeed * Time.deltaTime,ForceMode.Impulse);
            
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trap")

        {
            vLives = vLives - 1;

            if (vLives > 0)
            {
                tLives.text = "Lives: " + vLives;
                sMoveandRespawn.pRespawn();

            }
            else
            {
                //gameover
                tGameOver.text = "Game Over";
                fGameOver = true;
                Time.timeScale = 0.1f;



            }
        }


    }


    private void pFire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
              {

            vShot = Instantiate(vShotPrefab, transform.position + gFocalPoint.forward, Quaternion.identity);
            vShot.GetComponent<Rigidbody>().AddForce(gFocalPoint.forward * vShotForce, ForceMode.Impulse);
            Destroy(vShot, 5f);
        }


    }


}
