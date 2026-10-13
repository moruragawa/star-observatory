using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Interactor playerInteractor;
    [SerializeField] private GameObject clearPanel;

    public enum GameState
    {
        FindStar1,
        FindStar2,
        FindStar3,
        Clear
    }

    public enum HintId
    {
        Memo,
        Candy,
        Moon,
        Flower
    }

    public GameState CurrentState { get; private set; } = GameState.FindStar1;

    public bool HasStar { get; private set; }
    public bool HasKey { get; private set; }
    public bool HasGear { get; private set; }
    public bool IsTelescopeUsable { get; private set; }

    private HashSet<HintId> viewedHints = new HashSet<HintId>();

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

    public void UnlockTelescope()
    {
        IsTelescopeUsable = true;
        Debug.Log("望遠鏡が使用可能になりました");
    }

    // ヒントを確認済みにする
    public void MarkHintViewed(HintId hintId)
    {
        viewedHints.Add(hintId);
        Debug.Log("ヒント確認：" + hintId);
    }

    // 指定したヒントを確認済みか調べる
    public bool HasViewedHint(HintId hintId)
    {
        return viewedHints.Contains(hintId);
    }

    // すべてのヒントを確認済みか調べる
    public bool HasViewedAllHints()
    {
        return HasViewedHint(HintId.Memo)
            && HasViewedHint(HintId.Candy)
            && HasViewedHint(HintId.Moon)
            && HasViewedHint(HintId.Flower);
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
        playerController.enabled = false;
        playerInteractor.enabled = false;

        clearPanel.SetActive(true);

        Debug.Log("ゲームクリア！");
    }
}
