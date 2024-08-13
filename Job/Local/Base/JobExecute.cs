namespace Job.Local
{   
    public abstract class JobExecute : IDisposable
    {
        protected JobOptions _options; protected JobExecute(JobOptions options)
        {
            _options = options;
        }
        public abstract void Dispose();
        public abstract Task Execute();
        public virtual async Task OnException(Exception ex)
        {

        }
    }
}