using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDebugWindow : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject canvas;
    GameObject camera;
    public Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        canvas.transform.position = camera.transform.position + offset;
    }
}
