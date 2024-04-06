using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;
using UnityEngine.Events;

public class FootstepSystem : MonoBehaviour
{

    [Range(0,20f)]
    public float frequency = 10.0f;

    public UnityEvent onFootStep;

    bool isTriggered = false;

    float Sin;


    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if (inputMagnitude > 0)
        {
            StartFootsteps();
        }
    }

   private void StartFootsteps()
    {
        Sin = Mathf.Sin(Time.time * frequency);

        if (Sin > 0.97f && isTriggered == false)
        {



            isTriggered = true;
            UnityEngine.Debug.Log("Tic");
            onFootStep.Invoke();
                  
               
        }  else if(isTriggered == true && Sin < -0.97f)
        {

            isTriggered= false;
        }    
    }
}
