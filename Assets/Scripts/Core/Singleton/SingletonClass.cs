namespace Auto.Core
{
    /// <summary>
    /// 单例类
    /// </summary>
    /// <typeparam name="T">需要单例的类</typeparam>
    public class SingletonClass<T> where T : new()
    {
        private static T _instance;

        public static T Ins
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        public virtual void Dispose()
        {
            _instance = default;
        }
    }
}