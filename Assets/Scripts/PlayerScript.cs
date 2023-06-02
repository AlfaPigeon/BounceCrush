using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerScript : MonoBehaviour
{
    public float thrust = 800.0f;
    public float bounce = 100.0f;
    public float velocity = 30.0f;
    public float min_bounce = 100.0f;
    public float max_bounce = 150.0f;
    public GameObject EndGamePanel;
    public GameObject[] healthbar;
    public int shield = 3;
    public Rigidbody2D rb;
    private bool touch = false;

    private bool hit = false;
    private IEnumerator coroutine;



    public bool ads = false;
    string gameId = "3772339";
    bool testMode = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Advertisement.Initialize(gameId, testMode);
        for (int i = 0; i < shield; i++)
        {
                healthbar[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0 && !touch && !hit)
        {
            Debug.Log(Input.GetTouch(0).tapCount);
            rb.AddForce(-transform.up * thrust, ForceMode2D.Force);
            bounce = Mathf.Min(bounce + velocity, max_bounce);
            touch = true;
            hit = true;
        }
        else if(Input.touchCount == 0)
        {
            touch = false;
        }

    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Debug.Log("touching the ground");
            rb.AddForce(transform.up * bounce, ForceMode2D.Force);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            hit = false;
            AudioSource source = GetComponent<AudioSource>();

            source.Play();

            source.SetScheduledEndTime(AudioSettings.dspTime + 0.2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Ground":
                if (!hit) bounce = Mathf.Max(bounce - velocity, min_bounce);
                break;
            case "RedBox":
                if (shield > 0) {
                 
                    GetComponent<AudioSource>().PlayOneShot(collision.gameObject.GetComponent<AudioSource>().clip);
                    shield--;
                    healthbar[shield].SetActive(false);
                    rb.AddForce(transform.up * bounce, ForceMode2D.Force);
                    Destroy(collision.gameObject);
                }
                else
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.AddForce(-transform.right * 600, ForceMode2D.Force);
                break;
            case "HealthUp":
                if (shield < 5)
                {
                    GetComponent<AudioSource>().PlayOneShot(collision.gameObject.GetComponent<AudioSource>().clip);
                    healthbar[shield].SetActive(true);shield++;
                }
                Destroy(collision.gameObject);
                break;
            case "Ads":
                GetComponent<AudioSource>().PlayOneShot(collision.gameObject.GetComponent<AudioSource>().clip);
                if (shield < 5)
                {
                    healthbar[shield].SetActive(true); shield++;
                }
                if (shield < 5)
                {
                    healthbar[shield].SetActive(true); shield++;
                }
                Destroy(collision.gameObject);
                break;
        }





    }

    private float score = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Destroy")
        {
            GameObject current_score = GameObject.FindGameObjectWithTag("InGameScore");
            try {
            score = float.Parse(current_score.GetComponent<TMPro.TextMeshProUGUI>().text);
            current_score.SetActive(false);
            }
            catch { Debug.Log("Null pointer in parse float"); }
            
            coroutine = LoadEndGamePanel(1.0f);
            StartCoroutine(coroutine);
            
        }
    }


    private IEnumerator LoadEndGamePanel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        EndGamePanel.SetActive(true);
        GameObject post_score = GameObject.FindGameObjectWithTag("PostGameScore");
        GameObject high_score = GameObject.FindGameObjectWithTag("HighScore");

        float hscore = PlayerPrefs.GetFloat("HighScore");
        hscore = Mathf.Max(score, hscore);
        PlayerPrefs.SetFloat("HighScore",hscore);

        try
        {
            post_score.GetComponent<TMPro.TextMeshProUGUI>().text = "" + score;
            high_score.GetComponent<TMPro.TextMeshProUGUI>().text = "" + hscore;
        }
        catch
        {
            Debug.Log("Score Reference exception");
        }

        if (Advertisement.IsReady() && ads)
        {
            Advertisement.Show();
        }

        Destroy(gameObject);

    }

}
