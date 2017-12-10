using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    /// <summary>
    /// GameManager Script.
    /// Created by Daniel Pokladek.
    /// 
    /// Game Manager script is the heart of the game,
    /// it changes the state at which the game is and
    /// determines which player's turn it is. It also
    /// determines if the player is in the menus or in
    /// the play area.
    /// </summary>
    /// 

    public static GameManager gameManager = null;  // Static reference to the GameManager.

    [Header("Turn Settings")]
    public GameState gameState = GameState.Player1; // Setting the GameState to Player1 turn, later preferably change to menu so game starts with menu.
    public Text gameStateText;
    public float intermissionLength = 6;
    public string TurnState = "Player1";

    [Header("Camera Settings")]
    public Camera gameCamera;
    public Vector3 player1Pos = Vector3.zero;
    public Vector3 player2Pos = Vector3.zero;
    public Vector3 InterPos = Vector3.zero;
    public float defCamSize = 15f;
    public float interCamSize = 25f;
    public float shootCamSize = 10f;
    public bool moveToAsteroid = false;
    public bool moveToGameSpace = false;
    public bool moveToP1 = false;
    public bool moveToP2 = false;
    public bool moveToInter = false;

    // Animation time. RESET AFTER EACH ANIMATION!
    private float animationTime = 0;

    // Animation variables.
    private bool doAnimation;
    private float cameraStartSize;
    private float cameraEndSize;
    private Vector3 cameraStartPos;
    private Vector3 cameraEndPos;

    // Enum to determine the state of the game.
    public enum GameState {
        Menu,
        Player1,
        Player2,
        Intermission,
        EndScreen
    }

    void Awake ( ) {
        if (gameManager == null)
            gameManager = this;
        else if (gameManager != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start ( ) {
        TurnState = "Player2";  // At the start change the previous player to P2 so game switches to intermission.
        ChangeTurn();           // Change turn from P2 -> Intermission.
    }

    void Update ( ) {
        #region Animation Code
        if (doAnimation) {
            // Zoom the camera towards the asteroid.
            float currentCameraSize = gameCamera.orthographicSize;

            animationTime += Time.deltaTime;
            gameCamera.orthographicSize = Mathf.SmoothStep(currentCameraSize, cameraEndSize, animationTime);

            // Move camera towards teh center of selected asteroid.
            Vector3 cameraStartPos = gameCamera.transform.position;

            gameCamera.transform.position = Vector3.Lerp(cameraStartPos, new Vector3(cameraEndPos.x, cameraEndPos.y, -10), animationTime);

            // If the size and position of the camera is the same as end ones, set animation to off.
            if (gameCamera.orthographicSize == cameraEndSize && gameCamera.transform.position == cameraEndPos) {
                doAnimation = false;
                animationTime = 0;
            }
        }
        #endregion

        switch (gameState) {
            // Player1 state.
            case GameState.Player1:
                TurnState = "Player1";
                gameStateText.text = "Player1 Turn!";
                break;
            
            // Player2 state.
            case GameState.Player2:
                TurnState = "Player2";
                gameStateText.text = "Player2 Turn!";
                break;

            // Intermission state.
            case GameState.Intermission:
                TurnState = "Intermission";
                gameStateText.text = "6 Sec Intermission";
                break;

            // Menu not yet implemented.
            case GameState.Menu:
                TurnState = "MenuState";
                gameStateText.text = "";
                break;
        }
    }

    void MoveCamera ( Vector3 _cameraEndPos, float _cameraEndSize ) {
        cameraEndPos = _cameraEndPos;
        cameraEndSize = _cameraEndSize;
        doAnimation = true;
    }

    public void ChangeTurn ( ) {
        StartCoroutine(ChangeTurnIE());
    }

    // IEnumerators
    IEnumerator ChangeTurnIE ( ) {
        // If current turn is Player1.
        if (TurnState == "Player1") {
            // Change to intermission and show full play area.
            MoveCamera(InterPos, interCamSize);
            gameState = GameState.Intermission;
            yield return new WaitForSeconds(intermissionLength);

            // Perform camera animation.
            MoveCamera(player2Pos, defCamSize);
            gameState = GameState.Player2;
            StopCoroutine(ChangeTurnIE());
        }

        // If current turn is Player2.
        if (TurnState == "Player2") {
            // Change to intermission and show full play area.
            MoveCamera(InterPos, interCamSize);
            gameState = GameState.Intermission;
            yield return new WaitForSeconds(intermissionLength);

            // Perform camera animation.
            MoveCamera(player1Pos, defCamSize);
            gameState = GameState.Player1;
            StopCoroutine(ChangeTurnIE());
        }
    }
}
