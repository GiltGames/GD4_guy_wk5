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
    [SerializeField] int vNoofEnemy;
    [SerializeField] Transform enemyParent;
    [SerializeField] float vPowerupInterval;
    [SerializeField] float vPowerTimer;
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

                if (i % 3 == 2)
                {

                    Instantiate(Boss, pSpawnLocation(), Quaternion.identity, enemyParent);
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
            vPowerTimer = vPowerupInterval;
            int i = Random.Range(0, Powerup.Length);
            Instantiate(Powerup[i], pSpawnLocation(), Quaternion.identity, PowerupParent);
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

        return new Vector3(x, 0.1f, z);
    }

}
