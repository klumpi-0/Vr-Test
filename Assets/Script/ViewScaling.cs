using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewScaling : MonoBehaviour
{

    public Camera camera;
    public bool isGrabed;
    public bool lastIsGrabed;
    public Text DebugText;

    public float d0;
    public float dS;
    public Transform startTransform;
    private GameObject pointerObject;

    public Ray RayOrigin;

    // Start is called before the first frame update
    void Start()
    {
        isGrabed = false;
        camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrabed)
        {
            if (!lastIsGrabed)
            {
                // First grab
                shootInitialRay();
            }
            MoveAndScaleObject();    
        }
        else
        {
            if (lastIsGrabed)
            {
                //Last grab
                EndScaling();
            }
        }
        lastIsGrabed = isGrabed;
    }

    public void shootInitialRay()
    {
        // TODO: - calculate initial scale and distance - instantiate pointer object
        RaycastHit hit = ShootRay();
        pointerObject = Instantiate(this.gameObject, hit.point, this.gameObject.transform.rotation);
        pointerObject.GetComponent<Collider>().enabled = false;
        var trans = 0.1f;
        var col = pointerObject.GetComponent<Renderer>().material.color;
        col.a = trans;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        // Get initial values
        d0 = Vector3.Distance(camera.transform.position, this.gameObject.transform.position);
        startTransform = this.gameObject.transform;
        if (DebugText)
        {
            DebugText.text = "Alpha value " + col.a; 
        }
        
    }

    public void MoveAndScaleObject()
    {
        RaycastHit hit = ShootRay();
        pointerObject.transform.position = hit.point;
        dS = Vector3.Distance(camera.transform.position, hit.point);
        pointerObject.transform.localScale = startTransform.localScale * (dS / d0);
        pointerObject.transform.position = calculatePointWithoutCollision(pointerObject.transform.position, pointerObject.transform.localScale);
        dS = Vector3.Distance(camera.transform.position, pointerObject.transform.position);
        pointerObject.transform.localScale = startTransform.localScale * (dS / d0);
        if (DebugText)
        {
            DebugText.text = "Shooting ray at" + hit.transform.name;
        }
    }

    public void EndScaling()
    {
        // Destroy pointer, move object to position, scale up to right size
        this.gameObject.transform.position = pointerObject.transform.position;
        this.gameObject.transform.localScale = pointerObject.transform.localScale;
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        Destroy(pointerObject);
        /*
        RaycastHit hit = ShootRay();
        this.gameObject.transform.position = hit.point;
        dS = Vector3.Distance(camera.transform.position, hit.point);
        this.gameObject.transform.localScale = startTransform.localScale * (dS / d0);
        this.gameObject.transform.position = calculatePointWithoutCollision(this.gameObject.transform.position, this.gameObject.transform.localScale);
        dS = Vector3.Distance(camera.transform.position, this.gameObject.transform.position);
        this.gameObject.transform.localScale = startTransform.localScale * (dS / d0);
        */
    }

    public RaycastHit ShootRay()
    {
        RaycastHit hit;
        RayOrigin = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Physics.Raycast(RayOrigin, out hit, 100f);
        return hit;
    }

    public Vector3 calculatePointWithoutCollision(Vector3 cur_pos, Vector3 localScale)
    {
        Vector3 vector = Vector3.zero;
        Vector3 directionVector = RayOrigin.direction;
        vector = cur_pos - (directionVector * localScale.x);
        return vector;
    }

    public void changeIsGrabed()
    {
        isGrabed = !isGrabed;
    }

}
