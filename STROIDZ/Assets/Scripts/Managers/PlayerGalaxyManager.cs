using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerGalaxyManager : MonoBehaviour {

    /// <summary>
    /// PlayerGalaxyManager.
    /// Created by: Daniel Pokladek.
    /// 
    /// This script is controlling the galaxy of the player.
    /// It contains information about the galaxy, such as the
    /// health it currently is at. It will switch the sprite,
    /// depending on its health.
    /// 
    /// </summary>

    [Header("Galaxy Variables")]
    public float galaxyHealth = 5f;
    public SpriteRenderer sprRenderer;
    public Sprite[] galaxySprites;

    public Canvas Congratulations;

    void Start () {
        Congratulations = Congratulations.GetComponent<Canvas>();

        Congratulations.enabled = false;

        sprRenderer = GetComponent<SpriteRenderer>();
	}
	
	public void Damage (float _dmgAmnt) {
        galaxyHealth -= _dmgAmnt;
        UpdateHealth();
	}

    void UpdateHealth () {
        if (galaxyHealth == 5)
            sprRenderer.sprite = galaxySprites[5];
        if (galaxyHealth < 5 && galaxyHealth >= 4)
            sprRenderer.sprite = galaxySprites[4];
        if (galaxyHealth < 4 && galaxyHealth >= 3)
            sprRenderer.sprite = galaxySprites[3];
        if (galaxyHealth < 3 && galaxyHealth >= 2)
            sprRenderer.sprite = galaxySprites[2];
        if (galaxyHealth < 2 && galaxyHealth >= 1)
            sprRenderer.sprite = galaxySprites[1];
        if (galaxyHealth < 1)
        {
            Congratulations.enabled = true;
            sprRenderer.sprite = galaxySprites[0];
        }
    }
}
