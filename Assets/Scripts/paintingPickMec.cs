using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintingPickMec : MonoBehaviour
{
    public GameObject[] paintings;
    // Start is called before the first frame update
    void Start()
    {
       paintings[Random.Range(0, paintings.Length)].GetComponent<paintingMec>().light.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
