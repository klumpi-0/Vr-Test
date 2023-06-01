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
    public GameObject scalObject;

    public Text DebugText;
    public Text DebugText2;
    public int c;
    public int d;

    private Transform startTransform;

    // Start is called before the first frame update
    void Start()
    {
        c = 0;
        d = 0;
        meshRenderer = grabObject.GetComponent<MeshRenderer>();
        startMaterial = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
 
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

    public void ChangeMaterialHover()
    {
        c += 1;
        if (c == 100)
        {
            c = 0;
            if (meshRenderer.material == startMaterial)
            {
                meshRenderer.material = hoverMaterial;
            }
            else
            {
                meshRenderer.material = startMaterial;
            }

        }
    }

    public void ChangeTextSelect()
    {
        DebugText2.text += "Select ";
    }

    public void ChangeTextHover()
    {
        DebugText2.text += "Hurrrray Hover";
    }

    public void ScaleDown()
    {
        startTransform = grabObject.transform;                                                                      // Saves current scale
        grabObject.transform.localScale = grabObject.transform.localScale - grabObject.transform.localScale / 2;    // Scales object down by 50%
        DebugText2.text += grabObject.transform.localScale + "\n";                                                         // Adds scale vector to textfield
    }

    public void ScaleUp()
    {
        grabObject.transform.localScale = startTransform.localScale;                                                // Sets object back to old scale
        DebugText2.text += grabObject.transform.localScale + "\n";                                                         // Adds scale vector to textfield
    }
}
