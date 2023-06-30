using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
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
    public bool hideReferenceObject;

    private string fileName;
    public string ScalingType;

    public GameObject startButton;
    public GameObject stopButton;
    public GameObject OutputTextObject;
    public TextMeshProUGUI outputText;

    public string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScaSdM6IWYjlkWHJ78P1IEcHvm7uo54NvITWUPB6Erk2KbRGA/formResponse";    
    public static string UserID = null;
    public static List<int> scalingFactorList = null;
    public static List<int> scalingTypeOrder = null;
    public static List<int> scalingTypeOrderCopy = null;
    public static int currentScalingFactor = -1;
    public static Queue<int> scalingTypeQueue_;

    public Text DebugText;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        TimerRunning = false;
        recordingFinished = false;
        grabObject.GetComponent<XRGrabInteractable>().enabled = false;
        grabObjectStartPosition = grabObject.transform.position;
        fileName = "ThisHeadset/result.txt";
        if(UserID == null)
        {
            UserID = DateTime.Now.ToString("HHmmss");
        }
        if(scalingFactorList == null)
        {
            scalingFactorList = new List<int> { 1, 2, 3, 4, 5, 6 };
        }
        if(scalingTypeOrder == null)
        {
            scalingTypeQueue_ = this.GetComponent<ScalingOrder>().GetScalingTypeOrder();
        }
        // DebugText = Camera.current.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
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
            if (hideReferenceObject)
            {
                reference.GetComponent<Renderer>().enabled = false;
            }
        }
        if (recordingFinished && !TimerRunning)
        {
            SaveDataOutputText();
            stopButton.SetActive(false);
            OutputTextObject.SetActive(true);
            if (hideReferenceObject)
            {
                reference.GetComponent<Renderer>().enabled = true;
            }
        }
        if(recordingFinished && TimerRunning)
        {
            // Load next Level here
            LoadNextLevel();
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        // string output = UserID + "\n" + ScalingType + "\n" + finalTime + "\n" + HowCloseAreScales(reference.transform, grabObject.transform).ToString() + "\nPress button for next Test";
        string output = "Press button for next Test";
        outputText.text = output;

        SendOutputToForms(ScalingType, finalTime.ToString(), Math.Round(HowCloseAreScales(reference.transform, grabObject.transform), 3).ToString());
    }

    public float HowCloseAreScales(Transform referenceTrans, Transform grabObjectTrans)
    {
        float scaleComp = 1-((referenceTrans.localScale.x - grabObject.transform.localScale.x)
            + (referenceTrans.localScale.y - grabObject.transform.localScale.y)
            + (referenceTrans.localScale.z - grabObject.transform.localScale.z) /3);
        Math.Round((double)scaleComp, 5);
        return scaleComp;
    }

    public void SetBackGrabObject()
    {
        grabObject.transform.position = grabObjectStartPosition + new Vector3(0, .5f, 0);
    }

    /// <summary>
    /// Upload measured values to google forms
    /// </summary>
    /// <param name="Level"></param>
    /// <param name="Time"></param>
    /// <param name="Accuracy"></param>
    public void SendOutputToForms(string Level, string Time, string Accuracy)
    {
        StartCoroutine(Post(Level, Time, Accuracy));
    }

    IEnumerator Post(string Level, string Time, string Accuracy)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.816302759", UserID);
        form.AddField("entry.2091973494", Level);
        form.AddField("entry.1488930100", Time);
        form.AddField("entry.1150572038", Accuracy);

        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();
    }

    public void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex < 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            if(scalingTypeQueue_.Count == 0)
            {
                SceneManager.LoadScene(24);
            }
            SceneManager.LoadScene(scalingTypeQueue_.Dequeue());
            /*
            if (scalingTypeOrder.Count == 0)
            {
                scalingTypeOrder = scalingTypeOrderCopy;
                scalingFactorList.Remove(currentScalingFactor);
            }

            if (scalingFactorList.Count == 0)
            {
                // Load GoodbyeLevel
                SceneManager.LoadScene(24);
            }

            if (scalingTypeOrder.Count == 3)
            {
                var random = new System.Random();
                int index = random.Next(scalingFactorList.Count);
                currentScalingFactor = scalingFactorList[index];
                if(DebugText != null)
                {
                    DebugText.text = currentScalingFactor.ToString();
                }
            }
            int currentTechnique = scalingTypeOrder[0];
            scalingTypeOrder.RemoveAt(0);
            // Number of scenes before test + ()
            SceneManager.LoadScene(5 + (currentScalingFactor * currentTechnique));
            */
        }

    }


}
