using UnityEngine;

public class ApplicationExitController : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}