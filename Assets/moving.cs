using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject x;
    Vector3 original;
    float direction;
    void Start()
    {
        original = transform.position;
        direction = 1;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 1000 * direction);

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 1000 * direction);
        if (Vector3.Distance(transform.position, original) > 10f)
        {
            direction *= -1f;
        }
    }
}
