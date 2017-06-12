using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager Instance {
		get;
		private set;
	}
    public int levelNumber;
	public GameObject PlayPanel;
	public GameObject PrePlayPanel;
	public GameObject PausePanel;
	public GameObject GameOverPanel;
	public GameObject GameWinPanel;
  
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public GameObject StarPrePlay1;
    public GameObject StarPrePlay2;
    public GameObject StarPrePlay3;

    public float fullLevelTime;
    public float threshold3Stars;
    public float threshold2Stars;
    
    public string nextLevel;
    public int unlocked;

    public Sprite[] cardFace;
	public Sprite cardBack;
	public GameObject[] cards;
    
    private bool _init = false;
    private int cardsDestroyed;
	

    public bool prePlay = false;
    public bool play = false;
    public bool pause = false;
    public bool end = false;
    public bool win = false;
    


    private float fullTime; 
    private float timeLeft;
    public int starReached;
    public int starMaxReached;
    private string stringToSaveStars;


	// Use this for initialization
	void Start () {
        //PlayerPrefs.DeleteAll();

		Instance = this;
		prePlay = true;
		play = false;
		pause = false;
		end = false;
		win = false;
        cardsDestroyed = 0;

        stringToSaveStars = "starMaxReachedLevel" + levelNumber;
        TimeManager.startingTime = fullLevelTime;
        fullTime = TimeManager.startingTime;
        starMaxReached = PlayerPrefs.GetInt(stringToSaveStars, 0);
        ShowPrePlayStars(starMaxReached);
    }

    int CalcStars()
    {
        if (timeLeft > threshold3Stars * fullTime) return 3 ;
        else if (timeLeft > threshold2Stars * fullTime) return 2;
        else return 1;
    }

    void ShowStars(int stars)
    {
        if (stars > 0) Star1.SetActive(true);
        if (stars > 1) Star2.SetActive(true);
        if (stars > 2) Star3.SetActive(true);
    }

    void ShowPrePlayStars(int stars)
    {
        if (stars > 0) StarPrePlay1.SetActive(true);
        if (stars > 1) StarPrePlay2.SetActive(true);
        if (stars > 2) StarPrePlay3.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        timeLeft = TimeManager.startingTime;
        if (play == true)
        {
            if (!_init)
            {
                initializeCards();
            }
            if (Input.GetMouseButtonUp(0))
            {
                CheckCard();
            }
        
            foreach (GameObject c in cards)
            {
                if (c.GetComponent<Card>().state == 2)
                {
                    cardsDestroyed++;
                    if (c.GetComponent<Card>().isActiveAndEnabled)
                    {
                        c.GetComponent<Card>().destroyAnim();
                    }
                }
                Debug.Log("cards destroyed: " + cardsDestroyed);
            }
			if (cardsDestroyed == cards.Length) {
				GameManager.Instance.win = true;
				GameManager.Instance.WinGame ();
				PlayerPrefs.SetInt ("levelReached", unlocked);
			}
            cardsDestroyed = 0;
        }
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

        starReached = CalcStars();
        ShowStars(starReached);
        if (starReached > starMaxReached) PlayerPrefs.SetInt(stringToSaveStars, starReached);

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
			for (int i = 0; i < cardFace.Length; i++) {
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
		if (cards [c [0]].GetComponent<Card> ().cardValue == cards [c [1]].GetComponent<Card>().cardValue)	x = 2;

        for (int i = 0; i < c.Count; i++) {
			cards [c [i]].GetComponent<Card> ().state = x;
            if (x != 2) cards [c [i]].GetComponent<Card> ().falseCheck ();
            Card.notFlip = false;
        }

	}


}
