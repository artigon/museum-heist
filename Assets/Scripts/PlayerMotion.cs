using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;// for text

public class PlayerMotion : MonoBehaviour
{
    private CharacterController cController;
    private float speed = 15;
    private float angularSpeed = 150;
    private float rotationAboutY = 0;
    private float rotationAboutX = 0;
    public GameObject pCamera;//must be connected in UNITY to camera
    public static bool canMove = true;
    public static bool havePainting = false;
    public GameObject textTest;
    public static bool gotCaught = false;
    //private AudioSource walkSound;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float dx, dz;
            //aplies on player(and camera)
            rotationAboutY += Input.GetAxis("Mouse X") * angularSpeed * Time.deltaTime;
            transform.localEulerAngles = new Vector3(0, rotationAboutY, 0);

            //aplies in camera
            rotationAboutX -= Input.GetAxis("Mouse Y") * angularSpeed * Time.deltaTime;
            if (!(rotationAboutX > 90.0f || rotationAboutX < -90.0f))
                pCamera.transform.localEulerAngles = new Vector3(rotationAboutX, 0, 0);

            //Input.GetAxis("Horizontal") can be: -1, 0, 1
            dx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            dz = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            //simple motion
            //this.transform.Translate(new Vector3(0, 0, -0.3f));
            Vector3 motion = new Vector3(dx, -1, dz);//we thoght that dx & dz as local coordinates
            motion = transform.TransformDirection(motion);//changes motion from local to global coordinates
                                                          //Move is based on GLOBAL coordinates
            cController.Move(motion);
            //play walking sound
            //if (Mathf.Abs(motion.z) > 0.01f || Mathf.Abs(motion.x) > 0.01f)//only when player moves
        }

        if(gotCaught)
            textTest.SetActive(true);
    }
}
