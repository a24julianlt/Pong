using System.Collections;
using UnityEngine;

public class PelotaController : MonoBehaviour
{

    [SerializeField] GameManager gameManager;

    private Rigidbody2D rb;
    [SerializeField] float force;
    [SerializeField] float delay;

    const float MIN_ANG = 25.0f;
    const float MAX_ANG = 40.0f;

    AudioSource sfx;

    [SerializeField] AudioClip clip_start;
    [SerializeField] AudioClip clip_wall;
    [SerializeField] AudioClip clip_paddle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        //throwBall();
        //Invoke("throwBall", delay);
        transform.position = new Vector3(0, 0, 0); //Vector3.zero;

        int directionX = Random.Range(0, 2) == 0 ? -1 : 1; // El límite superior es exclusivo (el 2 quedaría fuera).
        StartCoroutine(throwBall(directionX));
    }

    IEnumerator throwBall(int directionX)
    {
        sfx.clip = clip_start;
        sfx.Play();

        transform.position = new Vector3(0, 0, 0); //Vector3.zero;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(delay);

        float angulo = Random.Range(MIN_ANG, MAX_ANG) * Mathf.Deg2Rad;
        int directionY = Random.Range(0, 2) == 0 ? -1 : 1;

        float x = Mathf.Cos(angulo) * directionX;
        float y = Mathf.Sin(angulo) * directionY;

        rb.AddForce(new Vector2(x, y) * force, ForceMode2D.Impulse);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Pala1") || collision.collider.CompareTag("Pala2"))
        {
            sfx.clip = clip_paddle;
            sfx.Play();

            var v = rb.linearVelocity;
            var boosted = v.normalized * v.magnitude * 1.1f; // +10% velocidad
            rb.linearVelocity = boosted;
        }
        else
        {
            sfx.clip = clip_wall;
            sfx.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Gol en " + other.tag + "!!");

        if (other.tag == "GoalRight")
        {
            // Lanzaremos la pelota hacia la derecha
            gameManager.AddPointP2();
            StartCoroutine(throwBall(1));
        }
        else if (other.tag == "GoalLeft")
        {
            // Lanzaremos la pelota hacia la izquierda
            gameManager.AddPointP1();
            StartCoroutine(throwBall(-1));
        }
    }
}