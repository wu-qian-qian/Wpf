namespace Job.Local
{
    public class JobOptions
    {
        public JobOptions(object options)
        {
            _options = options;
        }
        public readonly object _options;
    }
}