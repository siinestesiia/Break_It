using UnityEngine;


public class BallBounce : MonoBehaviour
{
    [Header ("- Ball Properties -")]
    [Tooltip ("The force the Ball receives in order to start moving.")]
    [SerializeField] float launchImpulse = 1.5f;
    // [Tooltip ("The Ball movement speed.")] 
    // [SerializeField] [Range(0.1f, 10f)] float ballSpeed = 1f;
    [Tooltip ("Time in seconds before reseting the Ball, after exiting the Play Zone.")]
    [SerializeField] float delayToReset = .8f;

    Vector3 ballVelocity;
    
    // Components
    Rigidbody ballRigidbody;
    TrailRenderer trailRenderer;

    // Other object's scripts
    PaddleController startGameScript;
    OutOfBoundsTrigger triggerScript;


    void Start()
    {
        GetObjectsAndComponents();
        InitializeBall();
        trailRenderer.enabled = false;

        SubscribingToEvents();
    }

    void Update()
    {
        ballVelocity = ballRigidbody.velocity;
        
    }

    void OnCollisionEnter(Collision Collider)
    {
        Vector3 ballDirection = ballVelocity;
        // Accessing the collider's normal.
        Vector3 collisionNormal = Collider.contacts[0].normal;
        Vector3 reflectedDirection = Vector3.Reflect(ballDirection, collisionNormal);

        ballRigidbody.velocity = reflectedDirection;
    }

    void SubscribingToEvents()
    {
        startGameScript.OnStartGame += LaunchBall;
        triggerScript.OnBallOutOfBounds += ResetBall;
    }

    void GetObjectsAndComponents()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
        startGameScript = FindObjectOfType<PaddleController>();
        triggerScript = FindObjectOfType<OutOfBoundsTrigger>();
    }

    public void LaunchBall()
    {
        ballRigidbody.isKinematic = true; // Disables Physics simulations.
        // Generate a random angle within a range.
        float randomAngle = Random.Range(-50f, 50f);
        Vector3 launchDirection = Quaternion.Euler(0, 0, randomAngle) * Vector3.down;
        ballRigidbody.isKinematic = false; // Enables Physics simulations.

        trailRenderer.enabled = true;
        
        ballRigidbody.AddForce(launchDirection * launchImpulse, ForceMode.Impulse);
    }

    void InitializeBall()
    {
        gameObject.transform.position = new Vector3(0, 9, 0);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void ResetBall()
    {
        Invoke("DeactivateBall", delayToReset);
    }

    void DeactivateBall()
    {
        trailRenderer.enabled = false;
        InitializeBall();
        ballRigidbody.velocity = new Vector3(0f, 0f, 0f);
    }
}


