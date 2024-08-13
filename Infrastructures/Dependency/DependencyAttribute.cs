namespace Dependency
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class DependencyAttribute : Attribute
        {
            public bool CanDependency { get; set; }

            public DependencyAttribute(bool canDependency)
            {
                CanDependency = canDependency;
            }
        }
}
