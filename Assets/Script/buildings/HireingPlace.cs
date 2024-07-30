using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireingPlace : MonoBehaviour
{
    public static HireingPlace instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform getPOs()
    {
        return gameObject.transform;
    }
}
