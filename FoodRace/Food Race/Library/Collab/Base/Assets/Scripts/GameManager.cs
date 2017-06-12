using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager Instance {
		get;
		private set;
	}
	public GameObject PlayPanel;
	public GameObject PrePlayPanel;
	public GameObject PausePanel;
	public GameObject GameOverPanel;
	public GameObject GameWinPanel;

	public bool prePlay = false;
	public bool play = false;
	public bool pause = false;
	public bool end = false;
	public bool win = false;

	public Sprite[] cardFace;
	public Sprite cardBack;
	public GameObject[] cards;

	public string nextLevel;
	private bool _init = false;
	private int _matches = 13;
    private int cardsDestroyed;
	// Use this for initialization
	void Start () {
		Instance = this;
		prePlay = true;
		play = false;
		pause = false;
		end = false;
		win = false;
        cardsDestroyed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
			if (!_init) {
				initializeCards ();
			}
			if (Input.GetMouseButtonUp (0)) {
				CheckCard ();
			}

        foreach (GameObject c in cards) { 
            if (c.GetComponent<Card>().state == 2)
            {
                cardsDestroyed++;
                if (c.GetComponent<Card>().isActiveAndEnabled)
                {
                    c.GetComponent<Card>().destroyAnim();
                }
            }
            Debug.Log("Cards Destroyed: " + cardsDestroyed);
        }
        if (cardsDestroyed == cards.Length) LevelManager.instance.WinLevel();
        cardsDestroyed = 0;
    }

	public void Play(){
		prePlay = false;
		play = true;
		pause = false;
		PrePlayPanel.SetActive (false);
		PlayPanel.SetActive (true);
		PausePanel.SetActive (false);
		GameOverPanel.SetActive (false);
		GameWinPanel.SetActive (false);
	}

	public void Pause(){
		prePlay = false;
		play = false;
		pause = !pause;
		if (pause) {
			PrePlayPanel.SetActive (false);
			PlayPanel.SetActive (false);
			PausePanel.SetActive (true);
			GameOverPanel.SetActive (false);
			GameWinPanel.SetActive (false);
		}
		else if(!pause) {
			PrePlayPanel.SetActive (false);
			PlayPanel.SetActive (true);
			PausePanel.SetActive (false);
			GameOverPanel.SetActive (false);
			GameWinPanel.SetActive (false);
		}
	}

	public void GameOver(){
		prePlay = false;
		play = false;
		pause = false;
		end = true;
		win = false;
		PrePlayPanel.SetActive (false);
		PlayPanel.SetActive (false);
		PausePanel.SetActive (false);
		GameOverPanel.SetActive (true);
		GameWinPanel.SetActive (false);
	}

	public void WinGame(){
		prePlay = false;
		play = false;
		pause = false;
		end = false;
		win = true;
		PrePlayPanel.SetActive (false);
		PlayPanel.SetActive (false);
		PausePanel.SetActive (false);
		GameOverPanel.SetActive (false);
		GameWinPanel.SetActive (true);
	}
	public void Back(){
		TKSceneManager.ChangeScene ("Path Scene");
	}

	public void Restart(){
		TKSceneManager.ChangeScene ("Level 1");
	}

	public void NextLevel(){
		TKSceneManager.ChangeScene (nextLevel);
	}
	public void initializeCards(){
		for (int id = 0; id < 2; id++) {
			for (int i = 0; i < 5; i++) {
				bool test = false;
				int choice = 0;
				while (!test) {
					choice = Random.Range (0, cards.Length);
					test = !(cards [choice].GetComponent<Card> ().initialized);
				}
				cards [choice].GetComponent<Card> ().cardValue = i;
				cards [choice].GetComponent<Card> ().initialized = true;
			
			}
		}

			foreach (GameObject c in cards)
				c.GetComponent<Card> ().setupCards ();
	
			if (!_init)
				_init = true;
	}

	public Sprite getCardBack(){
		return cardBack;
	}

	public Sprite getCardFace(int i){
		return cardFace [i];
	}

	void CheckCard(){
		List<int> c = new List<int> ();
		for (int i = 0; i < cards.Length; i++) {
			if (cards [i].GetComponent<Card> ().state == 1) {
				c.Add (i);
			}
		}

		if (c.Count == 2)
			cardComparision (c);
	}

	void cardComparision(List<int> c){
		Card.notFlip = true;

		int x = 0;
		if (cards [c [0]].GetComponent<Card> ().cardValue == cards [c [1]].GetComponent<Card>().cardValue) {
			x = 2;
			_matches--;
			if (_matches == 0) {
				Debug.Log ("You Win");
			}
            /*
            cards[c[0]].GetComponent<Card>().destroyAnim();
            cards[c[1]].GetComponent<Card>().destroyAnim();*/

        }
        

		for (int i = 0; i < c.Count; i++) {
			cards [c [i]].GetComponent<Card> ().state = x;
            if (x != 2) cards [c [i]].GetComponent<Card> ().falseCheck ();

        }

	}


}
