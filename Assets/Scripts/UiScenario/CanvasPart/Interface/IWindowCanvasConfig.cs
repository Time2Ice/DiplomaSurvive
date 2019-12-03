using UnityEngine;
using ScreenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode;

namespace UiScenario.CanvasPart
{
    public interface IWindowCanvasConfig
    {
        Vector2 ReferenceResolution { get; }
        RenderMode RenderMode { get; }
        ScreenMatchMode ScreenMatchMode { get; }
        CanvasAlignmentType AlignmentType { get; }
        int OrderRange { get; }
        int PixelsPerUnit { get; }
        CanvasCameraType CameraType { get; }
        float CameraFieldOfView { get; }
        bool InheritParentCameraSize { get; }
        float MainCameraSize { get; }        
    }
}