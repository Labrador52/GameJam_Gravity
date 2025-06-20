using System;
using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
    /// <summary>
    /// 获取该管理器依赖的其他管理器类型列表。预留属性，尽量避免使用。<br/>
    /// Get the dependencies of the manager. Avoid using this property if possible.
    /// </summary>
    /// <returns>管理器的依赖列表<br/>The dependencies of the manager.</returns>
    public IEnumerable<Type> GetDependencies();

    /// <summary>
    /// 初始化管理器，在ManagerInitializer中调用。<br/>
    /// Initializes the manager. Called by ManagerInitializer when the application starts.
    /// </summary>
    void Initialize();

}
