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
    public Material startMaterial;
    public Material grabMaterial;
    public Material hoverMaterial;
    private MeshRenderer meshRenderer;

    public GameObject  grabObject;
    public GameObject camera;

    public Text DebugTextHead;
    public Text DebugText2;
    public int c;
    public int d;

    public bool isGrabbed;              // gives us true if we are currently grabbing the object
    public bool lastIsGrabbed;          // isGrabbed value of the last frame  

    private Transform startTransform;
    private float d0;
    private float ds;

    // Start is called before the first frame update
    void Start()
    {
        c = 0;
        d = 0;
        meshRenderer = grabObject.GetComponent<MeshRenderer>();
        startMaterial = meshRenderer.material;
        isGrabbed = false;
        //DebugTextHead.text = "isGrabbed";
        //DebugTextHead.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrabbed)
        {
            //DebugTextHead.enabled = true;
            // TODO Enter what should be updated

            if (!lastIsGrabbed)
            {
                calculateOriginalScale();
            }
            updateSacle();

        }
        if (!isGrabbed)
        {
            //DebugTextHead.enabled = false;
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
    }

    /// <summary>
    /// Updates scale
    /// </summary>
    public void updateSacle()
    {
        ds = Vector3.Distance(grabObject.transform.position, camera.transform.position);
        grabObject.transform.localScale = startTransform.localScale * (ds / d0);
    }

    public void ChangeMaterialSelect()
    {
        if (meshRenderer.material == startMaterial)
        {
            meshRenderer.material = grabMaterial;
        }
        else
        {
            meshRenderer.material = startMaterial;
        }
    }

    public void ScaleDown()
    {
        startTransform = grabObject.transform;                                                                      // Saves current scale
        grabObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);    // Scales object down by 50%
        DebugText2.text += grabObject.transform.localScale + "\n";                                                         // Adds scale vector to textfield
    }

    public void ScaleUp()
    {
        grabObject.transform.localScale = startTransform.localScale;                                                // Sets object back to old scale
        DebugText2.text += grabObject.transform.localScale + "\n";                                                         // Adds scale vector to textfield
    }


    public void firstSelectEnter()
    {
        DebugText2.text += "First Select\n";
    }

    public void LastSelectLeave()
    {
        DebugText2.text += "Last Select Leave\n";
    }
}
