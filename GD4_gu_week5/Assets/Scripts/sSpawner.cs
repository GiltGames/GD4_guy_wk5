using UnityEngine;

public class sSpawner : MonoBehaviour
{
    [SerializeField] int vBossLevelInt;
    [SerializeField] int vLevel;
    [SerializeField] float vSpawnCLow;
    [SerializeField] float vSpawnCHigh;
    [SerializeField] float vSpawnPLow;
    [SerializeField] float vSpawnPHigh;
    [SerializeField] Vector3 vCentrePos;
    [SerializeField] Vector3 vPlayerPos;
    [SerializeField] GameObject Player; 
    [SerializeField] GameObject Centre;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject Boss1;
    [SerializeField] int vNoofEnemy;
    [SerializeField] Transform enemyParent;
    public float vPowerupInterval;
    public float vPowerTimer;
    [SerializeField] GameObject[] Powerup;
    [SerializeField] Transform PowerupParent;
   

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vCentrePos = Centre.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)

        {
            vLevel = vLevel + 1;

            for (int i = 0; i < vLevel; i++)

            {

                if (vLevel % 4 == 3 && i == 0)
                {

                   Boss1 = Instantiate(Boss, pSpawnLocation(), Quaternion.identity, enemyParent);
                    Boss1.transform.Translate(0,1.3f,0);    
                }

                else
                {
                    Instantiate(Enemy, pSpawnLocation(), Quaternion.identity, enemyParent);

                }

            }

        }

      
        vPowerTimer = vPowerTimer -Time.deltaTime;

        if (vPowerTimer <0)
        {
            vPowerTimer = 100;
            int i = Random.Range(0, Powerup.Length);
            Instantiate(Powerup[i], pSpawnLocation(), Quaternion.identity, PowerupParent);
        }



        if (Input.GetKeyDown(KeyCode.B))
        {
           Boss1 = Instantiate(Boss, pSpawnLocation(), Quaternion.identity, enemyParent);
            Boss1.transform.Translate(0, 1.3f, 0);

        }


        if (Input.GetKeyDown(KeyCode.N))
        {
             Instantiate(Powerup[2], pSpawnLocation(), Quaternion.identity, PowerupParent);
           

        }


    }
    Vector3 pSpawnLocation()

    {

        float x = Random.Range(-vSpawnCHigh, vSpawnCHigh);
        float z = Random.Range(-vSpawnCHigh, vSpawnCHigh);
        if (Mathf.Abs(x) < vSpawnCLow)
        {
            if (x < 0)
            {

                x = x - vSpawnCLow;

            }
            else
            {
                x = x + vSpawnCLow;

            }

        }

        if (Mathf.Abs(z) < vSpawnCLow)
        {
            if (z < 0)
            {

                z = z - vSpawnCLow;

            }
            else
            {
                z = z + vSpawnCLow;

            }

        }

        if (Mathf.Abs(x - vPlayerPos.x) < vSpawnPLow)
        {
            x = x + vSpawnPLow;

        }

        if (Mathf.Abs(z - vPlayerPos.z) < vSpawnPLow)
        {
            z = z + vSpawnPLow;

        }

        return new Vector3(x, .1f, z);
    }

}
