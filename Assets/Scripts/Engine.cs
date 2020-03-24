using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Engine
{
    public static GameSessionState sessionState 
    {
        get
        {
            return session.state;
        }
    }
    private static GameSession session;
    public static void AddBall()
    {
        if (session == null)//При переходе между уровнями должно будет вызываться само
            session = new GameSession(SceneManager.GetActiveScene());
        session.AddBall();
    }
    public static void BallInVictoryZone()
    {
        session.BallInVictoryZone();
    }
    public static void BallLost()
    {
        session.BallLost();
    }
    public static void NextLevel()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentBuildIndex >= SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.LogError("No more levels");
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        session = new GameSession(SceneManager.GetActiveScene());
    }
    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        session = new GameSession(SceneManager.GetActiveScene());
    }
}
public class GameSession
{
    private int ballsTotal;
    private int ballsInVictoryZone;
    private GameSessionState _s;
    private Scene level;
    public GameSessionState state {
        get
        { 
            return _s; 
        }
        private set
        {
            _s = value;
            Debug.Log("State change detected");
            Events.OnSessionStateChanged(value);
        }
    }
    public GameSession(Scene lvl)
    {
        level = lvl;
        state = GameSessionState.InProgress;
    }
    public void AddBall()
    {
        ballsTotal += 1;
        Logger.UpdateContent(UILogDataType.GameState, "Balls amount: " + ballsTotal);
    }
    public void BallInVictoryZone()
    {
        ballsInVictoryZone += 1;
        Logger.UpdateContent(UILogDataType.GameState, "Balls amount: " + ballsTotal + ", balls in box: " + ballsInVictoryZone);
        if (ballsTotal == ballsInVictoryZone)
        {
            Logger.UpdateContent(UILogDataType.GameState, "Victory!!!");
            if (state == GameSessionState.InProgress)
                state = GameSessionState.Won;
        }
    }
    public void BallLost()
    {
        if (state == GameSessionState.InProgress)
            state = GameSessionState.Lost;
    }
}
public enum GameSessionState {Won,Lost,InProgress}
