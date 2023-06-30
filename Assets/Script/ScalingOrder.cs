using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScalingOrder : MonoBehaviour
{

    public static List<int> scalingTypeOrder_ = null;
    public static List<int> scalingTypeOrderCopy_ = null;
    public static Queue<int> scalingTypeQueue = null;
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
        scalingTypeQueue = new Queue<int> ( new[] { 7, 13, 19, 9, 15, 21, 6, 12, 18, 10, 16, 22, 8, 14, 20, 11, 17, 23} );
        scalingTypeOrder_ = new List<int> { 1, 2, 3 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SetScalingType132()
    {
        scalingTypeQueue = new Queue<int>(new[] { 7, 19, 13,
            9, 21, 15,
            6, 18, 12,
            10, 22, 16,
            8, 20, 14,
            11, 23, 17 });
        scalingTypeOrder_ = new List<int> { 1, 3, 2 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetScalingType213()
    {
        scalingTypeQueue = new Queue<int>(new[] { 13,7, 19,
            15,9, 21,
            12,6, 18,
            16,10, 22,
            14,8, 20,
            17, 11, 23  });
        scalingTypeOrder_ = new List<int> { 2, 1, 3 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SetScalingType231()
    {
        scalingTypeQueue = new Queue<int>(new[] { 13,19, 7,
            15,21, 9,
            12,18, 6,
            16,22, 10,
            14,20, 8,
            17, 23, 11, });
        scalingTypeOrder_ = new List<int> { 2, 3, 1 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetScalingType312()
    {

        scalingTypeQueue = new Queue<int>(new[] { 19, 7, 13,
            21, 9,15,
            18, 6,12,
            22, 10,16,
            20, 8,14,
            23, 11,17 });
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SetScalingType321()
    {
        scalingTypeQueue = new Queue<int>(new[] { 19, 13, 7,
            21, 15,9,
            18, 12,6,
            22, 16,10,
            20, 14,8,
            23, 17,11 });
        scalingTypeOrder_ = new List<int> { 3, 2, 1 };
        scalingTypeOrderCopy_ = scalingTypeOrder_;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public Queue<int> GetScalingTypeOrder()
    {
        return scalingTypeQueue;
    }
}
