using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health : MonoBehaviour
{
    public int PlayerHealth = 5;

    private SpriteRenderer sr;
    public Sprite Planet5;
    public Sprite Planet4;
    public Sprite Planet3;
    public Sprite Planet2;
    public Sprite Planet1;
    public Sprite Planet0;

    void Start()
    {        
        sr = GetComponent<SpriteRenderer>();
    }

    public void UpdateHealth(int Number)
    {
        if (Number == 5)
        {

            sr.sprite = Planet5;
        }

        if (Number == 4)
        {

            sr.sprite = Planet4;
        }

        if (Number == 3)
        {
            sr.sprite = Planet3;
        }


        if (Number == 2)
        {
            sr.sprite = Planet2;
        }


        if (Number == 1)
        {
            sr.sprite = Planet1;
        }

        if (Number == 0)
        {
            sr.sprite = Planet1;
        }
    }

    //Destroy asteroid, take health from player
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ASTEROID")
        {
            Destroy(col.gameObject);
            PlayerHealth = PlayerHealth - 1;
            UpdateHealth(PlayerHealth);
        }
    }
}