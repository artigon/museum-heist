using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSystem : MonoBehaviour
{
    public GameObject blackPanel;
    public GameObject[] windows;
    private Vector3 spawnPoint;
    public GameObject player;
    private AudioSource sound;
    private GameObject windowSelected;
    public bool gotTheArt = false;
    // Start is called before the first frame update
    void Start()
    {
        blackPanel = GameObject.FindGameObjectWithTag("blackPanel");
        player = GameObject.FindGameObjectWithTag("Player");
        sound = this.transform.GetChild(0).GetComponent<AudioSource>();
        windows = GameObject.FindGameObjectsWithTag("Window");
        int index = Random.Range(0, windows.Length);
        windowSelected = windows[index];
        StartCoroutine(spwnPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (gotTheArt)
            windowSelected.GetComponent<windowMec>().showGrennPad();
    }

    IEnumerator spwnPlayer()
    {
        spawnPoint = windowSelected.GetComponent<windowMec>().windowSelected();
        sound.Play();
        yield return new WaitForSeconds(2);
        blackPanel.SetActive(false);
        player.transform.position = spawnPoint;

    }
}
