using UnityEngine;

public class PlanetaryGravity : MonoBehaviour {

    /// <summary>
    /// Planetary Gravity.
    /// Created by: Tom Gibbs, Daniel Pokladek.
    /// Modified by: Daniel Pokladek.
    /// 
    /// This script handles the gravity for asteroids,
    /// at the current state it has the basic functionality.
    /// Later it will search for planets to orbit around when lost.
    /// 
    /// </summary>

    // Using the HideInInspector atribute, as all values are assigned
    // through the AsteroidManager script.
    [HideInInspector]
    public Transform targetAsteroid;
    [HideInInspector]
    public float orbitRadius;
    [HideInInspector]
    public float orbitSpeed;
    [HideInInspector]
    public float radiusSpeed;
    [HideInInspector]
    public AsteroidManager asteroidManager;

    private Vector3 orbitAxis = new Vector3(0, 0, 1);

    void Start ( ) {
        // Grab the AsteroidManager script.
        asteroidManager = GetComponent<AsteroidManager>();

        // Set the initial position for the asteroid.
        transform.position = ( transform.position - targetAsteroid.position ).normalized * orbitRadius + targetAsteroid.position;
    }

    void Update ( ) {
        if (asteroidManager.targetAsteroid != null) {
            // Create the Vector3 for desired position of the asteroid.
            Vector3 desiredPosition = ( transform.position - targetAsteroid.position ).normalized * orbitRadius + targetAsteroid.position;

            // Perform the script to mimick the orbiting of asteroid.
            transform.RotateAround(targetAsteroid.position, orbitAxis, orbitSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
            
        }
    }
}
