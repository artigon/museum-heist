using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintingMec : MonoBehaviour
{
    public GameObject light;
    public GameObject pCamera;
    private bool ceck = false;
    public gameSystem gameSystem;
    // Start is called before the first frame update
    void Start()
    {
        gameSystem = GameObject.FindGameObjectWithTag("gameSystem").GetComponent<gameSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (light.activeSelf)
        {
            RaycastHit hit;
            if (Physics.Raycast(pCamera.transform.position, pCamera.transform.forward, out hit))
            {
                if (hit.transform.gameObject == this.gameObject && hit.distance < 20)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        PlayerMotion.havePainting = true;
                        this.gameObject.SetActive(false);
                        gameSystem.gotTheArt = true;
                    }
                }
            }
        }
    }
}
