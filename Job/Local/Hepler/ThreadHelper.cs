namespace Job.Local
{
    internal partial class ThreadHelper
    {
        private readonly int _id;
        private readonly string _name; 
        private readonly string _const;
        private Thread _thread;
        //信号量这里的使用主要是用来让线程；挂起与恢复
        private Semaphore _sem;
        /// <summary>
        /// 执行Job   
        /// </summary>
        private JobExecute _jobExecute;
        /// <summary>
       /// 是否执行结束
       /// </summary>
        public bool ExecuteEnd { get; private set; }
        private bool IsKill = false;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; private set; } = string.Empty;
    }
}
