using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialColor : MonoBehaviour
{
    public Material startMaterial;
    public Material hoverMaterial;
    public Material grabMaterial;

    public MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<MeshRenderer>();
        startMaterial = renderer.material;
    }

    public void SetStartMaterial()
    {
        renderer.material = startMaterial;
    }

    public void SetHoverMaterial()
    {
        renderer.material = hoverMaterial;
    }

    public void SetGrabMaterial()
    {
        renderer.material = grabMaterial;
    }
}
