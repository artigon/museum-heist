using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class artPickMec : MonoBehaviour
{
    public GameObject[] arts;
    public gameSystem gameSystem;
    // Start is called before the first frame update
    void Start()
    {
        gameSystem = GameObject.FindGameObjectWithTag("gameSystem").GetComponent<gameSystem>();
        arts = GameObject.FindGameObjectsWithTag("art");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickArt()
    {
        
        int index = Random.Range(0, arts.Length);
        arts[index].GetComponent<paintingMec>().light.SetActive(true);
        gameSystem.theArt = arts[index];
    }
}
