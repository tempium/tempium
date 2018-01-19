using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject player;
	public GameObject flag;
	public GameNode spawnNode;
	public GameNode finishNode;
	public float startwait;
	public Text finishText;

	private static bool finish;
	private bool nextStage;
	private static int sceneNumber=1;
	public GameNode currentNode;
	private int allScene;

	 
	// Use this for initialization
	void Start () {
		flag.SetActive (true);
		player.SetActive (true);
		finish = false;
		nextStage = false;
		allScene = 3;
		currentNode = spawnNode;
		finishText.text = string.Empty;
		StartCoroutine (SpawnWave ());
	}
	
	// Update is called once per frame
	void Update () {
		if (currentNode == finishNode) {
			Finish ();
			finishText.text = "You Win !";
		}
		if (nextStage) {
			if(Input.GetKeyDown(KeyCode.Space)){
				sceneNumber++;
				Debug.Log (sceneNumber);
				if (sceneNumber <= allScene) {
					SceneManager.LoadScene ("Tutorial_"+sceneNumber);
				} else {
					sceneNumber = 0;
					SceneManager.LoadScene ("Tutorial_End");
				}
			}
		}
	}

	IEnumerator SpawnWave(){
		yield return new WaitForSeconds (startwait);
		while (true) {
			currentNode = player.GetComponent<PlayerMovement>().node;
			if (finish) {
				nextStage = true;
				break;
			}
			yield return new WaitForSeconds (startwait);
		}
		yield return new WaitForSeconds (startwait);
	}
	public void Finish(){
		flag.SetActive(false);
		finish=true;
	}
	public static bool isFinish (){
		return finish;
	}
}
