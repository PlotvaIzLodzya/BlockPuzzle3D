using System;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class CameraSizeAdjust : MonoBehaviour
    {
        private Camera _camera;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            AdjustCameraSizeToScreenResolution(1920f, 1080f, 5f);
        }

        private void AdjustCameraSizeToScreenResolution(float targeScreentWidth, float targetScreenHeight, float defaultOrthCamSize)
        {
            float currentAspect = (float)Screen.width / (float)Screen.height;
            var targetHeightToCamSizeRatio = targetScreenHeight / defaultOrthCamSize;
            _camera.orthographicSize = targeScreentWidth / currentAspect / targetHeightToCamSizeRatio;
        }
    }
}