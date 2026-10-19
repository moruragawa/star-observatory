using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Volume telescopeVolume;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ScreenController screenController;

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

    public void GetStar(int starIndex)
    {
        HasStar = true;
        uiManager.UpdateStarCollected(starIndex);
    }

    public void UseStar()
    {
        HasStar = false;
    }

    public void GetKey()
    {
        HasKey = true;
        uiManager.ShowKeyIcon(true);
    }

    public void UseKey()
    {
        HasKey = false;
        uiManager.ShowKeyIcon(false);
    }

    public void GetGear()
    {
        HasGear = true;
        uiManager.ShowGearIcon(true);
    }

    public void UseGear()
    {
        HasGear = false;
        uiManager.ShowGearIcon(false);
    }

    public void UnlockTelescope()
    {
        IsTelescopeUsable = true;

        if (telescopeVolume != null)
        {
            telescopeVolume.enabled = false;
        }
    }

    // ヒントを確認済みにする
    public void MarkHintViewed(HintId hintId)
    {
        viewedHints.Add(hintId);
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
    }

    // ゲームクリア時の処理
    private void OnClear()
    {
        screenController.ShowClearScreen();
    }
}
