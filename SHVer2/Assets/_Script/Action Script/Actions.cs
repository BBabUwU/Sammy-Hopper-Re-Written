using System;

public static class Actions
{
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
    public static Action<string> punish;

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

}
