using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artPickMec : MonoBehaviour
{
    public GameObject[] arts;
    // Start is called before the first frame update
    void Start()
    {
        arts = GameObject.FindGameObjectsWithTag("art");
        arts[Random.Range(0, arts.Length)].GetComponent<paintingMec>().light.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
