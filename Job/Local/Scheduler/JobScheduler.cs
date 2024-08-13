using Job.Local;
using System.Reflection;

namespace Job
{
    /// <summary>
    /// 综合调度器，所有的任务处理方式都是在这里
    /// 目前只支持并行
   /// </summary>   
   public class JobScheduler
    {
        private static object _lock = new object(); 
        private bool IsStop { get; set; } = false;
        /// <summary>
        /// 调度线程  
        /// </summary>       
        private Thread _thread;         
        private Semaphore _sem;        
        private Dictionary<string, SchedulerCore> _jobs = new();

        protected JobScheduler()
        {
            _thread = new Thread(() =>
             {
            while (true)
            {
                if (_jobs.Count == 0)
                {
                    IsStop = true;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("无任务阻塞等待任务");
                    _sem.WaitOne();
                }
                lock (_lock)
                {
                    try
                    {
                        foreach (SchedulerCore job in _jobs.Values)
                        {
                            (bool isSuccess, string message) = job.ExecuteCore();
                            // Console.WriteLine(message);
                            Thread.Sleep(1);
                        }
                    }
                    catch (Exception ex)
                    {
                        //补抓异常
                    }
                }
            }
              });
            _thread.IsBackground = true;
            _sem = new Semaphore(1, 1);
        }

        public JobScheduler Execute()
        {
            _thread.Start(); 
            return this;
        }
        /// <summary>
        /// 会覆盖
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="timer"></param>
        /// <param name="job"></param>       
        /// <returns></returns>
        public JobScheduler AddJobs(JobExecute execute, TimeSpan timer)
        {
            string key = GetMethodDescription<JobNameAttribute>(execute).Name;
            ThreadHelper thread = new ThreadHelper(execute, key);
            SchedulerCore scheduler = new SchedulerCore(thread, JobType.Parallel, timer);
            _jobs[key] = scheduler; if (IsStop == true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("获得job开始工作");
                _sem.Release();
            }
            return this;
        }

        public void Stop(string jobName)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("任务暂停");
            _jobs[jobName].Stop();
        }

        public void Run(string jobName)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("任务重新启动");
            _jobs[jobName].Run();
        }

        public void Kill(string jobName)
        {
            lock (_lock)
            {
                _jobs[jobName].Kill();
                _jobs.Remove(jobName);
                Console.WriteLine($"移除job{jobName}");
            }
        }

        /// <summary>
        /// 获取方式上面的特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>        
        /// <returns></returns>
        private T GetMethodDescription<T>(JobExecute service) where T : Attribute
        {
            //T attr = default(T);
            //var typeInfo = service.GetType();
            //var frames = new StackTrace().GetFrames();
            //var frame = frames?.FirstOrDefault(x => x.GetMethod().ReflectedType == typeInfo);
            //var methodInfo = frame?.GetMethod() as MethodInfo;
            //attr = methodInfo?.GetCustomAttribute<T>();
            //return attr;
            T attr = default(T);             
            var type=service.GetType();             
            attr = type.GetCustomAttribute<T>();            
            return attr;
        }

        public static JobScheduler CreatScheduler()
        {
            JobScheduler scheduler = new JobScheduler(); 
            return scheduler;
        }

        //public static JobScheduler CreateScheduler()
        //{

        //}
    }
}

 