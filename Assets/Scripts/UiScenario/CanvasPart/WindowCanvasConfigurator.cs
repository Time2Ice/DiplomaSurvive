using UnityEngine;
using UnityEngine.UI;
using ScreenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode;

namespace UiScenario.CanvasPart
{
    public class WindowCanvasConfigurator : IWindowCanvasConfigurator
    {
        private readonly UnityEngine.Camera _camera;

        public WindowCanvasConfigurator(UnityEngine.Camera camera)
        {
            _camera = camera;
        }
        public void UpdateCanvasSettings(Canvas canvas, IWindowCanvasConfig config)
        {
            var canvasScaler = canvas.GetComponent<CanvasScaler>();
            canvasScaler.referenceResolution = config.ReferenceResolution;
            canvasScaler.referencePixelsPerUnit = config.PixelsPerUnit;

            UpdateAlignment(config.AlignmentType, canvasScaler);
            UpdateScreenMatchMode(config.ScreenMatchMode, canvasScaler);

            /*var camera = Camera.main;
            camera.orthographic = config.CameraType == CanvasCameraType.Orthogonal;
            camera.fieldOfView = config.CameraFieldOfView;
            if (!config.InheritParentCameraSize)
            {
                camera.orthographicSize = config.MainCameraSize;
            }*/

            if (config.RenderMode != RenderMode.ScreenSpaceOverlay)
            {
                canvas.worldCamera = _camera;
            }
            canvas.renderMode = config.RenderMode;

            if(config.AlignmentType != CanvasAlignmentType.Custom)
            {
                canvas.gameObject.SetActive(true);
            }
        }

        private void UpdateAlignment(CanvasAlignmentType alignment, CanvasScaler scaler)
        {
            if(alignment == CanvasAlignmentType.PreferWidth)
            {
                UpdateScalePrefferWidth(scaler);
            }
            else if(alignment == CanvasAlignmentType.PreferHeight)
            {
                UpdateScalePrefferHeight(scaler);
            }
        }

        private void UpdateScreenMatchMode(ScreenMatchMode screenMatchMode, CanvasScaler scaler)
        {
            if (scaler.screenMatchMode != screenMatchMode)
            {
                scaler.screenMatchMode = screenMatchMode;                
            }
        }

        private void UpdateScalePrefferWidth(CanvasScaler scaler)
        {
//            var scaleX = Screen.width / scaler.referenceResolution.x;
//            var scaleY = Screen.height / scaler.referenceResolution.y;
            var matchWidthOrHeightFactor = 1.0f;//scaleX >= scaleY ? 1 : 0;
            scaler.matchWidthOrHeight = matchWidthOrHeightFactor;
        }

        private void UpdateScalePrefferHeight(CanvasScaler scaler)
        {
//            var scaleX = Screen.width / scaler.referenceResolution.x;
//            var scaleY = Screen.height / scaler.referenceResolution.y;
            var matchWidthOrHeightFactor = 1.0f;// = scaleX <= scaleY ? 1 : 0;
            scaler.matchWidthOrHeight = matchWidthOrHeightFactor;
        }
    }
}
