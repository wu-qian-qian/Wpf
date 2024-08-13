namespace Job.Local
{
    internal partial class ThreadHelper
    {
        public ThreadHelper(JobExecute jobExecute, string name)
        {
            _sem = new Semaphore(0, 1);
            _name = name;
            _id = Thread.CurrentThread.ManagedThreadId;
            _jobExecute = jobExecute;
            IntializeSingle();
            _thread.Start();
            _thread.IsBackground = true;
            _const = $"Thread Name:{_name}\tThread ID:{_id}";
        }

        void IntializeSingle()
        {
            this._thread = new Thread(
               () => {
                   try
                   {
                       while (IsKill == false)
                       {
                           Thread.Sleep(10); try
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
                               _ = _jobExecute.OnException(ex);
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
        /// <summary>
        /// 执行结束，只有当执行结束才允许进行下一次的执行
        /// </summary>        
        /// <returns></returns>       
        private bool DigestionExecute()
        {
            if (ExecuteEnd == true)            
            {
                ExecuteEnd = false;                
                return true;
            }
            else 
                return false;
        }

        public (bool, string) ExecuteRelease()
        {
             if (DigestionExecute() == true)
             {
                _sem.Release(); return (true, ErrorMessage);
             }
             return (false, ErrorMessage);
        }

        /// <summary>
        /// Kill the thread         
        /// /// </summary>        
        public void Kill()
        {
             _thread.Interrupt();
        }      
     }       
}
