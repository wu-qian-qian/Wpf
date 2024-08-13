namespace System
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class JobNameAttribute : Attribute
    {
        public string Name { get; set; }
        public JobNameAttribute(string name)
        {
            Name = name;
        }
    }
}