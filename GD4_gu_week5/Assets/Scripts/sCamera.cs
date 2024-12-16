using UnityEngine;

public class sCamera : MonoBehaviour
{
    [SerializeField] float vMouseSpeed=200;
    [SerializeField] float vMouseCamSpeed = 20;
    [SerializeField] bool fXMouseInvert;
    [SerializeField] bool fYMouseInvert;
    [SerializeField] Transform gPlayer;
    [SerializeField] Transform gCameraTransform;
    [SerializeField] float vCamLowLim = 30;
    [SerializeField] float vCamUpLim = 50;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }

    // Update is called once per frame
    void Update()
    {
        float vMouseUpTmp = Input.GetAxis("MouseX");
        float vKeySideTmp = Input.GetAxis("Horizontal");

        vMouseUpTmp = vMouseUpTmp +vKeySideTmp;

        
        if (fXMouseInvert)
        {
            vMouseUpTmp = vMouseUpTmp * -1;

        }

        transform.Rotate(Vector3.up * vMouseSpeed *Time.deltaTime* vMouseUpTmp);

        transform.position = gPlayer.position;




        float vMouseSideTmp = Input.GetAxis("MouseY");

        if (fYMouseInvert) 
        {
            vMouseSideTmp *= -1;   
        }

        gCameraTransform.Translate(new Vector3(0,vMouseSideTmp*vMouseCamSpeed*Time.deltaTime,0));

        if(gCameraTransform.position.y > vCamUpLim)
        {
            gCameraTransform.position = new Vector3(gCameraTransform.position.x,vCamUpLim,gCameraTransform.position.z);
        }

        if (gCameraTransform.position.y < vCamLowLim)
        {
            gCameraTransform.position = new Vector3(gCameraTransform.position.x, vCamLowLim, gCameraTransform.position.z);
        }

        /*
         // redunnt code re angle change 
        gCameraTransform.Rotate(vMouseSideTmp * Time.deltaTime * vMouseCamSpeed * Vector3.forward);
        if (gCameraTransform.eulerAngles.x> vCamUpLim)
        {
            gCameraTransform.rotation = Quaternion.Euler(vCamUpLim, gCameraTransform.rotation.y, gCameraTransform.rotation.z);

        }

        if (gCameraTransform.eulerAngles.x < vCamLowLim)
        {
            gCameraTransform.rotation = Quaternion.Euler(vCamLowLim, gCameraTransform.rotation.y, gCameraTransform.rotation.z);

        }
             */
        gCameraTransform.LookAt(transform.position);
   
    }
}
