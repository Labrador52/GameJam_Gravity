using System;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInitializer : MonoBehaviour
{
    private void Awake()
    {
        InitializeManagers();   // 初始化Managers     Initialize managers.
    }

    private void InitializeManagers()
    {
        // 获取Managers
        List<IManager> managers = new List<IManager>(GetComponentsInChildren<IManager>());

        try
        {
            managers = managers.OrderByDependencies();  // 根据依赖关系排序 Sort Managers by their dependencies.

        }

        catch (Exception e)
        {
            Debug.LogError($"初始化管理器失败: {e.Message}");
            GameManager.QuitGame();     // 如果检测到依赖环，退出游戏 Quit the game if a circular dependency is detected.
            return;

        }

        // 依次初始化Managers
        foreach (IManager manager in managers)
        {
            manager.Initialize();

        }
    }
}

public static class ManagerExtensions
{
    /// <summary>
    /// 根据初始化依赖排序。<br/>
    /// Sort the managers by their dependencies.
    /// </summary>
    /// <param name="managers">被排序的Managers。The managers to sort.</param>
    /// <returns>排序后的Managers。The sorted managers.</returns>
    public static List<IManager> OrderByDependencies(this IEnumerable<IManager> managers)
    {
        var sortedManagers = new List<IManager>();
        var visitedManagers = new HashSet<IManager>();
        var recursionStack = new HashSet<IManager>(); // 用于检测循环依赖
        var managerList = new List<IManager>(managers); // 转换为List以便多次遍历

        foreach (var manager in managerList)
        {
            if (!visitedManagers.Contains(manager))
            {
                if (!Visit(manager, visitedManagers, recursionStack, sortedManagers, managerList))
                {
                    throw new Exception("检测到循环依赖！");
                    // return managerList; // 如果检测到循环依赖，返回原始顺序
                }
            }
        }

        return sortedManagers;
    }

    /// <summary>
    /// 访问Manager，递归处理所有依赖，直到没有依赖为止，并添加到sortedManagers中。若检测到循环依赖，返回false。<br/>
    /// Visit the manager, recursively process all dependencies until there are no dependencies, and add them to sortedManagers.
    /// If a circular dependency is detected, return false.
    /// </summary>
    /// <param name="manager">被访问的Manager。The manager to visit.</param>
    /// <param name="visitedManagers">已访问的Managers。The managers that have been visited.</param>
    /// <param name="recursionStack">递归栈。The recursion stack.</param>
    /// <param name="sortedManagers">排序后的Managers。The sorted managers.</param>
    /// <param name="allManagers">所有Managers。The all managers.</param>
    /// <returns>Manager是否被访问过。Whether the manager is visited.</returns>
    private static bool Visit(IManager manager, HashSet<IManager> visitedManagers, HashSet<IManager> recursionStack, List<IManager> sortedManagers, List<IManager> allManagers)
    {
        if (recursionStack.Contains(manager))
        {
            return false; // 检测到循环依赖
        }

        if (visitedManagers.Contains(manager))
        {
            return true;
        }

        recursionStack.Add(manager);

        var dependencies = new List<IManager>();
        foreach (var type in manager.GetDependencies()) // 获取依赖的管理器类型
        {
            foreach (var m in allManagers)              // 在allManagers中查找匹配类型的管理器
            {
                if (m.GetType() == type)
                {
                    dependencies.Add(m);
                    break;
                }
            }
        }

        foreach (var dependency in dependencies)        // 递归处理所有依赖
        {
            if (!Visit(dependency, visitedManagers, recursionStack, sortedManagers, allManagers))
            {
                return false;
            }
        }

        recursionStack.Remove(manager);
        visitedManagers.Add(manager);
        sortedManagers.Add(manager);

        return true;
    }
}
