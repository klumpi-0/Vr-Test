using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public GameObject grabObject;
    public GameObject reference;

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
        fileName = "ThisHeadset/result.txt";
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerRunning)
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
        }
        if (recordingFinished && !TimerRunning)
        {
            SaveDataOutputText();
            stopButton.SetActive(false);
            OutputTextObject.SetActive(true);
            // Load next Level here

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
        string output = ScalingType + "\n" + finalTime + "\n" + HowCloseAreScales(reference.transform, grabObject.transform).ToString();
        sw.WriteLine(output);
    }

    public void SaveDataOutputText()
    {

        var finalTime = System.Math.Round(time, 2);
        string output = ScalingType + "\n" + finalTime + "\n" + HowCloseAreScales(reference.transform, grabObject.transform).ToString();
        outputText.text = output;
    }

    public float HowCloseAreScales(Transform referenceTrans, Transform grabObjectTrans)
    {
        float scaleComp = 1-((referenceTrans.localScale.x - grabObject.transform.localScale.x)
            + (referenceTrans.localScale.y - grabObject.transform.localScale.y)
            + (referenceTrans.localScale.z - grabObject.transform.localScale.z) /3);
        return scaleComp;
    }


}
