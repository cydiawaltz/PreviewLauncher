using UnityEngine;
using UnityEngine.SceneManagement;

public class preview_help : MonoBehaviour
{
    [SerializeField] GameObject obj0;
    [SerializeField] GameObject obj1;

    private void Start()
    {
        OnMae();
    }
    public void OnMae()
    {
        obj0.SetActive(true);
        obj1.SetActive(false);
    }

    public void OnTugi()
    {
        obj0.SetActive(false);
        obj1.SetActive(true);
    }
    public void OnHome()
    {
        SceneManager.LoadScene("Preview");
    }
}
