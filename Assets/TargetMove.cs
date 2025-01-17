using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

using UnityEngine.UI;

public class TargetMove : NetworkBehaviour
{
    public Transform start;
    public Transform end;
    public GameObject TargetPrefab;
    [SerializeField] NetworkIdentity _identity = null;
    public int score;
    public Text Scoreboard;

    // Start is called before the first frame update

    void Awake()
    {

        Scoreboard = GameObject.Find("Scoreboard").GetComponent<Text>();
        score = 0;
        start = GameObject.Find("StartPoint").transform;
        end = GameObject.Find("EndPoint").transform;
    }
    void Start()
    {
        //if (_identity.isServer)
        //{
        //Debug.Log("girdim");
        if (isServer)
        {
            TargetMoveCMD();
        }
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TargetMoveCMD()
    {
        //NetworkServer.Spawn(TargetPrefab);
        StartCoroutine(WaitAndMoveStartToEnd(1f));
    }
    
    public IEnumerator WaitAndMoveStartToEnd(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // start at time X
        float journeyLength = 5f;
        float startTime = Time.time;
        while ((Time.time - startTime) / journeyLength < 1)
        { // until one second passed
            transform.position = Vector3.Lerp(start.transform.position, end.transform.position, (Time.time - startTime) / journeyLength); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }

        StartCoroutine(WaitEndMoveEndToStart(1f));
    }

    public IEnumerator WaitEndMoveEndToStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); // start at time X
        float journeyLength = 5f;
        float startTime = Time.time;
        while ((Time.time - startTime) / journeyLength < 1)
        { // until one second passed
            transform.position = Vector3.Lerp(end.transform.position, start.transform.position, (Time.time - startTime) / journeyLength); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }

        StartCoroutine(WaitAndMoveStartToEnd(1f));
    }

    void OnCollisionEnter(Collision collision)
    {
        score = score + 1;

        Scoreboard.text = score.ToString();

    }
}
