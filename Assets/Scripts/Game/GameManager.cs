using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        FindStar1,
        FindStar2,
        FindStar3,
        Clear
    }

    public GameState CurrentState { get; private set; } = GameState.FindStar1;

    public bool HasStar { get; private set; }
    public bool HasKey { get; private set; }
    public bool HasGear { get; private set; }

    public void GetStar()
    {
        HasStar = true;
        Debug.Log("星を入手");
    }

    public void UseStar()
    {
        HasStar = false;
        Debug.Log("星を使用");
    }

    public void GetKey()
    {
        HasKey = true;
        Debug.Log("鍵を入手");
    }

    public void UseKey()
    {
        HasKey = false;
        Debug.Log("鍵を使用");
    }

    public void GetGear()
    {
        HasGear = true;
        Debug.Log("歯車を入手");
    }

    public void UseGear()
    {
        HasGear = false;
        Debug.Log("歯車を使用");
    }

    // ゲーム状態を更新する
    public void AdvanceState()
    {
        switch (CurrentState)
        {
            case GameState.FindStar1:
                CurrentState = GameState.FindStar2;
                break;
            case GameState.FindStar2:
                CurrentState = GameState.FindStar3;
                break;
            case GameState.FindStar3:
                CurrentState = GameState.Clear;
                OnClear();
                break;
        }

        Debug.Log("現在のGameState：" + CurrentState);
    }

    // ゲームクリア時の処理
    private void OnClear()
    {
        Debug.Log("ゲームクリア！");
    }
}
