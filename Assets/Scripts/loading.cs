using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class loading : MonoBehaviour 
{
	[Header("Загружаемая сцена")]
	public int sceneID;
	[Header("Остальные объекты")]
	public Scrollbar loadingImg;

	
	void Start ()  
	{
        StartCoroutine(AsyncLoad());
	}

	IEnumerator AsyncLoad()
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
		while (!operation.isDone)
		{
		    float progress = operation.progress / 0.9f;
		    loadingImg.size = progress;
		    yield return null;
		}
	}
}
		

