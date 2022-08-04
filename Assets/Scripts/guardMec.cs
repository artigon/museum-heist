using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class guardMec : MonoBehaviour
{
    private AudioSource sound;

    private Animator animator;
    private NavMeshAgent agent;
    public Vector3 target;
    private Vector3 lastTarget;
    public GameObject player;
    private bool atTarget;

    //for sight
    public GameObject originSight;
    private Vector3 origin;
    public float sphereRadius;
    private Vector3 direction;
    public float maxDistance;
    public LayerMask layerMask;
    public GameObject currentHitObject;
    private float currentHitDisance;

    //patrol
    private Vector3[] points = new Vector3[9];
    private float y = 3f;
    private int lastPoint = -1;
    private int newPoint = -2;

    // Start is called before the first frame update
    void Start()
    {
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

        newPoint = Random.Range(0, 8);
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
            newPoint = Random.Range(0, 8);
            if (newPoint != lastPoint)
                target = points[newPoint];
            lastPoint = newPoint;
        }

        if (agent.enabled)
            agent.SetDestination(target);

        origin = originSight.transform.position;
        direction = transform.forward;
        RaycastHit hit;
        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDisance = hit.distance;
            if (currentHitObject == player)
            {
                sound.Play();
                atTarget = true;
                target = player.transform.position;
                StartCoroutine(isPlayerStillThere());
            }
        }
        else
        {
            currentHitDisance = maxDistance;
            currentHitObject = null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            PlayerMotion.gotCaught = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDisance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDisance, sphereRadius);
    }

    IEnumerator isPlayerStillThere()
    {
        
        lastTarget = target;
        target = player.transform.position;
        yield return new WaitForSeconds(20);
        if (!atTarget || !player.activeSelf)
        {
            target = lastTarget;
            atTarget = false;
        }
    }
}
