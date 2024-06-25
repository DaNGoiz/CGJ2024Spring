namespace YSFramework
{
    /// <summary>
    /// 不带参、无返回值的回调方法
    /// </summary>
    public delegate void CallBack();
    /// <summary>
    /// 带一个参数、无返回值的回调方法
    /// </summary>
    public delegate void CallBack<T>(T arg);
    /// <summary>
    /// 带二个参数、无返回值的回调方法
    /// </summary>
    public delegate void CallBack<T, X>(T arg1, X arg2);
    /// <summary>
    /// 带三个参数、无返回值的回调方法
    /// </summary>
    public delegate void CallBack<T, X, Y>(T arg1, X arg2, Y arg3);
    /// <summary>
    /// 带四个参数、无返回值的回调方法
    /// </summary>
    public delegate void CallBack<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);
    /// <summary>
    /// 带五个参数、无返回值的回调方法
    /// </summary>
    public delegate void CallBack<T, X, Y, Z, W>(T arg1, X arg2, Y arg3, Z arg4, W arg5);
    /// <summary>
    /// 带六个参数、无返回值的回调方法
    /// </summary>
    public delegate void CallBack<T, X, Y, Z, W, A>(T arg1, X arg2, Y arg3, Z arg4, W arg5, A arg6);
    /// <summary>
    /// 不带参、有返回值的回调方法
    /// </summary>
    public delegate object CallBack_Return();
    /// <summary>
    /// 带一个参数、有返回值的回调方法
    /// </summary>
    public delegate object CallBack_Return<T>(T arg1);
    /// <summary>
    /// 带二个参数、有返回值的回调方法
    /// </summary>
    public delegate object CallBack_Return<T, X>(T arg1, X arg2);
}