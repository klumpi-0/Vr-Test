using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScalingGunPointer : MonoBehaviour
{
    public GameObject sphere;
    public GameObject middlePoint;
    public GameObject laser;

    private GameObject scaleObject;
    private Transform startTransform;
    private float d0;
    private float ds;

    public TextMeshProUGUI selectButtonText;

    public Material rayHitMaterial;
    private Material startMaterial;

    public Text DebugText;

    private RaycastHit raycastHit;
    private bool isGrabbed;
    private bool isSelected;
    private bool lastIsSelected;
    

    // Start is called before the first frame update
    void Start()
    {
        isGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLookDirection();
        ShootRay();
        showLaser();
    }

    private void FixedUpdate()
    {
        if (isSelected)
        {
            if (!lastIsSelected)
            {
                SelectIsScaleObject();
            }
            //UpdateScale();
        }
        if (!isSelected)
        {
            if (lastIsSelected)
            {
                UnselectIsScaleObject();
            }
        }
        lastIsSelected = isSelected;
    }

    public void UpdateLookDirection()
    {
        var dir = sphere.transform.position - middlePoint.transform.position;
        var rot = Quaternion.LookRotation(dir, Vector3.forward);
        middlePoint.transform.rotation = rot;
    }

    public void ShootRay()
    {
        Physics.Raycast(middlePoint.transform.position, (middlePoint.transform.position - sphere.transform.position), out raycastHit);
        //if (DebugText != null) { DebugText.text = raycastHit.transform.name; }
        if(rayHitMaterial != null)
        {
            middlePoint.GetComponent<MeshRenderer>().material = rayHitMaterial;
        }
    }

    public void TestButton()
    {
        if(startMaterial == null)
        {
            startMaterial = laser.GetComponent<MeshRenderer>().material;
        }
        if(laser.GetComponent<MeshRenderer>().material == startMaterial)
        {
            laser.GetComponent<MeshRenderer>().material = rayHitMaterial;
        }
        else
        {
            laser.GetComponent<MeshRenderer>().material = startMaterial;
        }
    }

    public void ChangeIsSelected()
    {
        isSelected = !isSelected;
    }

    public void changeIsGrabbed()
    {
        isGrabbed = !isGrabbed;
    }

    public void showLaser()
    {
        laser.SetActive(isGrabbed);
    }

    public void SelectIsScaleObject()
    {
        scaleObject = raycastHit.transform.gameObject;
        startTransform = scaleObject.transform;
        scaleObject.GetComponent<Rigidbody>().useGravity = false;
        d0 = Vector3.Distance(middlePoint.transform.position, scaleObject.transform.position);

        DebugText.text = "Selected " + scaleObject.name;
        selectButtonText.text = "Unselect";
    }

    public void UnselectIsScaleObject()
    {
        scaleObject.GetComponent<Rigidbody>().useGravity = true;

        DebugText.text = "New Text";
        selectButtonText.text = "Select";
    }

    public void UpdateScale()
    {
        ds = Vector3.Distance(middlePoint.transform.position, scaleObject.transform.position);
        scaleObject.transform.localScale = startTransform.localScale * (ds / d0);
    }

    public void pullCloser()
    {
        var move = middlePoint.transform.position - scaleObject.transform.position;
        move = move.normalized / 10;
        scaleObject.transform.position = scaleObject.transform.position + move;
    }

    public void pushAway()
    {
        var move =  scaleObject.transform.position - middlePoint.transform.position;
        move = move.normalized / 10;
        scaleObject.transform.position = scaleObject.transform.position + move;
    }
}
