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
	private int sceneNumber;
	public GameNode currentNode;


	 
	// Use this for initialization
	void Start () {
		finish = false;
		nextStage = false;
		sceneNumber = 0;
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
				SceneManager.LoadScene (sceneNumber);
			}
		}
	}

	IEnumerator SpawnWave(){
		yield return new WaitForSeconds (startwait);
		PlayerMovement n_player= Instantiate(player).GetComponent<PlayerMovement>();
		n_player.startNode = spawnNode;
		Instantiate (flag, finishNode.transform.position, finishNode.transform.rotation);
		while (true) {
			yield return new WaitForSeconds (startwait);
			currentNode = n_player.GetComponent<PlayerMovement>().node;
			if (finish) {
				yield return new WaitForSeconds (startwait+0.5f);
				nextStage = true;
				break;
			}
		}
	}
	public void Finish(){
		finish=true;
		flag.SetActive (false);
	}
	public static bool isFinish (){
		return finish;
	}
}
