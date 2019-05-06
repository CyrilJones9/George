using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour
{
    public float speed = 30;

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw("horizontal");
        GetComponent<Rigidbody2D>().horizontal = new Vector2(0, v) * speed;
    }
}

}
