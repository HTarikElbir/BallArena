using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private AudioSource hit;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 5.0f;
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    private float newYPos = -0.5f;
    private float originalTimeScale;
   
    
    
  
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        originalTimeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed *-1);
        powerupIndicator.transform.position = transform.position + new Vector3(0,newYPos, 0);
        if (transform.position.y < -5)
        {
            Instantiate(effect, transform.position, transform.rotation);
            SceneManager.LoadScene("GameOverScene");

        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDownRoutine());

        }
    }
    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            hit.Play();
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            Instantiate(effect, transform.position, transform.rotation);
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with" + collision.gameObject.name + "with powerup set to" + hasPowerup);
        }else if (collision.gameObject.CompareTag("Enemy"))
        {
            hit.Play();
        }
    }

    void PauseScene() 
    {
        //pauseText.gameObject.SetActive(true);

    }
}
