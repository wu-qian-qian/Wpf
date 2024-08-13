namespace Job.Local
{
    /// <summary>
    /// 调度核心
    /// </summary>
    internal partial class SchedulerCore
    {
        public readonly ThreadHelper _threadHepler; 
        public readonly JobType _type = JobType.Parallel;
        /// <summary>
        /// 下一次执行时间
        /// </summary>
        private DateTime ExecuteTimer { get; set; }
        /// <summary>
        /// 执行时间间隔
        /// </summary>         
        private TimeSpan _timer;
        private bool IsRunning { get; set; } = true; public SchedulerCore(ThreadHelper threadHepler, JobType type, TimeSpan timer)
        {
            _threadHepler = threadHepler;
            _type = type;
            _timer = timer;
            ExecuteTimer = DateTime.Now.AddHours(_timer.Hours).AddMinutes(_timer.Minutes).AddSeconds(_timer.Seconds);
        }

        /// <summary>
        /// 执行核心
        /// </summary>         
        /// <returns></returns>
        public (bool, string) ExecuteCore()
        {
            if (IsRunning == false)
            {
                ExecuteTimer =
                DateTime.Now.AddHours(_timer.Hours).AddMinutes(_timer.Minutes).AddSeconds(_timer.Seconds); 
                return (false, "Job Is Stop");
            }
            if (DateTime.Now < ExecuteTimer)
            {
                return (false, "Job is not ready to execute");
            }
            (bool success, string message) = _threadHepler.ExecuteRelease();
            ExecuteTimer =
            DateTime.Now.AddHours(_timer.Hours).AddMinutes(_timer.Minutes).AddSeconds(_timer.Seconds); return (success, message);
        }
        public void Stop()
        {
            IsRunning = false;
        }

        public void Run()
        {
            IsRunning = true;
        }

        public void Kill()
        {
            _threadHepler.Kill();
        }
    }
}