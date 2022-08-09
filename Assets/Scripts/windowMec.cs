using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windowMec : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 windowSelected()
    {
        this.transform.GetChild(0).transform.gameObject.SetActive(false);
        this.transform.GetChild(1).transform.gameObject.SetActive(true);
        return this.transform.GetChild(2).transform.position;
    }

    public void showGrennPad()
    {
        this.transform.GetChild(3).transform.gameObject.SetActive(true);
    }
}
