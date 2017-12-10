using UnityEngine;

public class OrbitShoot : MonoBehaviour {

    /// <summary>
    /// OrbitShoot Script.
    /// Created by: Tom Gibbs.
    /// Modified by: Daniel Pokladek.
    /// 
    /// This script handles when the player drags on the asteroid,
    /// it gets the initial position and adds relative force to the asteroid.
    /// Once player lets go of the button, it will shoot the asteroid in the space,
    /// where they have pointed.
    /// 
    /// </summary>

    // Hide the variables in inspector,
    // as they are assigned by the asteroid manager.
    // ----

    [HideInInspector]
    public float shootForceMultiplier;
    [HideInInspector]
    public AsteroidManager asteroidManager;
    [HideInInspector]
    public GameObject RadVis;                   // Set up using the AsteroidManager.
    [HideInInspector]
    public GameObject RadVisInstance;
    [HideInInspector]
    public Camera gameCamera;

    private Vector3 orbitPos;
    private Vector3 clickPos;
    private Vector3 initialPos;

    private Vector3 fingPos;
    private Vector3 radPos;

    private float startSize;
    private float endSize;
    private float defSize;

    private float time = 0;

    private bool moveToAsteroid = false;

    void Start ( ) {
        asteroidManager = gameObject.GetComponent<AsteroidManager>();

        gameCamera = Camera.main;
    }

    void FixedUpdate ( ) {
        #region ZoomToShoot
        if (moveToAsteroid) {
            // Zoom camera towards the selected asteroid.
            startSize = gameCamera.orthographicSize;
            endSize = GameManager.gameManager.shootCamSize;

            time += Time.deltaTime;
            gameCamera.orthographicSize = Mathf.SmoothStep(startSize, endSize, time);

            // Move camera to center of asteroid.
            Vector3 startPos = gameCamera.transform.position;
            Vector3 endPos = gameObject.transform.position;
            gameCamera.transform.position = Vector3.Lerp(startPos, new Vector3(endPos.x, endPos.y, -10), time);
        }
        #endregion
    }

    void OnMouseDown ( ) {
        // Reset time before animation, to make sure it works.
        time = 0;

        // If the owner of asteroid is the current player, perform the code below.
        // This code gets the initial position before the user starts dragging.
        if (asteroidManager.asteroidOwner == GameManager.gameManager.TurnState) {
            moveToAsteroid = true;
            orbitPos = transform.position;

            fingPos = Input.mousePosition;
            fingPos.z = 11;

            radPos = gameCamera.ScreenToWorldPoint(fingPos);
            RadVisInstance = Instantiate(RadVis, radPos, Quaternion.identity);
        }
    }

    void OnMouseDrag ( ) {
        // This code moves the asteroid according on where the player is aiming.
        // It also adds force to the asteroid, relative to how far player drags.
        if (asteroidManager.asteroidOwner == GameManager.gameManager.TurnState) {
            asteroidManager.UpdatePlanetaryGravity(asteroidManager.orbitRadius, asteroidManager.orbitSpeed, null);

            transform.position = gameCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            Vector3 allowedPos = clickPos - orbitPos;
            clickPos = gameCamera.ScreenToWorldPoint(Input.mousePosition);
            allowedPos = Vector3.ClampMagnitude(allowedPos, 3);
            transform.position = orbitPos + allowedPos;
        }
    }

    void OnMouseUp ( ) {
        moveToAsteroid = false;
        time = 0;

        Destroy(RadVisInstance);

        if (asteroidManager.asteroidOwner == GameManager.gameManager.TurnState) {
            GetComponent<Rigidbody2D>().AddForce(( orbitPos - transform.position ) * Vector3.Distance(transform.position, orbitPos) * shootForceMultiplier);
            GameManager.gameManager.ChangeTurn();
        }
    }
}