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

}
