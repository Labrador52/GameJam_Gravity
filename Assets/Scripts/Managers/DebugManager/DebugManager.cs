using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugManager : MonoBehaviour, IManager
{
    #region Properties
    [Tooltip("是否启用调试输入功能")]
    public bool enableDebugInput = true;

    #endregion

    #region Methods
    public IEnumerable<Type> GetDependencies()
    {
        return new List<Type>
        {
            typeof(InputManager)
        };
    }

    // 重写 Initialize 方法
    public void Initialize()
    {

        if (enableDebugInput)
        {
            Debug.Log("DebugManager: 调试输入已启用。");
            // TODO: 等待 IInputGetter 实现后取消注释以下代码
            /*
            // 尝试从 Service 获取 IInputGetter 实例
            // IInputGetter inputGetter = Service.Get<IInputGetter>();
            // if (inputGetter != null)
            // {
            //     // 获取 Debug Action Map 并订阅 DebugTrigger 的 performed 事件
            //     // 假设 IInputGetter 有一个获取 InputActions 的方法
            //     // inputGetter.GetInputActions().Debug.DebugTrigger.performed += OnDebugTriggerPerformed;
            //     Debug.Log("DebugManager: 已尝试订阅 DebugTrigger 事件。");
            // }
            // else
            // {
            //     Debug.LogError("DebugManager: 无法获取 IInputGetter 实例，调试输入可能无法工作。");
            // }
            */
        }
        else
        {
            Debug.Log("DebugManager: 调试输入已禁用。");
            // TODO: 等待 InputAction.Debug 实现后取消注释以下代码
            /*
            // 如果调试输入被禁用，则禁用 InputAction.Debug Action Map
            // 假设你的 InputActions 类名为 MyInputActions，并且有一个 Debug Action Map
            // new MyInputActions().Debug.Disable();
            // Debug.Log("DebugManager: 已尝试禁用 Debug Action Map。");
            */
        }

        Debug.Log("DebugManager initialized");

    }

    // TODO: 实现 OnDebugTriggerPerformed 方法，等待 InputActions.Debug.DebugTrigger 存在后取消注释
    /*
    private void OnDebugTriggerPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("DebugManager: DebugTrigger 被触发了！");
        // 在这里添加调试触发后的逻辑
    }
    */

    // TODO: 如果需要，重写 OnDestroy 来取消订阅事件
    /*
    private void OnDestroy()
    {
        if (enableDebugInput)
        {
            // 确保在销毁时取消订阅，防止内存泄漏
            // IInputGetter inputGetter = Service.Get<IInputGetter>();
            // if (inputGetter != null)
            // {
            //     inputGetter.GetInputActions().Debug.DebugTrigger.performed -= OnDebugTriggerPerformed;
            // }
        }
    }
    */

    #endregion
}
