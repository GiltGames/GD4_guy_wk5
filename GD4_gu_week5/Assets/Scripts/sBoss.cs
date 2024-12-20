using TMPro;
using UnityEngine;

public class sBoss : MonoBehaviour
{
    [SerializeField] float vFireInterval;
    [SerializeField] float vFireRapidInterval;
    [SerializeField] int vFireNo;
    [SerializeField] float[] vFireTime;
    [SerializeField] bool[] vFired;
    [SerializeField] float vFireTimer;
    [SerializeField] GameObject gFirePreFab;
    [SerializeField] GameObject gFire;
    [SerializeField] GameObject gPlayer;
    [SerializeField] float vFireForce;
    [SerializeField] Vector3 vFireDir;
    [SerializeField] Vector3 vDirDist;
    [SerializeField] float vOriginOffset=2;
    [SerializeField] GameObject gShotParent;
    [SerializeField] AudioSource audioS;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gPlayer = GameObject.Find("Player");
       gShotParent = GameObject.Find("ShotParent");
        audioS = GetComponent<AudioSource>();
        pReset();
    }

    // Update is called once per frame
    void Update()
    {
        vFireTimer = vFireTimer - Time.deltaTime;
        if (vFireTimer < 0 )

        {
            vFireTimer = vFireInterval;
            pReset();


        }
        
        else
        {
            for (int i = 0;i <vFireNo;i++)
            {
                if (vFireTimer < vFireTime[i] && vFired[i] ==false)

                {
                    vFired[i]=true;
                    pFire();


                }

            }


        }

        


       

    }

    private void pFire()
    {
        
        vFireDir = (gPlayer.transform.position - transform.position).normalized;

        
        gFire= Instantiate(gFirePreFab,transform.position+(vFireDir*vOriginOffset),Quaternion.identity,gShotParent.transform);
        gFire.transform.position = new Vector3(gFire.transform.position.x, .2f,gFire.transform.position.z);
        Destroy(gFire,.8f);
        Rigidbody rb = gFire.GetComponent<Rigidbody>();
        rb.AddForce(vFireDir*vFireForce,ForceMode.Impulse);
        audioS.Play();


    }

    private void pReset()
    {

        for (int i = 0; i < vFireNo; i++)

        {
            vFired[i] = false;
            vFireTime[i] = (i+1) * vFireRapidInterval;


        }


    }

}
