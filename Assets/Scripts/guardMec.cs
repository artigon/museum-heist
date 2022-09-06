using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class guardMec : MonoBehaviour
{
    public gameSystem gameSystem;

    private AudioSource sound;
    public GameObject text;

    private Animator animator;
    private NavMeshAgent agent;
    public Vector3 target;
    private Vector3 lastTarget;
    public GameObject player;
    private bool atTarget;

    //for sight
    public float sightRange;
    public float hearingRange;//for now use this for testing
    public float checkRangeForBust, bustRange;
    public bool playerInRange, playerInBustRange;
    public LayerMask whatIsPlayer;


    //patrol
    public GameObject[] pointsObj;
    private Vector3[] points = new Vector3[11];
    private float y = 3f;
    private int lastPoint = -1;
    private int newPoint = -2;

    // Start is called before the first frame update
    void Start()
    {
        gameSystem = GameObject.FindGameObjectWithTag("gameSystem").GetComponent<gameSystem>();

        player = GameObject.FindGameObjectWithTag("Player");

        sound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        points[0] = new Vector3(-43.1f, y, -25.4f);
        points[1] = new Vector3(27.5f, y, -27.2f);
        points[2] = new Vector3(-78.2f, y, -111.6f);
        points[3] = new Vector3(75.2f, y, -109.8f);
        points[4] = new Vector3(77.8f, y, -271.4f);
        points[5] = new Vector3(-4.9f, y, -212.2f);
        points[6] = new Vector3(-77.4f, y, -268.3f);
        points[7] = new Vector3(-7.6f, y, -268.2f);
        points[8] = new Vector3(-0.8f, y, -111.8f);
        points[9] = new Vector3(41f, y, -160.83f);
        points[10] = new Vector3(-40.78f, y, -160.83f);

        newPoint = Random.Range(0, 10);
        if (newPoint != lastPoint)
            target = points[newPoint];
        lastPoint = newPoint;

    }
    
    // Update is called once per frame
    void Update()
    {
        

        if (this.transform.position.x - target.x <= 0.05f &&
                this.transform.position.z - target.z <= 0.05 &&
                target != player.transform.position)
        {
            newPoint = Random.Range(0, 10);
            if (newPoint != lastPoint)
                target = points[newPoint];
            lastPoint = newPoint;
        }



        if (agent.enabled)
            agent.SetDestination(target);

        playerInRange = Physics.CheckSphere(this.transform.position, hearingRange, whatIsPlayer);
        if(playerInRange)
        {
            sound.Play();
            bustRange = Vector3.Distance(player.transform.position, this.transform.position);
            if (bustRange <= checkRangeForBust)
                playerInBustRange = true;
            else
                playerInBustRange = false;
        }

        if (playerInRange && !playerInBustRange)
            StartCoroutine(isPlayerStillThere());
        else if (playerInBustRange && playerInRange)
            playerBusted();

    }

    IEnumerator isPlayerStillThere()
    {
        lastTarget = target;
        target = player.transform.position;
        agent.speed = 10;
        yield return new WaitForSeconds(20);
        if (!player.activeSelf)
        {
            target = lastTarget;
            agent.speed = 5;
        }
    }

    public void playerBusted()
    {
        gameSystem.gameOverBoo = true;
    }
}
