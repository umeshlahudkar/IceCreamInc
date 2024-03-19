using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneIndex;

    public void OnButtonClick()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
