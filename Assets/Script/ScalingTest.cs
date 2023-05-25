using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Pooling;
using UnityEngine.XR.Management;

public class ScalingTest : MonoBehaviour
{
    private InputDevice leftHandDevice;
    private InputDevice rightHandDevice;
    private List<InputDevice> inputDevices;

    private void Start()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, inputDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices);
    }

    private void Update()
    {
        if (!leftHandDevice.isValid || !rightHandDevice.isValid)
        {
            return;
        }

        // Get the position of the left hand
        if (leftHandDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftHandPosition))
        {
            Debug.Log("Left Hand Position: " + leftHandPosition);
        }

        // Get the position of the right hand
        if (rightHandDevice.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightHandPosition))
        {
            Debug.Log("Right Hand Position: " + rightHandPosition);
        }
    }
}
