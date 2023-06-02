using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideGravityScript : MonoBehaviour
{

    public string[] TagList;
    public float SidePush = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //To make it harder
        float score = Mathf.Round(Time.timeSinceLevelLoad*100);
       SidePush = Mathf.Min((int)(score / 10000) + 5, 10); 
    }

    void FixedUpdate()
    {
        foreach (string s in TagList)
        {
           foreach(GameObject g in GameObject.FindGameObjectsWithTag(s))
           {
                Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
                rb.AddForce(-g.transform.right * SidePush);
           }
        }

    }
}
