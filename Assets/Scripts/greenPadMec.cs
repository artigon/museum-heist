using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenPadMec : MonoBehaviour
{
    private gameSystem gameSystem;
    // Start is called before the first frame update
    void Start()
    {
        gameSystem = GameObject.FindGameObjectWithTag("gameSystem").GetComponent<gameSystem>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            gameSystem.playerGotBack = true;
    }
}
