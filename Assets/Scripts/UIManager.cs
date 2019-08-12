﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // used to communicate with UI elements in Unity

public class UIManager : MonoBehaviour
{
  // we needed to bring in UnityEngine.UI (above) to have the Text variable since it's a UI element
  [SerializeField]
  private Text _scoreText;
  // get a handle to text
  // Start is called before the first frame update

  // The actual image field that you will use to alternate between the 4 live sprites. (The Lives_Display_img in the Canvas)
  [SerializeField]
  private Image _LivesImg;
  // Sprites to alternate between depending on how many lives left (assign the sprites in the Inspector in Unity by changing the size of this field to 4 and dragging & dropping the 4 sprites)
  [SerializeField]
  private Sprite[] _liveSprites;

  [SerializeField]
  private Text _gameOverText;

  [SerializeField]
  private Text _restartText;

  private GameManager _gameManager;

  void Start()
  {
    // assign text component to the handle so when game starts, there is a score that can be updated
    _scoreText.text = "Score: " + 0;
    // set this object to false to not display "Game Over" at the start of the game. Since _gameOverText is not a GameObject type, we must put .gameObject first
    _gameOverText.gameObject.SetActive(false);

    _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

    if (_gameManager == null)
    {
      Debug.LogError("Game Manager is NULL");
    }
  }

  // Update is called once per frame
  public void UpdateScore(int playerScore)
  {
    // don't forget to put ".text" b/c you can't assign anything to just _scoreText
    _scoreText.text = "Score: " + playerScore;
  }

  public void UpdateLives(int currentLives)
  {
    // access display image sprite and give it a new one based on currentLives
    _LivesImg.sprite = _liveSprites[currentLives];

    // when out of lives, GameOverSequence() will display game over text by setting it's object active. This code should NOT go in the Player script b/c displaying UI things is the UI Manager's job
    if (currentLives < 1)
    {
      GameOverSequence();
    }
  }

  // all code for a Game Over is in here
  void GameOverSequence()
  {
    // notify Game Manager that the game is over so it can trigger a restart when player presses the "R" key
    _gameManager.GameOver();
    _gameOverText.gameObject.SetActive(true);
    // Show restart text
    _restartText.gameObject.SetActive(true);

    StartCoroutine(GameOverFlicker());
  }

  // causes the flickering Game Over effect
  IEnumerator GameOverFlicker()
  {

    while (true)
    {
      // turn off 
      _gameOverText.gameObject.SetActive(false);

      // wait half a second
      yield return new WaitForSeconds(0.5f);

      // turn on 
      _gameOverText.gameObject.SetActive(true);

      // wait half a second (the flicker won't work if there's no wait time here so don't forget to add it )
      yield return new WaitForSeconds(0.5f);

    }
  }



}
