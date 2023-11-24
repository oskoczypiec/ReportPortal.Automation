namespace ReportPortal.Core.Utilities
{
    public static class ObjectFactory
    {
        public static T Get<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}
