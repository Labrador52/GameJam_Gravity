using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    #region Properties

    #endregion

    #region Methods
    // Custom methods
    public static void QuitGame(bool error = false)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 在编辑器中停止播放模式
        if (error) Debug.LogError("游戏发生错误，已退出。");
        else Debug.Log("游戏已退出。");
#else
        Application.Quit(); // 在构建版本中退出游戏
#endif
    }


    public IEnumerable<Type> GetDependencies()
    {
        yield break;
    }
    
    public void Initialize()
    {
        Debug.Log("GameManager initialized");
    }

    #endregion

}
