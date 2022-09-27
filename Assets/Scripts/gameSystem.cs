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
    public GameObject theArt;
    private string endTimer;
    public scoresDataMec scorsMec;
    public artPickMec artPick;
    private bool gameEnded = false;
    public sceneMoverMec sceneMover;
    public GameObject goBackMsg;
    public GameObject gameOverkMsg;
    public bool gameOverBoo = false;
    // Start is called before the first frame update
    void Start()
    {
        gameOverBooRestart();
        player = GameObject.FindGameObjectWithTag("Player");
        sound = this.transform.GetChild(0).GetComponent<AudioSource>();
        scorsMec = GameObject.FindGameObjectWithTag("scorsMec").GetComponent<scoresDataMec>();
        artPick = GameObject.FindGameObjectWithTag("artStuff").GetComponent<artPickMec>();
        windows = GameObject.FindGameObjectsWithTag("Window");
        artPick.pickArt();
        pickWindow();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverBoo)
            StartCoroutine(gameOver());

        if (gotTheArt)
            windowSelected.GetComponent<windowMec>().showGrennPad();

        if (playerGotBack && !gameEnded)
            StartCoroutine(winGame());

        if(gameEnded && Input.GetKeyDown(KeyCode.Space))
        {
            sceneMover.SceneMovement();
        }


    }

    public void gameOverBooRestart()
    {
        gameOverBoo = false;
        gameEnded = false;
    }

    public void pickWindow()
    {
        int index = Random.Range(0, windows.Length);
        windowSelected = windows[index];
        if (!(Vector3.Distance(theArt.transform.position, windowSelected.transform.position)
            >= 100f))
            pickWindow();
        StartCoroutine(spwnPlayer());
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
        gameEnded = true;
        gameOn = false;
        endTimer = TimerText.text;
        blackPanel.SetBool("state", false);
        player.transform.position = playerWaitPoint;
        Time.timeScale = 0f;
        yield return new WaitForSeconds(1);
        scorsMec.addScore(endTimer);
        goBackMsg.SetActive(true);
    }

    IEnumerator gameOver()
    {
        blackPanel.SetBool("state", false);
        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;
        goBackMsg.SetActive(true);
        gameOverkMsg.SetActive(true);
        gameEnded = true;
        gameOn = false;
        endTimer = TimerText.text;
        player.transform.position = playerWaitPoint;
    }
}
