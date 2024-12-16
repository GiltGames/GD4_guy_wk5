using UnityEngine;

public class vEnemy : MonoBehaviour
{
    [SerializeField] GameObject Aura;
    [SerializeField] GameObject EnemyBody;
    Rigidbody rb;
    [SerializeField] float vAttackInterval;
    [SerializeField] GameObject Player;
    [SerializeField] float vTimer;
    [SerializeField] float vAttackForce;
    [SerializeField] Vector3 vDirection;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vTimer = vTimer - Time.deltaTime;

        if(vTimer <0)

        {
            vTimer = vAttackInterval;
            vDirection = Player.transform.position - transform.position;


            rb.AddForce(vDirection * vAttackForce, ForceMode.Impulse);


        }


    }
}
