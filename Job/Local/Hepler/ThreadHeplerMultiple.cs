namespace Job.Local
{
    internal partial class ThreadHelper
    {
        public ThreadHelper(string name)
        {
            _sem = new Semaphore(0, 1);
            _name = name;
            _id = Thread.CurrentThread.ManagedThreadId;
            IntializeMultiple();
            _thread.Start();
            _thread.IsBackground = true;
            _const = $"Thread Name:{_name}\tThread ID:{_id}";
        }
        private void IntializeMultiple()
        {
            this._thread = new Thread(
               () => {
                   try
                   {
                       while (IsKill == false)
                       {
                           Thread.Sleep(10); 
                           try
                           {
                               if (_jobExecute != null)
                               {
                                   //无需要使用异步等待直接；这里使用了单一线程为其工作所以可以直接阻塞
                                   _jobExecute.Execute().GetAwaiter().GetResult();
                               }
                           }
                           catch (ApplicationException ex)
                           {
                              ErrorMessage = $"{_const}\n{ex.Message}";
                              Console.ForegroundColor = ConsoleColor.Red;
                              Console.WriteLine("捕获到异常：" + ErrorMessage);
                           }
                           finally
                           {
                               ExecuteEnd = true;
                               _sem.WaitOne();
                           }
                       }
                   }
                   catch (ThreadInterruptedException ex)
                   {
                       Console.ForegroundColor = ConsoleColor.Red;
                       Console.WriteLine($"job被杀死{_name}");
                       IsKill = true;
                   }
                   finally
                   {
                       _jobExecute.Dispose();
                   }
               }
               );
        }

        public void Run(JobExecute job)
        {
            _jobExecute = job;
            _sem.Release();
        }
    }
}