using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
/// <summary>
/// Script used for Perspective scaling on a object
/// </summary>
public class PerspectiveScaling : MonoBehaviour
{
    public GameObject  grabObject;      // Object which should be scaled
    public GameObject camera;           // Camere through which the player looks (Take from XR Origin)

    public Text DebugTextHead;
    public Text DebugText2;

    public bool isGrabbed;              // gives us true if we are currently grabbing the object
    public bool lastIsGrabbed;          // isGrabbed value of the last frame  

    private Transform startTransform;   // The Transform value we collect at the beginning when we grab a object
    private float d0;                   // Distance between camera and starting postion
    private float ds;                   // Distance between camera and current position
    private float alpha;                // Angle which takes is taken of the screen by the object

    // Start is called before the first frame update
    void Start()
    {
        isGrabbed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrabbed)
        {
            // TODO Enter what should be updated
            if (!lastIsGrabbed)
            {
                calculateOriginalScale();
            }
            updateSacle();
        }
        if (!isGrabbed)
        {
        }

        lastIsGrabbed = isGrabbed;      // Allways have to be the last line in the Update Method
    }

    /// <summary>
    /// Changes the bool value isGrabbed everytime we grab or leave this object
    /// </summary>
    public void objectIsGrabbed()
    {
        isGrabbed = !isGrabbed;
    }
    /// <summary>
    /// Calculates initial values when the object is grabbed
    /// </summary>
    public void calculateOriginalScale()
    {
        startTransform = grabObject.transform;
        d0 = Vector3.Distance(grabObject.transform.position, camera.transform.position);
        float middle = (startTransform.localScale.x + startTransform.localScale.y + startTransform.localScale.z) / 3;
        alpha = 2 * Mathf.Rad2Deg * Mathf.Atan(middle / 2 * d0);        // Uses forced perspective
        // d0 = d0 - middle / 2;
    }

    /// <summary>
    /// Updates scale in relation to the original position
    /// </summary>
    public void updateSacle()
    {
        ds = Vector3.Distance(grabObject.transform.position, camera.transform.position);
        float middle = (grabObject.transform.localScale.x + grabObject.transform.localScale.y + grabObject.transform.localScale.z) / 3;
        // ds = ds - middle / 2;
        float newh = (2 * Mathf.Rad2Deg * Mathf.Tan(alpha / 2)) / ds;
        // grabObject.transform.localScale = new Vector3(newh, newh, newh);
        grabObject.transform.localScale = startTransform.localScale * (ds / d0);
    }
}
