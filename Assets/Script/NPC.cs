using UnityEngine;


public class NPC : MonoBehaviour
{
    [SerializeField] float timeToStop;
    bool stop = false;
    [SerializeField] float secToStop;
    [SerializeField] float secToStart;
    [SerializeField] Transform path;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeToStop += Time.deltaTime;
        transform.position = path.transform.position;

        if (timeToStop > secToStop)
        {
            stop = true;
        }

        if (stop)
        {
            path.GetComponent<followerpath>().enabled = false;

            Invoke("start", secToStart);

        }

    }



    private void start()
    {
        path.GetComponent<followerpath>().enabled = true;
        stop = false;
        timeToStop = 0;
    }

}
