using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class faceDetector : MonoBehaviour {

    public ARFaceManager faceManager;
    public FaceRunnerController controller;


    private void OnEnable() {
        faceManager.facesChanged += OnFacesChanged;
        
    }
    private void OnDisable() {

        faceManager.facesChanged -= OnFacesChanged;
    }
    private void OnFacesChanged(ARFacesChangedEventArgs args) {

        if (args.added.Count > 0) { 
            controller.face = args.added[0];
        }
    }
}