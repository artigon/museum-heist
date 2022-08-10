using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameSystem : MonoBehaviour
{
    public Animator blackPanel;
    public GameObject[] windows;
    private Vector3 spawnPoint;
    private Vector3 playerWaitPoint = new Vector3(-39.69f, 2.52f, 4.67f);
    public GameObject player;
    private AudioSource sound;
    private GameObject windowSelected;
    public bool gotTheArt = false;
    public bool playerGotBack = false;
    public bool gameOn = true;
    private float Timer;
    public Text TimerText;
    // Start is called before the first frame update
    void Start()
    {
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

        if (playerGotBack)
            StartCoroutine(winGame());
        
    }

    IEnumerator timer()
    {
        if (gameOn)
        {
            Timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(Timer / 60F);
            int seconds = Mathf.FloorToInt(Timer % 60F);
            yield return new WaitForSeconds(0.01f);
            TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            StartCoroutine(timer());

        }
    }

    IEnumerator spwnPlayer()
    {
        spawnPoint = windowSelected.GetComponent<windowMec>().windowSelected();
        sound.Play();
        yield return new WaitForSeconds(2f);
        blackPanel.SetBool("state", true);
        StartCoroutine(timer());
        player.transform.position = spawnPoint;

    }

    IEnumerator winGame()
    {
        gameOn = false;
        print("win");
        player.transform.position = playerWaitPoint;
        yield return new WaitForSeconds(1);
    }

    
}
