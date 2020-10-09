using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepController : MonoBehaviour
{
    //Animator
    private Animator anim;
    Vector3 lastPosition = Vector3.zero;
    float speed = 0f;

    //AI variables
    NavMeshAgent nma;
    NavMeshPath path;
    Vector3 target;
    public float timeForNewPath;
    bool validPath;
    bool inCoRoutine = false;
    
    //Chase variables
    float chaseSpeed = 10.0f;
    float runThisLong = 4.0f;
    bool beingChased = false;
    Vector3 playerPos;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }
    private void Update()
    {
        UpdateAnimation();
        if (!inCoRoutine)
        {
            StartCoroutine(WalkRandom(timeForNewPath));
        }
        if(beingChased)
        {
            CalculateRun(playerPos);
        }
    }
    
    //Walk Randomly ---------------------------------------------------------
    Vector3 GetNewRandomPosition()
    {
        //Time between generating new path
        timeForNewPath = Random.Range(2, 7);
        //Get random location between -10 and 10 in the x and z directions
        float x = Random.Range(-100, 100);
        float z = Random.Range(-100, 100);
        //Return position of new point to path towards
        Vector3 pos = new Vector3(x, 0.0f, z);
        return pos;
    }

    IEnumerator WalkRandom(float time)
    {
        inCoRoutine = true;
        GetNewPath();
        validPath = nma.CalculatePath(target, path);
        yield return new WaitForSeconds(time);
        while(!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = nma.CalculatePath(target, path);
        }
        inCoRoutine = false;
    }
    void GetNewPath()
    {
        target = GetNewRandomPosition();
        nma.SetDestination(target);
    }
    
    //Run away from player ---------------------------------------------------
    private void OnTriggerStay(Collider other)
    {
        //Only run away from player
        if (other.gameObject.tag == "Player")
        {
            //Makes conditional true in Update function for sheep to run
            beingChased = true;
            playerPos = other.transform.position;
            StartCoroutine(RunAway(runThisLong));
        }
    }

    IEnumerator RunAway(float time)
    {
        inCoRoutine = true;
        //Wait time seconds before the sheep stops being chased
        yield return new WaitForSeconds(time);
        beingChased = false;
        inCoRoutine = false;
    }

    void CalculateRun(Vector3 playerPosition)
    {
        //Get distance of sheep from player
        Vector3 distance = transform.position - playerPosition;
        transform.forward = distance;
        Vector3 moveForward = new Vector3(0.0f, 0.0f, chaseSpeed * Time.deltaTime);
        transform.Translate(moveForward);
    }

    void UpdateAnimation()
    {
        //Get speed of sheep
        float movementPerFrame = Vector3.Distance(lastPosition, transform.position);
        speed = movementPerFrame / Time.deltaTime;
        lastPosition = transform.position;
        //Set speed to animation variable
        anim.SetFloat("speed", speed);
    }
}