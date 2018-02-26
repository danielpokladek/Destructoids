using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public Image countdownImage;

    public float timer = 1f;

    void Update ()
    {
        timer -= .1f * Time.deltaTime;

        countdownImage.fillAmount = timer;

        if (timer < 0)
        {
            GameObject gObject = GameObject.Find("DontDestroyOnLoad");
            GameManager _gameManager = (GameManager) gObject.GetComponent(typeof(GameManager));
            _gameManager.ChangeTurn();
        }
	}
}
