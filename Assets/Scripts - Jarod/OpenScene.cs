using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour {

    public Scene scene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Open()
    {
        if (scene != null)
        {
            SceneManager.LoadScene(scene.name);
        }
    }
}
