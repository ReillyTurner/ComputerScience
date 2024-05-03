using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovment : MonoBehaviour
{
   // this will check what waypoint the player is currently at, it will then check what waypoints are next to it. it then will move the player to the next waypoint


    // SPEEDINCREASE = 10; 

    public const float speedIncrease = 10;


    
    // the waypoint the player is currently at
    public WayPoint currentWayPoint;
    // the waypoint the player is moving to
    public WayPoint nextWayPoint;
    public WayPoint pastWayPoint;
    // the speed the player moves at
    public float speed = 3.0f;

    float initialSpeed = 3.0f;

     float timer = 0f;

    public float gameTimer = 0f;

    public Text resultText;

    public GameObject restartButton;

    public float respawnTime = 5f;
    public float score = 0;
    public List<GameObject> hiddenObjects = new List<GameObject>();

    public Collider2D playerCollider;
    public GameObject spawnPoints;

    // Start is called before the first frame update
    void Start()

    {

        restartButton.SetActive(false);

        // get the WayPointManager script
        WayPointManager wayPointManager = FindObjectOfType<WayPointManager>();
        // set the current waypoint to the closest waypoint to the player
        currentWayPoint = wayPointManager.GetClosestWayPoint(transform.position);
        // set the next waypoint to the first waypoint in the list of waypoints that are next to the current waypoint
        nextWayPoint = currentWayPoint.GetNextWayPoints()[0];


            // Get all the child game objects
            Transform[] childTransforms = spawnPoints.GetComponentsInChildren<Transform>();

            // Create an array 
            GameObject[] childObjects = new GameObject[childTransforms.Length];

            // (FOR) LOOPS BEGIN HERE
            for (int i = 0; i < childTransforms.Length; i++)
            {
                childObjects[i] = childTransforms[i].gameObject;
            }
            int numToHide = Random.Range(6, 8); 
            for (int i = 0; i < numToHide; i++)
            {
                int randomIndex = Random.Range(0, childObjects.Length);
                hiddenObjects.Add(childObjects[randomIndex]);
                childObjects[randomIndex].SetActive(false);
            } 
            
    }

public void RespawnObjectsAfterDelay()
{   
    Debug.Log("Respawning objects...");

    // Determine how many hidden objects to respawn
    int numToRespawn = Random.Range(1, hiddenObjects.Count + 1);

    // Randomly select objects to respawn
    List<int> respawnIndices = new List<int>();
    for (int i = 0; i < numToRespawn; i++)
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, hiddenObjects.Count);
        } while (respawnIndices.Contains(randomIndex));

        respawnIndices.Add(randomIndex);

        GameObject obj = hiddenObjects[randomIndex];
        obj.SetActive(true);

    }

    // Remove respawned objects from the list
    for (int i = respawnIndices.Count - 1; i >= 0; i--)
    {
        hiddenObjects.RemoveAt(respawnIndices[i]);
    }
}

    // Update is called once per frame
        void Update()
    {
        timer += Time.deltaTime;
        if (timer > respawnTime)
        {
            timer = 0;
            RespawnObjectsAfterDelay();
        }

        gameTimer += Time.deltaTime;
        resultText.text = Mathf.Round(gameTimer).ToString();
        if (score >= 1)
        {   
            Debug.Log("You lose!");
            speed = 0;
            initialSpeed = 0;
            resultText.text = "You lose. Your final time was: " + Mathf.Round(gameTimer).ToString() + " seconds.";
            restartButton.SetActive(true);
            this.enabled = false;
        }

        // speed = initialSpeed + (gameTimer / 10); // 10 is a hard-coded value, you can adjust it to your liking : set it as a constant
        speed = initialSpeed + (gameTimer / speedIncrease);

        // move the player towards the next waypoint
        transform.position = Vector3.MoveTowards(transform.position, nextWayPoint.transform.position, speed * Time.deltaTime);

        // if the player is at the next waypoint
        if (transform.position == nextWayPoint.transform.position)
        {
            pastWayPoint = currentWayPoint;
            
            // set the current waypoint to the next waypoint
            currentWayPoint = nextWayPoint;
            // get all the waypoints that are next to the current waypoint
            List<WayPoint> nextWayPoints = currentWayPoint.GetNextWayPoints();
            // remove the past waypoint from the list
            nextWayPoints.Remove(pastWayPoint);

            // set the next waypoint to a random waypoint from the list of waypoints that are next to the current waypoint
            nextWayPoint = nextWayPoints[Random.Range(0, nextWayPoints.Count)];

        }
    }
private bool playerCollect;
    private bool collected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerCollect = true;
        Debug.Log("Player has collided with " + collision.name);
        StartCoroutine(ProcessInput(collision.gameObject));
    }

private void OnTriggerExit2D(Collider2D collision)
    {
        playerCollect = false;
        if (!collected)
        {
            score++;
        }
    }
    private System.Collections.IEnumerator ProcessInput(GameObject collidedObject)
    {
        while (playerCollect)
        {
            yield return null; 

            if (Input.GetKeyDown(KeyCode.Space))
            {
                collected = true;
                collidedObject.SetActive(false);
                hiddenObjects.Add(collidedObject);
                yield break; 
            } else {
                collected = false;  
            }
            
        }
        }
    }
