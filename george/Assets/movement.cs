using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
** Scene_0
**
** The player controller is organized in a "top down" manner - the code for
** Scene_0 starts here and ends below at the comment for Scene_1 - and so on.
**
** Scene_0 handles minimal movement of the player. Scene_1 is loaded when the
** player leaves the wiewport.
*/

public class movement : MonoBehaviour
{
    public float speed = 1.0f;
    public float speedStep = 0; // Not used yet.
    public string axisX = "Horizontal";
    public string axisY = "Vertical";

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator amr;
    public bool Grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        /*
         * GetComponent() is a relatively expensive method, we call it here
         * and cache the result as rb, this reduces the overhead in Update().
         *
         * Since the update overhead is split across the Update() methods of
         * all of the game objects, it can add up in non-obvious ways.
         */
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        amr = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Get the amount of X and Y axis movement the user is asking for.
         *
         * There are two methods for getting the user input, GetAxis() and GetAxisRaw().
         *
         * GetAxis() "smooths" the input and the resulting values will be a float
         * between -1.0 and 1.0, with a middle value of 0 representing no movement.
         * Negative values represent left and down, positive up and right.
         *
         * GetAxisRaw() does no smoothing, it returns either -1, 0, or 1.
         */
        var moveX = Input.GetAxisRaw(axisX);
        var moveY = Input.GetAxisRaw(axisY);
        if (Grounded)
        {
            // we can jump
            if (moveY == 1)
            {
                Grounded = false;
                rb.velocity = Vector2.up * 1;
            }
            // apply force?
        }
        rb.velocity = new Vector2(moveX * speed, moveY * speed);
        if (rb.velocity != Vector2.zero)
        {
            amr.enabled = true;
            if (rb.velocity.x > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
        else
        {
            amr.enabled = false;
        }
    }


    void LoadNextScene()
    {
        /*
         * Using the SceneParameters object lets us get a Scene object back,
         * this can be useful for error checking - although that is not done
         * here (yet).
         */
//      var parameters = new LoadSceneParameters(LoadSceneMode.Single);
//
//      Scene scene = SceneManager.LoadScene("Scene_" +
//        (++GameState.SceneNumber), parameters);
//
//      // Trace execution - just in case...
//      Debug.Log("PlayerController: LoadNextScene(): loading scene '" +
//        sceneNumber + "'");
    }

    /*
    ** Scene_1
    **
    ** Add colliders to interact with objects in the game world.
    */

    /*
     * OnCollisionEnter() is called whenever we collide with another game
     * object with a collider component. We are passed a Collision 2D object,
     * other, that we can examine to determine what we collided with. When we
     * collide with an object the behavior of our sprite and of the object we
     * collided with is determined by the Unity physics engine - if you just
     * want to detect the collision, use OnTriggerEnter() and set the IsTrigger
     * property on the game object's collider.
     */
    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                Debug.Log("Player Controller: OnCollisionEnter2D(): collision with Memory: '"
                          + other.gameObject.name + "'");
                Grounded = true;
                break; 
            

         

            default:
                Debug.Log("Player Controller: OnCollisionEnter2D(): collision with untagged object: '"
                          + other.gameObject.name + "'");
                // TODO: throw exception?
                break;
        }
    }

    void Describe(GameObject memory)
    {
        throw new System.NotImplementedException();
    }
}