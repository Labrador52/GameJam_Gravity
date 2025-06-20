using System;
using System.Collections.Generic;
using Labrador.UnityWrench.Service;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IManager
{
    #region Properties
    private InputActions inputActions;

    #endregion

    #region Methods

    //IManager Methods
    public IEnumerable<Type> GetDependencies()
    {
        // 如果InputManager依赖于其他管理器，在这里返回它们的类型
        yield break;
    }

    public void Initialize()
    {
        // 初始化输入管理器
        inputActions = new InputActions();
        inputActions.Enable();

        {
            // 处理调试触发器事件
            Debug.Log("Debug Trigger Pressed");
        };

        Debug.Log("InputManager initialized");
    }

    #endregion
}