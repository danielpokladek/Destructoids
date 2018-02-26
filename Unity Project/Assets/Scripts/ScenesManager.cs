using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public Canvas exitMenu;
    public Button Play;
    public Button Exit;

    public Button pauseGame;
    public bool paused;
    public Canvas pauseMenu;
    public Button Resume;
    public Button Restart;
    public Button adjustAudio;
    public Button quitToMainMenu;

    public Canvas audioMenu;
    public Button Done;

    public Canvas inGameQuitMenu;
    public Button yes;
    public Button no;

    public Canvas inGameRestartMenu;
    public Button rYes;
    public Button rNo;

    public Canvas inGameExitAppMenu;
    public Button eYes;
    public Button eNo;

    public Canvas nextRoundMenu;
    public Button rematch;
    public Button nextroundtoMainMenu;

    public Canvas CongratulationsToPlayer1;
    public Canvas CongratulationsToPlayer2;


    void Start()
    {
        if (exitMenu)
            exitMenu = exitMenu.GetComponent<Canvas>();

        if(Play)
            Play = Play.GetComponent<Button>();

        if(Exit)
            Exit = Exit.GetComponent<Button>();

        if(pauseMenu)
            pauseMenu = pauseMenu.GetComponent<Canvas>();

        if (Resume)
            Resume = Resume.GetComponent<Button>();

        if(Restart)
            Restart = Restart.GetComponent<Button>();

        if(adjustAudio)
            adjustAudio = adjustAudio.GetComponent<Button>();

        if(quitToMainMenu)
            quitToMainMenu = quitToMainMenu.GetComponent<Button>();

        if(audioMenu)
            audioMenu = audioMenu.GetComponent<Canvas>();

        if(Done)
            Done = Done.GetComponent<Button>();

        if(nextRoundMenu)
            nextRoundMenu = nextRoundMenu.GetComponent<Canvas>();

        if(rematch)
            rematch = rematch.GetComponent<Button>();

        if(nextRoundMenu)
            nextroundtoMainMenu = nextroundtoMainMenu.GetComponent<Button>();

        if(CongratulationsToPlayer1)
            CongratulationsToPlayer1 = CongratulationsToPlayer1.GetComponent<Canvas>();

        if(CongratulationsToPlayer2)
            CongratulationsToPlayer2 = CongratulationsToPlayer2.GetComponent<Canvas>();


        //Hides canvas at scene load. 
        exitMenu.enabled = false; 
        pauseMenu.enabled = false;
        audioMenu.enabled = false;
        inGameQuitMenu.enabled = false;
        nextRoundMenu.enabled = false;
        pauseGame.enabled = true;
        CongratulationsToPlayer1.enabled = false;
        CongratulationsToPlayer2.enabled = false;

        pauseGame.enabled = true;

        paused = false;
    }

    public void ExitMenu()
    {
        exitMenu.enabled = true;
        Play.enabled = false;
        Exit.enabled = false;
    }

    public void HideExitPopUp()
    {
        exitMenu.enabled = false;
        Play.enabled = true;
        Exit.enabled = true;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene(1);
    }

    //Toggle whether game is in paused state (bool).
    public void PauseButton()
    {
        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0;
            PauseMenu();
        }
        else if (!paused)
        {
            Time.timeScale = 1;
            HidePausePopUp();
        }
    }

    public void PauseMenu()
    {
        pauseMenu.enabled = true;
        Resume.enabled = true;
        Restart.enabled = true;
        adjustAudio.enabled = true;
        quitToMainMenu.enabled = true;
    }

    public void HidePausePopUp()
    {
        paused = false;
        pauseMenu.enabled = false;
        Resume.enabled = false;
        Restart.enabled = false;
        adjustAudio.enabled = false;
        quitToMainMenu.enabled = false;
        paused = false;
    }

    public void AudioMenu()
    {
        pauseMenu.enabled = false;
        audioMenu.enabled = true;
        Done.enabled = true;
    }

    public void HideAudioMenuPopUp()
    {
        audioMenu.enabled = false;
        Done.enabled = false;
        PauseMenu();
    }

    public void InGameQuitMenu()
    {
        pauseMenu.enabled = false;
        inGameQuitMenu.enabled = true;
        yes.enabled = true;
        no.enabled = true;
    }

    public void HideInGameQuitMenu()
    {
        inGameQuitMenu.enabled = false;
        yes.enabled = false;
        no.enabled = false;
        PauseMenu();
    }

    public void InGameRestartMenu()
    {
        pauseMenu.enabled = false;
        inGameRestartMenu.enabled = true;
        rYes.enabled = true;
        rNo.enabled = true;
    }

    public void HideInGameRestartMenu()
    {
        inGameRestartMenu.enabled = false;
        rYes.enabled = false;
        rNo.enabled = false;
        PauseMenu();
    }

    public void InGameExitApp()
    {
        pauseMenu.enabled = false;
        inGameExitAppMenu.enabled = true;
        eYes.enabled = true;
        eNo.enabled = true;
    }

    public void HideInGameExitApp()
    {
        
        inGameExitAppMenu.enabled = false;
        eYes.enabled = false;
        eNo.enabled = false;
        PauseMenu();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
