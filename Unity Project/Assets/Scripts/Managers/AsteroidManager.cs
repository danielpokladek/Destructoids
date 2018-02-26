using System.Collections;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

    /// <summary>
    /// Asteroid Manager.
    /// Created by: Daniel Pokladek.
    /// 
    /// This script is the heart of the asteroid,
    /// all variables are assigned through this script.
    /// They will update other scripts when needed,
    /// this makes it easier to manage everything.
    /// 
    /// </summary>
    
    [Header("Asteroid Variables")]
    public float asteroidHealth = 1f;
    public float orbitRadius = 6f;
    public float orbitSpeed = 25f;
    public float shootForce = 55f;
    public float damageAmount = .5f;

    [Header("Gravity Settings")]
    public Transform targetAsteroid;
    public GameObject targetPlanet;

    [Header("Effects")]
    public GameObject impactFX;
    public GameObject radVisPrefab;
    public float destroyDelay = .3f;   // Delay between collision and destruction of object.

    [HideInInspector]
    public string asteroidOwner;
    public OrbitShoot orbitShootScr;                // Reference to OrbitShoot script.
    public PlanetaryGravity planetaryGravityScr;    // Reference to PlanetaryGravity script.

    // Select who is the owner of asteroid. Uses drop down menu in editor.
    public enum PlayerOwner {
        Player1,
        Player2
    }

    public PlayerOwner playerOwner = PlayerOwner.Player1;

    // Start(), when this object is initialised.
    void Start ( ) {
        switch (playerOwner) {
            case PlayerOwner.Player1:           // If planet belongs to Player1:
                asteroidOwner = "Player1";      // Set the owner to Player1
                gameObject.layer = 10;          // Set the layer of object to 10.
                break;                          // Break the case statement.

            case PlayerOwner.Player2:
                asteroidOwner = "Player2";
                gameObject.layer = 10;
                break;
        }
    }

    // Awake(), when all objects has been initialised.
    void Awake ( ) {
        orbitShootScr = GetComponent<OrbitShoot>();
        planetaryGravityScr = GetComponent<PlanetaryGravity>();

        targetPlanet = targetAsteroid.gameObject;

        UpdatePlanetaryGravity(orbitRadius, orbitSpeed, targetPlanet.transform);
        UpdateOrbitShoot(shootForce);

        // Set the references.
        orbitShootScr.RadVis = radVisPrefab;
    }

    // Update(), refreshes every second.
    void Update ( ) {

        // If asteroid's health reaches 0, kill it.
        if (asteroidHealth <= 0)
            Destroy(gameObject, destroyDelay);
    }

    #region Collisions and Triggers
    void OnCollisionEnter2D ( Collision2D coll ) {
        GameObject asteroidHit = coll.gameObject;

        if (coll.gameObject.tag == "planet")
            StartCoroutine(DestroyAsteroid(destroyDelay, asteroidHit));
    }

    void OnTriggerEnter2D ( Collider2D coll ) {
        if (coll.gameObject.name == "outOfBounds")
            Destroy(gameObject);

        if (coll.gameObject.tag == "galaxy")
            coll.gameObject.GetComponent<PlayerGalaxyManager>().Damage(1f);
    }
    #endregion

    #region Public Functions.
    // Update the PlanetaryGravity script with given values.
    public void UpdatePlanetaryGravity ( float _orbitRadius, float _orbitSpeed, Transform _targetAsteroid ) {
        if (planetaryGravityScr != null) {
            planetaryGravityScr.orbitRadius = _orbitRadius;
            orbitRadius = _orbitRadius;

            planetaryGravityScr.orbitSpeed = _orbitSpeed;
            orbitSpeed = _orbitSpeed;

            planetaryGravityScr.targetAsteroid = _targetAsteroid;
            targetAsteroid = _targetAsteroid;
        }
    }

    // Update the OrbitShoot script with given values.
    public void UpdateOrbitShoot ( float _shootForce ) {
        if (orbitShootScr != null) {
            orbitShootScr.shootForceMultiplier = _shootForce;
            shootForce = _shootForce;
        }
    }
    #endregion

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "GALAXY")
        {
            this.gameObject.SetActive(false);
        }
    }

    #region IEnumerators
    IEnumerator DestroyAsteroid ( float _destroyDelay, GameObject _asteroidHit ) {
        Instantiate(impactFX, gameObject.transform.position, gameObject.transform.rotation);

        yield return new WaitForSeconds(_destroyDelay);

        if (_asteroidHit != null)
            _asteroidHit.GetComponent<AsteroidManager>().asteroidHealth -= damageAmount;

        Destroy(gameObject);
    }
    #endregion
}
