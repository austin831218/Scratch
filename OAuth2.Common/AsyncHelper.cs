using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Common
{
    public class AsyncHelper
    {
        public static Task NullAsTask
        {
            get
            {
                return Task.FromResult<object>(null);
            }
        }
    
    
        public static Task RunAsynchronously(Action method)
        {
            return Task.Run(() =>
            {
                method.Invoke();
            });
        }
        public static Task<T> RunAsynchronously<T>(Func<T> method)
        {
            return Task.Run<T>(() =>
            {
                return method.Invoke();
            });
        }
    }
}
