using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

public static class Engine
{
    private static int levelsPerSession;
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
    public static void LevelDone()
    {
        if (AdCaller.isReady)
            AdCaller.ShowAds();
        else
            NextLevel();
    }
    public static void NextLevel()
    {
        int newSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (newSceneBuildIndex >= SceneManager.sceneCountInBuildSettings)
        {
            newSceneBuildIndex = 1;
        }
        SceneManager.LoadScene(newSceneBuildIndex);
        session = new GameSession(SceneManager.GetActiveScene());
        levelsPerSession++;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level" + (newSceneBuildIndex - 1).ToString() + " started. Total levels count -" + levelsPerSession);
    }
    public static void RestartLevel()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level" + SceneManager.GetActiveScene().ToString() + " restarted");  
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
            if (value == GameSessionState.Won)
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level" + level.buildIndex + " won");
            if (value == GameSessionState.Lost)
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level" + level.buildIndex + " failed");
            CustomEvents.OnSessionStateChanged(value);
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
        if (ballsInVictoryZone == 0)
            AdCaller.LoadAds();
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
