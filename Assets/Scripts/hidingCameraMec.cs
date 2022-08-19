using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidingCameraMec : MonoBehaviour
{
    public GameObject player;
    public GameObject pCamera;
    public GameObject hCamera;
    private float angularSpeed = 150;
    private float rotationAboutY = 0;
    //private float rotationAboutX = 0;
    private bool hiding = false;



    // Start is called before the first frame update
    void Start()
    {
        pCamera = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hiding)
        {
            RaycastHit hit;
            if (Physics.Raycast(pCamera.transform.position, pCamera.transform.forward, out hit))
            {
                if (hit.transform.gameObject == this.gameObject && hit.distance < 20)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        player.SetActive(false);
                        hCamera.SetActive(true);
                        PlayerMotion.canMove = false;
                        hiding = true;
                    }
                }
            }
        }
        else if (hiding)
        {
            //aplies in camera
            rotationAboutY += Input.GetAxis("Mouse X") * angularSpeed * Time.deltaTime;
            hCamera.transform.localEulerAngles = new Vector3(0, rotationAboutY, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.SetActive(true);
                hCamera.SetActive(false);
                PlayerMotion.canMove = true;
                hiding = false;
            }
        }
    }
}
