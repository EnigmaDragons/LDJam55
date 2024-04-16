using UnityEngine;
#if UNITY_EDITOR_WIN
using Input = UnityEngine.Input;
using UnityEngine.Windows;
#endif

public class EzScreen : MonoBehaviour
{
    [SerializeField] private string filename;
    
    private static int _counter;

    protected void Awake()
    {
#if UNITY_EDITOR_WIN
        while (File.Exists($"{filename}_{_counter}.png"))
            _counter++;
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            var n = $"{filename}_{_counter++}.png";
            ScreenCapture.CaptureScreenshot(n);
            Debug.Log($"Captured screenshot: {n}");
        }
    }
}
