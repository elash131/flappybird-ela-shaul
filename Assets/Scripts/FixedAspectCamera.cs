using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public sealed class FixedAspectCamera : MonoBehaviour
{
    [SerializeField] private Vector2 _targetAspect = new Vector2(9f, 16f);
    [SerializeField] private Color _barColor = Color.black;
    [SerializeField] private bool _keepOverlayCanvasesInsideCamera = true;
    [SerializeField] private float _canvasPlaneDistance = 1f;
    [SerializeField] private int _canvasSortingOrder = 1000;
    [SerializeField] private Vector2 _uiReferenceResolution = new Vector2(540f, 960f);
    [SerializeField, Range(0f, 1f)] private float _uiMatchWidthOrHeight = 0.5f;
    [SerializeField] private string _scaledCanvasName = "ScoreCanvas";

    private Camera _camera;
    private int _lastScreenWidth;
    private int _lastScreenHeight;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        ApplyAspect();
        ApplyCanvasMode();
    }

    private void Update()
    {
        if (Screen.width == _lastScreenWidth && Screen.height == _lastScreenHeight)
        {
            return;
        }

        ApplyAspect();
        ApplyCanvasMode();
    }

    private void OnPreCull()
    {
        GL.Clear(true, true, _barColor);
    }

    private void OnValidate()
    {
        _targetAspect.x = Mathf.Max(1f, _targetAspect.x);
        _targetAspect.y = Mathf.Max(1f, _targetAspect.y);
    }

    private void ApplyAspect()
    {
        _lastScreenWidth = Screen.width;
        _lastScreenHeight = Screen.height;

        float target = _targetAspect.x / _targetAspect.y;
        float window = (float)Screen.width / Screen.height;

        if (window > target)
        {
            float width = target / window;
            // Camera.rect controls which part of the screen the camera draws into.
            _camera.rect = new Rect((1f - width) * 0.5f, 0f, width, 1f);
        }
        else
        {
            float height = window / target;
            // If the screen is too tall, draw less height and keep the game centered.
            _camera.rect = new Rect(0f, (1f - height) * 0.5f, 1f, height);
        }
    }

    private void ApplyCanvasMode()
    {
        if (!_keepOverlayCanvasesInsideCamera)
        {
            return;
        }

        Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Canvas canvas in canvases)
        {
            bool isManagedCanvas = canvas.renderMode == RenderMode.ScreenSpaceOverlay
                || (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == _camera);

            if (!isManagedCanvas)
            {
                continue;
            }

            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = _camera;
            // Draw the Canvas with the same camera, so the UI stays inside the 9:16 area.
            canvas.planeDistance = _canvasPlaneDistance;
            canvas.overrideSorting = true;
            canvas.sortingOrder = _canvasSortingOrder;

            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
            if (scaler == null || canvas.name != _scaledCanvasName)
            {
                continue;
            }

            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = _uiReferenceResolution;
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = _uiMatchWidthOrHeight;
        }
    }
}
