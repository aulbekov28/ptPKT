using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ptPKT.ServiceCaller.RemoteAddressStrategy;

namespace ptPKT.ServiceCaller.RemoteService
{
    public interface IRemoteService
    {
        IRemoteService SetRemoteAddress(IRemoteAddressPath settingsPath);
        
        Task<string> GetTasksStatusAsync(int taskId);
        
        Task<string> AddTaskAsync(string taskName, string arguments, DateTime? runAfter = null);
    }
}