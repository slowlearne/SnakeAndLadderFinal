using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Player : MonoBehaviour
{
    public Vector3 initialPosition;
    public GameObject start;
    public bool isPlayerTurn = false;
    public List<GameObject> targetObjects;
    public DiceRoller diceRoller;
    public int currentTargetIndex, FinalIndex;
    public List<GameObject> WayPoints;
    public GameManager gamemanagerobj;
    public int myPlayerId;

    public bool isInGame, IsMoving;
    private bool isMovingToFirstPosition;
    private bool isMovingToTarget;
    bool isOnLadder = false;
    bool isOnSnake = false;
    IconBlink iconblinkobj;

    int countPosition;
    public int diceValue;
    public GameObject gameOver;
    public AudioSource audiosource;
    public AudioClip audioclip,gameEndClip,playerEnterInGameClip, ladderClimbClip,snakeEatClip,PlayerDeadSound;

    public void Start()
    {

        initialPosition = gameObject.transform.position;
        gameOver.SetActive(false);
    }

    public void PlayerMove()
    {
        diceValue = diceRoller.diceResult;
        if (diceValue == 1 && !isInGame)
        {
            countPosition = 0;
            print("countPosition:" + countPosition);
            Invoke(nameof(MoveDiceToFirstPosition), 0.8f);
         

        }
        else if (isInGame)
        {
            Invoke(nameof(MoveDice), 0.8f);
           
            countPosition = countPosition + diceValue;
            print("countPosition:" + countPosition);
        }
        else
        {
            Invoke(nameof(ProceedAfterMove), 0.8f);
           
        }

    }
    public void MoveDiceToFirstPosition()
    {
        if (!isMovingToFirstPosition && !isMovingToTarget)
        {
            isMovingToFirstPosition = true;
            isInGame = true;
            audiosource.PlayOneShot(playerEnterInGameClip);
            LeanTween.move(gameObject, targetObjects[0].transform.position, 0.2f).setOnComplete(() =>
            {
                print("dice first position ma aayo");
                isMovingToFirstPosition = false;
                ProceedAfterMove();


            });
        }
    }
  public void MoveDice()
    {
        if (!isMovingToFirstPosition && !isMovingToTarget)
        {
            StartCoroutine(MoveToTarget());
        }
    }
  public IEnumerator MoveToTarget()
    {

        isMovingToTarget = true;
        FinalIndex = currentTargetIndex + diceValue;


        for (int i = 0; i < diceValue; i++)
        {

            print("FinalIndex is :" + FinalIndex);
            print("currentTargetIndex is" + currentTargetIndex);
            if ((FinalIndex + 1) <=targetObjects.Count)
            {
                currentTargetIndex += 1;

                transform.position = targetObjects[currentTargetIndex].transform.position;
                audiosource.PlayOneShot(audioclip);
                yield return new WaitForSeconds(0.2f);
                if (currentTargetIndex == 99)
                {
                    print(" game over "+diceRoller.currentPlayer+"wins");
                    gamemanagerobj.audiosource.Stop();
                    audiosource.PlayOneShot(gameEndClip);
                    diceRoller.redPlayer.isPlayerTurn = false;
                    diceRoller.bluePlayer.isPlayerTurn = false;
                    diceRoller.greenPlayer.isPlayerTurn = false;
                    diceRoller.yellowPlayer.isPlayerTurn = false;
                    TMP_Text gameovertext = gameOver.GetComponent<TMP_Text>();
                    gameovertext.text = diceRoller.currentPlayer.name + "Wins";
                    gameOver.SetActive(true);

                    gamemanagerobj.StopCoroutine();
                    StopCoroutine(iconblinkobj.startBlink());
                    }
                }
        }
        isDiceInsamePosition();
        Invoke(nameof(HoldAfterIsDiceInSamePosition), 0.2f);
  }

    void HoldAfterIsDiceInSamePosition()
    {
        CheckIfInLadder();
        CheckIfSnake();

        isMovingToTarget = false;

        if (!isOnLadder && !isOnSnake)
        {
            print("proced to  move ma pugo");
            ProceedAfterMove();
        }
    }

    public void CheckIfInLadder()
    {
        float tolerance = 0.01f;
        for (int j = 0; j < gamemanagerobj.LadderList.Count; j++)
        {
            Vector3 LaddertargetPosition = gamemanagerobj.LadderList[j].LadderPoints[0].transform.position;

            if (Vector3.Distance(transform.position, LaddertargetPosition) < tolerance)
            {
                isOnLadder = true;
                print("dice is in Ladder");

                // Check if the ladder point exists in the targetObjects list
                int ladderPointIndex = targetObjects.IndexOf(gamemanagerobj.LadderList[j].LadderPoints[1]);
                audiosource.PlayOneShot(ladderClimbClip);
                LeanTween.move(gameObject, gamemanagerobj.LadderList[j].LadderPoints[1].transform.position, 0.15f).setOnComplete(() =>
                {

                    currentTargetIndex = ladderPointIndex;
                    isDiceInsamePosition();
                    print("after ladder climb CTI is" + currentTargetIndex);
                    isOnLadder = false;
                    isMovingToTarget = false;
                    
                    if (!isOnLadder && !isOnSnake)
                    {
                        ProceedAfterMove();
                    }
                });
            }
        }
    }

    public void CheckIfSnake()
    {
        float tolerance = 0.01f;
        for (int i = 0; i < gamemanagerobj.SnakeList.Count; i++)
        {
            Vector3 targetPosition = gamemanagerobj.SnakeList[i].SnakePoints[0].transform.position;

            // Check if the current position is approximately equal to the target position
            if (Vector3.Distance(transform.position, targetPosition) < tolerance)
            {
                isOnSnake = true;
                Vector3[] arrayofVector3 = new Vector3[gamemanagerobj.SnakeList[i].SnakePoints.Count];
                print("array of vector count " + arrayofVector3);

                // Copy the positions to the array
                for (int j = 0; j < gamemanagerobj.SnakeList[i].SnakePoints.Count; j++)
                {

                    arrayofVector3[j] = gamemanagerobj.SnakeList[i].SnakePoints[j].transform.position;
                    print("snakepoints position are: " + arrayofVector3[j]);
                }
                int snakeIndex = targetObjects.IndexOf(gamemanagerobj.SnakeList[i].SnakePoints[gamemanagerobj.SnakeList[i].SnakePoints.Count - 1]);
                // Perform the LeanTween movement
                audiosource.PlayOneShot(snakeEatClip);
                gameObject.transform.LeanMoveSpline(arrayofVector3, 0.5f).setOnComplete(() =>
                {

                    // Set the current target index based on the last point in the array
                    currentTargetIndex = snakeIndex;
                    isDiceInsamePosition();
                    isOnSnake = false;
                    isMovingToTarget = false;
                    if (!isOnLadder && !isOnSnake)
                    {
                        ProceedAfterMove();
                    }
                });


            }
        }
    }

    public void ProceedAfterMove()
    {
        print("ya aayo");
        if (!isMovingToFirstPosition && !isMovingToTarget)
        {
            if (diceRoller.redPlayer.isPlayerTurn)
            {
                diceRoller.redPlayer.isPlayerTurn = false;
                
                gamemanagerobj.StopCoroutine();
                gamemanagerobj.count = 2;
                diceRoller.canRoll = true;
                gamemanagerobj.StartMyCoroutine();

            }
            else if (diceRoller.bluePlayer.isPlayerTurn)
            {
                diceRoller.bluePlayer.isPlayerTurn = false;
               
                gamemanagerobj.StopCoroutine();
                diceRoller.canRoll = true;
                gamemanagerobj.count = 3;
                gamemanagerobj.StartMyCoroutine();
            }
            else if (diceRoller.greenPlayer.isPlayerTurn)
            {
                diceRoller.greenPlayer.isPlayerTurn = false;
                
                gamemanagerobj.StopCoroutine();
                diceRoller.canRoll = true;
                gamemanagerobj.count = 4;
                gamemanagerobj.StartMyCoroutine();
            }
            else
            {
                diceRoller.yellowPlayer.isPlayerTurn = false;
                
                gamemanagerobj.StopCoroutine();
                diceRoller.canRoll = true;
                gamemanagerobj.count = 1;
                gamemanagerobj.StartMyCoroutine();
            }
        }
    }
    bool isKilledCompleted;
    public void isDiceInsamePosition()
    {
        float tolerance = 0.01f;
        print("sameposition ma pugo");
        Player[] players = new Player[] { diceRoller.redPlayer, diceRoller.bluePlayer, diceRoller.greenPlayer, diceRoller.yellowPlayer };
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != this) // Skip the current player
            {
                print("current position"+transform.position);
                print("marne ko position" + players[i].transform.position);
                if (Vector3.Distance(transform.position, players[i].transform.position) < tolerance)
                {
                    audiosource.PlayOneShot(PlayerDeadSound);
                    print("position equal bhayo");
                    // Move the other player to the first position
                    LeanTween.move(players[i].gameObject, players[i].initialPosition, 0.2f).setOnComplete(() =>
                     {
                         isKilledCompleted = true;

                     });


                    players[i].currentTargetIndex = 0;
                    players[i].isInGame = false;
                }
            }
        }
    }

}


