using EventModule.Local;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuanShen_Demo
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
           _ = new App().Run(); 
        }
   
    }

}
