using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    public Debris debris;
    public int totalDebris = 10;
    public GameObject respawn;
    public int lives = 5;
	public AudioSource explode;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Deadly")
        {
            OnExplode();
            if (lives > 0)
            {
                lives--;
                transform.Translate(respawn.transform.position - transform.position);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            }
            else
            {
                Destroy(gameObject);
                // TODO: Change scene to Game Over
            }
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Deadly")
        {
            OnExplode();
            if (lives > 0)
            {
                lives--;
                transform.Translate(respawn.transform.position - transform.position);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            }
            else
            {
                Destroy(gameObject);
                // TODO: Change scene to Game Over
            }
        }
    }

    void OnExplode()
    {
		explode.Play();
        for (int i = 0; i < totalDebris; i++)
        {
            transform.TransformPoint(0, -100, 0);
            var clone = Instantiate(debris, transform.position, Quaternion.identity) as Debris;
            var body2D = clone.GetComponent<Rigidbody2D>();
            body2D.AddForce(Vector3.right * Random.Range(-1000, 1000));
            body2D.AddForce(Vector3.up * Random.Range(500, 2000));
		}
    }
}
