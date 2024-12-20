using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class vEnemy : MonoBehaviour
{
    [SerializeField] GameObject Aura;
    [SerializeField] GameObject EnemyBody;
    Rigidbody rb;
    [SerializeField] float vAttackInterval;
    [SerializeField] float vAttackTimeRandomness;
    [SerializeField] float vHitDelay;
    [SerializeField] GameObject Player;
    [SerializeField] float vTimer;
    [SerializeField] float vAttackForce;
    [SerializeField] Vector3 vDirection;
    [SerializeField] Renderer Renderer;
    [SerializeField] float vTimeBlue;
    [SerializeField] float vTimeYellow;
    [SerializeField] float vTimeRed;
    [SerializeField] float vTimeFlash;
    [SerializeField] float vTimeFlashInt;
    [SerializeField] float vLowMass;
    [SerializeField] float vHighMass;
    [SerializeField] TextMeshProUGUI tScore;
    [SerializeField] GameObject explode;
    [SerializeField] sPlayer sPlayer;
    [SerializeField] AudioSource audioS;
    [SerializeField] AudioClip vHit;
    [SerializeField] AudioClip vLightning;
    [SerializeField] AudioClip vDamage;
    [SerializeField] AudioClip vExplode;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        Renderer = transform.GetChild(0).GetComponent<Renderer>();
        Player = GameObject.Find("Player");
        tScore = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        sPlayer = Player.GetComponent<sPlayer>();
        Renderer.material.color = Color.yellow;
        audioS= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sPlayer.fGameStart)
        {

            vTimer = vTimer - Time.deltaTime;
        }
        // colour change

        if (vTimer > vTimeBlue)
        {
            Renderer.material.color = Color.blue;
            Aura.SetActive(false);
        }
        else
        {
           rb.mass = vHighMass;
            
            if (vTimer > vTimeYellow)
            {
                Renderer.material.color = Color.yellow;
                Aura.SetActive(false);
            }
            else
            {
                if (vTimer > vTimeRed)
                {
                    Renderer.material.color = Color.red;

                    if (vTimer < vTimeFlash)
                    {
                        

                        if(Mathf.FloorToInt(vTimer/vTimeFlashInt)%2 == 0)

                        {
                            Aura.SetActive(false);

                        }

                        else
                        {

                            Aura.SetActive(true);
                        }


                    }



                }

            }
        }


        // attack
        if(vTimer <0)

        {
            vTimer = vAttackInterval +(Random.Range(-vAttackTimeRandomness,0));
            vDirection = Player.transform.position - transform.position;


            rb.AddForce(vDirection.normalized * vAttackForce, ForceMode.Impulse);


        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enemy Hit: "+ collision.gameObject.tag);
      
        audioS.Play();  
        
        if (collision.gameObject.tag == "Shot")
        {

            Destroy(collision.gameObject);
            vTimer = vHitDelay;
            rb.mass = vLowMass;
            audioS.clip = vDamage;
            audioS.Play();  

        }

        if (collision.gameObject.tag == "Trap" && vTimer >vTimeBlue)
        {
            
           
            sPlayer.vScore = sPlayer.vScore + 1;
            tScore.text = "Score: "+sPlayer.vScore;

            GameObject explodeS = Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(explodeS, 1f);
            audioS.clip = vExplode;
            audioS.Play();


            Destroy(gameObject);

        }


    }

    private void OnCollisionStay(Collision collision)
    {
       

        if (collision.gameObject.tag == "Trap" && vTimer > vTimeBlue)
        {


            sPlayer.vScore = sPlayer.vScore + 1;
            tScore.text = "Score: " + sPlayer.vScore;

            GameObject explodeS = Instantiate(explode, transform.position, Quaternion.identity);
            Destroy(explodeS, 1f);

            Destroy(gameObject);

        }


    }


}
