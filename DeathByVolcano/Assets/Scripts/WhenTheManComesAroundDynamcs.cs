using UnityEngine;
using System.Collections;

public class WhenTheManComesAroundDynamcs : MonoBehaviour
{

    public Transform[] LeftSideOfEnlightenment;
    public Transform[] TheOtherLeftSideOfEnlightenment;
    public float speed;
    public float timeMin;
    public float timeMax;
    public int pOneScore;
    public int PTwoScore;

    PointDistributionUnityKernelFinalBuild pointScript;
    GameObject points;


    public int godScoreValue;
    Rigidbody2D AGH;
    Transform GodDestination;
    float journeyLength;
    float startTime;
    float distcovered;
    float fracJourney;
    float distCovered;
    float timeWait;

    Vector3 lastPos;
    Vector3 gdPos;

    // Use this for initialization
    void Start()
    {
        AGH = GetComponent<Rigidbody2D>();
        StartCoroutine(GodLikePath());
    }

    // Update is called once per frame
    void Update()
    {
        //   distCovered = (Time.deltaTime - startTime) * speed;
        //    fracJourney = distCovered / journeyLength;
        //  transform.position = Vector3.Lerp(transform.position, GodDestination.position, (Vector3.Distance( lastPos, GodDestination.position) * Time.deltaTime) * speed);

        AGH.AddForce((gdPos - transform.position) * speed * Time.smoothDeltaTime);
        points = GameObject.Find("LavaCollider");
        pointScript = points.GetComponent<PointDistributionUnityKernelFinalBuild>();

    }

    /*
¦¦¦¦¦¦¦¦¦¦¦¦¦_¦¦¦¦¦
¦¦¦¦¦¦¦___¦¦_¦¦_¦¦¦
¦¦¦¦¦¦¦¯¦¯¦¦¦¦¦¯¦_¦
¦¦¦¦¦¦¦¦_¦¦¦¦¦¦¦¦¯¦_
¦¦¦¦¦¦¦¯_¯¦¦¦_____¯¯
¦¦¦¦¦___¦¦¯¯¯¯¦¦¦¦¦
¦¦¦¦¦¯___¦¦¯¯¦¦¦¦¦¦
¦¦¦¦¦¦___¦¦¯¯¯¦¦¦¦¦
¦_¦¦¦¦¦__¦¦¦¯¯¦¦¦¦¦
¦¯¦¦¦¦¦_¦¯¦¯¦¯¦¦¦¦¦
¦¦¦¦¦¦¦¦__¦¦__¦¦¦¦¦
¦¦¦¦¦¦¦¦¯¦¦¦¯¦¦_¦¦¦
¦¦¦¦¦¦¦¦¦¯_¯_¯¦_¦¦¦
¦¦¦¦¦¦¦¦¯¦¦¦¦¦¦¦¦¦¦
¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦
¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦¦ 
*/

    void LeftSideUpdate()
    {
        GodDestination = LeftSideOfEnlightenment[Random.Range(0, LeftSideOfEnlightenment.Length)];
        gdPos = GodDestination.position;
    }

    void OtherLeftSideUpdate()
    {
        GodDestination = TheOtherLeftSideOfEnlightenment[Random.Range(0, TheOtherLeftSideOfEnlightenment.Length)];
        gdPos = GodDestination.position;
    }

    void timeRandomizer()
    {
        timeWait = Random.Range(timeMin, timeMax);
    }

    IEnumerator GodLikePath()
    {
        Debug.Log("Hello, I am god, watch me perform an act of moving");
        LeftSideUpdate();
        Debug.Log("I am now going towards the left, look at me go, wooo weee!");
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, GodDestination.position);
        lastPos = transform.position;
        timeRandomizer();
        yield return new WaitForSeconds(timeWait);

        OtherLeftSideUpdate();
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, GodDestination.position);
        lastPos = transform.position;
        timeRandomizer();
        yield return new WaitForSeconds(timeWait);
        StartCoroutine(GodLikePath());

    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform.tag == "Wall")
        {
            //   AGH.isKinematic = true;
            //  AGH.isKinematic = false;
            StartCoroutine(GodLikePath());

        }

        if (collision2D.gameObject.tag == "Player1" || collision2D.gameObject.tag == "Player2")
        {
            pOneScore = pointScript.PlayerOneScore;
            PTwoScore = pointScript.PlayerTwoScore;
            

            if (collision2D.gameObject.tag == "Player1")
            {
                pOneScore = pOneScore + godScoreValue;
                pointScript.PlayerOneScore = pOneScore;
                Destroy(GameObject.FindWithTag("Player1"));
            }

            if (collision2D.gameObject.tag == "Player2")
            {
                PTwoScore = PTwoScore + godScoreValue;
                pointScript.PlayerTwoScore = PTwoScore;
                Destroy(GameObject.FindWithTag("Player2"));
            }
        }

    }


}
