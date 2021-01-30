using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float speed = 5.0f;
    float rotateSpeed = 1.0f;
    float jumpForce = 5.0f;

    int maxspeed = 5;

    private Rigidbody playerRB;

    public ParticleSystem jumpParticle;
    public ParticleSystem starParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(0, h * rotateSpeed, 0));

        playerRB.AddForce(v * transform.forward * speed);

        playerRB.velocity = new Vector3(Mathf.Clamp(playerRB.velocity.x, -maxspeed, maxspeed),
            Mathf.Clamp(playerRB.velocity.y, -maxspeed, maxspeed),
            Mathf.Clamp(playerRB.velocity.z, -maxspeed, maxspeed));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            jumpParticle.Play();
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("PlayScene");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            starParticle.Play();
        }
    }
}
