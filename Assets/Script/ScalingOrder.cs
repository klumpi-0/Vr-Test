using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScalingOrder : MonoBehaviour
{

    public static List<int> scalingTypeOrder_ = null;
    public static List<int> scalingTypeOrderCopy_ = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetScalingType123()
    {
        scalingTypeOrder_ = new List<int> { 1, 2, 3 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SetScalingType132()
    {
        scalingTypeOrder_ = new List<int> { 1, 3, 2 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetScalingType213()
    {
        scalingTypeOrder_ = new List<int> { 2, 1, 3 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SetScalingType231()
    {
        scalingTypeOrder_ = new List<int> { 2, 3, 1 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetScalingType312()
    {
        scalingTypeOrder_ = new List<int> { 3, 1, 2 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SetScalingType321()
    {
        scalingTypeOrder_ = new List<int> { 3, 2, 1 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public List<int> GetScalingTypeOrder()
    {
        return scalingTypeOrder_;
    }
}
