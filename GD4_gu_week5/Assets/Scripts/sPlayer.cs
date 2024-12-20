using TMPro;
using Unity.Hierarchy;

using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class sPlayer : MonoBehaviour
{
    [SerializeField] float vPlayerSpeed;
    [SerializeField] Transform gFocalPoint;

    [SerializeField] float vMassUp = 25;
    [SerializeField] float vMass = 5;
    [SerializeField] float vMassUpPlayerSpeed = 50;
    [SerializeField] float vMassNormalPlayerSpeed = 10;
    [SerializeField] float vMassUpTimer;
    [SerializeField] float vMassUpInterval = 7;
    [SerializeField] float vShotUpMultiple = 1;
    [SerializeField] float vMassSize;
    [SerializeField] Vector3 vOriginalSize;

    //ShotUp variables
    [SerializeField] float vShotUpMult = 10;
    [SerializeField] bool fShotUp;
    [SerializeField] float vShotUpTimer;
    [SerializeField] float vShotUpInterval;
    [SerializeField] GameObject Aura2;

    //RepelUp variables
    [SerializeField] GameObject Aura3;
    [SerializeField] float vRepelUpTimer;
    [SerializeField] float vRepelUpInterval;
    [SerializeField] sRepel sRepel;
    [SerializeField] float vRepelGravity = -50f;


    Rigidbody rb;
    [SerializeField] int vLives;
    public int vScore;
    [SerializeField] sMoveandRespawn sMoveandRespawn;
    [SerializeField] TextMeshProUGUI tScore;
    [SerializeField] TextMeshProUGUI tLives;
    [SerializeField] TextMeshProUGUI tGameOver;
    [SerializeField] TextMeshProUGUI tInstructions;
    [SerializeField] TextMeshProUGUI tTitle;
    [SerializeField] float vTimeEnd;
    public bool fGameOver;
    public bool fGameStart;
    [SerializeField] GameObject vShotPrefab;
    [SerializeField] GameObject vShot;
    [SerializeField] float vShotForce;
    public int vShotLimit;
    [SerializeField] int vShotsLeft;
    public bool fSafe = false;
    [SerializeField] float vSafeTimer;
    [SerializeField] float vSafeInterval;
    [SerializeField] GameObject Aura;
    [SerializeField] string[] vPowerupTagText;
    [SerializeField] Transform ShotParent;


    [SerializeField] Transform gCamera;

    [SerializeField] sSpawner sSpawner;

    [SerializeField] float vGameOverTimer;
    [SerializeField] float vGameOverInterval=3;
    [SerializeField] Vector3 vStartPosPlayer;
    [SerializeField] AudioSource audioS;
    [SerializeField] AudioClip vLightning;
    [SerializeField] AudioClip vHit;
    [SerializeField] AudioClip vExplode;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        sMoveandRespawn = GetComponent<sMoveandRespawn>();

        transform.rotation = Quaternion.identity;
        vOriginalSize = transform.localScale;
        sRepel = gameObject.GetComponent<sRepel>();
        vStartPosPlayer = transform.position;
        audioS = GetComponent<AudioSource>(); 
     


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
                tTitle.enabled = false;
                tInstructions.enabled = false;  

                gCamera.rotation = Quaternion.identity;

            }


           


        }


        if (!fGameOver && fGameStart)
        {
            pInputForce();

            pFire();

            pTimer();



        }

        else
        {
            vGameOverTimer = vGameOverTimer - Time.deltaTime;
            if (vGameOverTimer < 0 && fGameOver)
            {
                Time.timeScale = 1;

                vScore = 0;
                vLives = 3;
                fGameOver = false;
                fGameStart = false;
                SceneManager.LoadScene(0);
            }

        }
    }



    void pInputForce()
    {

        float vUp = Input.GetAxis("Vertical");
        float vHor = Input.GetAxis("Horizontal");

        Vector3 vInput = ((gFocalPoint.forward * vUp)+ (gFocalPoint.right * vHor)).normalized;


        //rb.AddForce(gFocalPoint.forward * vUp* vPlayerSpeed * Time.deltaTime,ForceMode.Impulse);
        rb.AddForce(vInput * vPlayerSpeed * Time.deltaTime, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision collision)
    {
        audioS.clip = vHit;
        audioS.Play();

        if (collision.gameObject.tag == "Trap" && !fSafe)

        {
            vLives = vLives - 1;
            Aura3.SetActive(false);
            Aura2.SetActive(false);

            audioS.clip = vExplode;
            audioS.Play();



            if (vLives > 0)
            {
                tLives.text = "Lives: " + vLives;
                StartCoroutine(sMoveandRespawn.pRespawn());

            }
            else
            {
                //gameover
                tGameOver.text = "Game Over";
                fGameOver = true;
                vGameOverTimer = vGameOverInterval;
                Time.timeScale = 0.1f;
                StartCoroutine(sMoveandRespawn.pRespawn());
               
                

             



            }
        }

        
        

           

    }

    private void OnTriggerEnter(Collider other)
    {

        sSpawner.vPowerTimer = sSpawner.vPowerupInterval;
        
        if (other.gameObject.tag == vPowerupTagText[0])
        {
            fSafe = true;
            vSafeTimer = vSafeInterval;
            Aura.SetActive(true);
            Destroy(other.gameObject);

        }

        if (other.gameObject.tag == vPowerupTagText[1])
        {
            vPlayerSpeed = vMassUpPlayerSpeed;
            rb.mass = rb.mass * vMassUp;
            vMassUpTimer = vMassUpInterval;
            transform.localScale = transform.localScale * vMassSize;

            Destroy(other.gameObject);

        }

        if (other.gameObject.tag == vPowerupTagText[2])
        {
            fShotUp = true;
            vShotUpTimer = vShotUpInterval;
           Aura2.SetActive(true);

            Destroy(other.gameObject);

        }

        if (other.gameObject.tag == vPowerupTagText[3])
        {
            vRepelUpTimer = vRepelUpInterval;
            sRepel.vGravity = vRepelGravity;
            Aura3.SetActive(true);

            Destroy(other.gameObject);

        }

      





    }


    private void pFire()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
              {

            audioS.clip = vLightning;
            audioS.Play();


            if(!fShotUp)
                {
                float vShotUpMultiplierTmp;
                if (fShotUp)
                {
                    vShotUpMultiplierTmp = vShotUpMult;
                }
                else
                {
                    vShotUpMultiplierTmp = 1.0f;
                }


                vShot = Instantiate(vShotPrefab, transform.position + gFocalPoint.forward, Quaternion.identity, ShotParent);
                vShot.GetComponent<Rigidbody>().AddForce(gFocalPoint.forward * vShotForce, ForceMode.Impulse);
                vShot.GetComponent<Rigidbody>().mass = vShotUpMultiplierTmp * vShot.GetComponent<Rigidbody>().mass;
                Destroy(vShot, 2f);

            }

            else
            {
               vEnemy[] vEnemy = FindObjectsByType<vEnemy>(FindObjectsSortMode.None);
                int vEnemyCount = vEnemy.Length;


                
                for (int i = 0; i < vEnemyCount; i++)
                {
                    float vShotUpMultiplierTmp;
                   
                    
                        vShotUpMultiplierTmp = vShotUpMult;

                    Vector3 vDirect = (vEnemy[i].transform.position - transform.position).normalized;


                    vShot = Instantiate(vShotPrefab, transform.position + vDirect, Quaternion.identity, ShotParent);
                    vShot.GetComponent<Rigidbody>().AddForce(vDirect * vShotForce, ForceMode.Impulse);
                    vShot.GetComponent<Rigidbody>().mass = vShotUpMultiplierTmp * vShot.GetComponent<Rigidbody>().mass;
                    Destroy(vShot, 2f);



                }



            }

        }


    }

    private void pTimer()
    {

        // Power up Timer goes down
        vSafeTimer = vSafeTimer - Time.deltaTime;
        if (vSafeTimer < 0)
        {
            fSafe = false;
            Aura.SetActive(false);

        }

        //Massup Timer

        vMassUpTimer = vMassUpTimer - Time.deltaTime;
        if (vMassUpTimer < 0)
        {
            vPlayerSpeed = vMassNormalPlayerSpeed;
            rb.mass = vMass;
            transform.localScale =vOriginalSize;

        }

        //ShotUp Timer

        vShotUpTimer = vShotUpTimer - Time.deltaTime;
        if (vShotUpTimer < 0)
        {
            fShotUp = false;
            Aura2.SetActive(false);
        }


        //Repel Timer

        vRepelUpTimer = vRepelUpTimer - Time.deltaTime;
        if (vRepelUpTimer < 0)
        {
            sRepel.vGravity = 0;
            Aura3.SetActive(false);
        }

       

    }


}
