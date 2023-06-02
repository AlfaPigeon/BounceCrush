using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerScript : MonoBehaviour
{
    public GameObject RedBox;
    public GameObject Ads;
    public GameObject HealthUp;

    public float delta = 5f;
    private bool summon = false;
    private float prev;

    // Start is called before the first frame update
    void Start()
    {
        prev = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (summon) { 
            prev = Time.time;


            int ran = Random.Range(0, 70);

            if (ran == 0) { Instantiate(HealthUp, new Vector3(transform.position.x, transform.position.y, transform.position.z),transform.rotation); summon = false; return; } else if (ran == 1) { Instantiate(Ads, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation); summon = false; return; }



            GameObject red = Instantiate(RedBox, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

            float score = Mathf.Round(Time.timeSinceLevelLoad * 100);

            if (score < 2000)
            {
                red.transform.localScale = new Vector3(1f, Random.Range(1, 2), 1f);
                ran = Random.Range(0, 7);
            }
            else if (score < 3000)
            {
                red.transform.localScale = new Vector3(1f, Random.Range(1, 3), 1f);
                ran = Random.Range(0, 5);
            }
            else if (score < 5000)
            {
                red.transform.localScale = new Vector3(1f, Random.Range(1, 4), 1f);
                ran = Random.Range(0, 4);
            }
            else if (score < 15000)
            {
                red.transform.localScale = new Vector3(1f, Random.Range(1, 5), 1f);
                ran = Random.Range(0, 3);
            }
            else if (score < 50000)
            {
                red.transform.localScale = new Vector3(1f, Random.Range(1, 6), 1f);
                ran = Random.Range(0, 2);
            }
            else
            {
                red.transform.localScale = new Vector3(1f, Random.Range(1, 7), 1f);
                ran = Random.Range(0, 2);
            }


            
            if (ran == 0)
            {
                red.GetComponent<Rigidbody2D>().gravityScale = -1;
            }
            else
            {
                red.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
         summon = false;
        }else if(Time.time > prev + delta) { summon = true; } 
        
    }
}
