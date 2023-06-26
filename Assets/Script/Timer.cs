using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Timer : MonoBehaviour
{

    public GameObject grabObject;
    public GameObject reference;

    public Vector3 grabObjectStartPosition;

    public float time;
    public bool TimerRunning;
    public bool recordingFinished;

    private string fileName;
    public string ScalingType;

    public GameObject startButton;
    public GameObject stopButton;
    public GameObject OutputTextObject;
    public TextMeshProUGUI outputText;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        TimerRunning = false;
        recordingFinished = false;
        grabObject.GetComponent<XRGrabInteractable>().enabled = false;
        grabObjectStartPosition = grabObject.transform.position;
        fileName = "ThisHeadset/result.txt";
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerRunning && !recordingFinished)
        {
            time += Time.deltaTime;
        }

    }

    public void ChangeTimerState()
    {
        ChangeRecordingFinished();
        TimerRunning = ! TimerRunning;
        UpdateScene();
    }

    public void ChangeRecordingFinished()
    {
        if(!recordingFinished && TimerRunning)
        {
            recordingFinished = true;
        }
    }

    public void UpdateScene()
    {
        if (!recordingFinished && !TimerRunning)
        {
            
        }
        if (!recordingFinished && TimerRunning)
        {
            startButton.SetActive(false);
            stopButton.SetActive(true);
            grabObject.GetComponent<XRGrabInteractable>().enabled = true;
        }
        if (recordingFinished && !TimerRunning)
        {
            SaveDataOutputText();
            stopButton.SetActive(false);
            OutputTextObject.SetActive(true);
            
        }
        if(recordingFinished && TimerRunning)
        {
            // Load next Level here
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void StopTimer()
    {
        TimerRunning = false;
    }

    public void SaveData()
    {

        var finalTime = System.Math.Round(time, 2);
        if (!File.Exists(fileName))
        {
            var sr = File.CreateText(fileName);
        }
        var sw = File.AppendText(fileName);
        string output = ScalingType + "\n" + finalTime + "\n" + HowCloseAreScales(reference.transform, grabObject.transform).ToString() + "\nPress button for next Test";
        sw.WriteLine(output);
    }

    public void SaveDataOutputText()
    {

        var finalTime = System.Math.Round(time, 2);
        string output = ScalingType + "\n" + finalTime + "\n" + HowCloseAreScales(reference.transform, grabObject.transform).ToString() + "\nPress button for next Test";
        outputText.text = output;
    }

    public float HowCloseAreScales(Transform referenceTrans, Transform grabObjectTrans)
    {
        float scaleComp = 1-((referenceTrans.localScale.x - grabObject.transform.localScale.x)
            + (referenceTrans.localScale.y - grabObject.transform.localScale.y)
            + (referenceTrans.localScale.z - grabObject.transform.localScale.z) /3);
        return scaleComp;
    }

    public void SetBackGrabObject()
    {
        grabObject.transform.position = grabObjectStartPosition + new Vector3(0, .5f, 0);
    }

}
