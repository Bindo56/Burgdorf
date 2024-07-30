using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class followerpath : MonoBehaviour
{
    [SerializeField] PathCreator pathCreation;
    public EndOfPathInstruction endOfPathInstruction;
    [SerializeField] float speed = 5;
        float distanceTravelled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreation.path.GetPointAtDistance(distanceTravelled,endOfPathInstruction);
    }

    private void Loop()
    {
        
    }
}
