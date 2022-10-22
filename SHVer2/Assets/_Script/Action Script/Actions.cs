using System;
using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;
public static class Actions
{

    ///<summary>---------------|
    /// Stage 1 Delegates      |
    ///</summary>--------------|
    public static Action<QuizDiff> wrongAnswer;
    public static Func<float> timeLeft;

    ///<summary>---------------|
    /// Stage 2 Delegates      |
    ///</summary>--------------|

    //Stage 2 Quiz
    //Sends the answer value of a platform
    public static Action<string> answer;

    //Sends message that the quiz has started
    //This is used to enable the collider of the platforms
    public static Action quizStarted;

    //Used to send a message to punish the player for answering wrong
    public static Action<string, int> punish;

    //Increments number of quiz answered for the puzzle pieces
    public static Action<int> incrementQuiz;

    //UI Delegates
    //Update choice text
    // Also updates the answer values of the platforms
    public static Action<Choice, string> updateChoiceText;

    //Update Text
    public static Action<string, UITextType> updateText;

    //Boss related
    //timer
    public static Action timesUp;
    public static Action startTime;
    public static Action stopTime;

    //Spawn Enemy
    public static Action spawnEnemy;
    public static Action decreaseEnemyCounter;

    //Decrease projectile counter
    public static Action decreaseProjectileCounter;

    //Stop and start quiz
    public static Action startQuiz;
    public static Action stopQuiz;

    //Start attack phase
    public static Action bossTurn;

    //Player Turn
    public static Action playerTurn;

    //Damage boss
    public static Action<int> damageBoss;

    //Boss defeated
    public static Action bossDefeated;

    ///<summary>---------------|
    /// Stage 3 Delegates      |
    ///</summary>--------------|

    //Quiz mechanic
    public static Action<bool> disableParry;
    public static Action parried;
    public static Action NoAnswer;
    public static Action correctGraph;
    public static Action incorrectGraph;
    public static Action correctAnswer;
    public static Action resumeParry;
    public static Action<bool> switchShield;
    public static Action damageLimitReached;
    public static Action meteorAtkOver;
    public static Action inCorrect;
    public static Action<int> interactColor;

    //Boss animation
    public static Action shootAnim;
    public static Action meteorShootAnim;

    //Graphing
    public static Func<CurrentAxis> CurrentAxis;
    public static Func<int[]> GraphCurrentAnswer;
    public static Action<int, int, GameObject> SetAxis;
    public static Action<int> LineIntersected;
    public static Action<int> ResetLineIntersect;
    public static Func<bool> CheckGraph;
    public static Func<Stg3_Quiz> currentQuiz;


    ///<summary>---------------|
    /// In general             |
    ///</summary>--------------|

    //UI delegate
    public static Action<UITextType, string> UpdateQuestionText;
    public static Action ClearInput;
    public static Func<Canvas> canvas;
    public static Action<String> UpdateTopic;

    //Dialogue UI
    public static Action<UITextType, string> nameText;
    public static Action<UITextType, string> dialogueText;
    public static Action<char> updateDialogueText;
    public static Func<string> getDialoguetUIText;
    public static Action<UITextType, string> updateUIText;

    //Video player
    public static Func<VideoPlayer> videoPlayer;


    //Objective Manager
    public static Func<List<QuestGiver>> ListQuest;
    public static Func<List<int>> ExtrasList;

    //Add quest to the list
    public static Action<QuestGiver> addToList;

    //Audio
    public static Action<string> playAudio;
    public static Action<string> pauseAudio;

    //Set Controls
    //Allow movement
    public static Action<bool> setMovement;
    public static Action<bool> setWeapon;
    public static Action<bool> setNotepad;
    public static Action<bool> setInventory;
    public static Action<bool> setAllControls;

    //Button delegate
    public static Action GoNextScene;
    public static Action GoMainMenu;

    //Add
    public static Action<string> addEveluation;

    //Quiz
    public static Action leaveQuiz;

    //Player position
    public static Func<Transform> playerPos;

    //Camera options
    public static Action<CameraType> Switch_Camera;
    public static Action Switch_DefaultCamera;
    public static Action<CameraType> Set_DefaultCamera;

    //Checkpoint System
    public static Action<Vector2> SetCheckPoint;
    public static Action Check_Lives;

    //Health system
    public static Action<bool> ResetDeath;
    public static Action<float> SetPlayerHealth;

    //Pause control
    public static Action<bool> SetPause;

    //Increment Extras counter
    public static Action IncrementExtra;

    //Indicator
    public static Action<string> UpdateIndicator;

}
