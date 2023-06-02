using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textMesh;
    public float Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Score = Mathf.Round (Time.timeSinceLevelLoad * 100);
        textMesh.text = ""+Score;
    }
}
