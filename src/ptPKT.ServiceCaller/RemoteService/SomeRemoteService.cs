using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ptPKT.ServiceCaller.RemoteAddressStrategy;
using ptPKT.ServiceCaller.ServiceCaller;

namespace ptPKT.ServiceCaller.RemoteService
{
    public class SomeRemoteService : IRemoteService
    {
        private readonly IServiceCallWrapper _callWrapper;
        private IRemoteAddressPath _addressPath;

        public SomeRemoteService(IServiceCallWrapper callWrapper, IRemoteAddressPath addressPath)
        {
            _callWrapper = callWrapper;
            _addressPath = addressPath;
        }

        private string RemoteAddress => _addressPath.GetServiceAddress();
        
        public IRemoteService SetRemoteAddress(IRemoteAddressPath addressPath)
        {
            _addressPath = addressPath;
            return this;
        }

        public async Task<string> GetTasksStatusAsync(int taskId)
        {
            object taskInfo = null; // some meaningful object
            
            await _callWrapper.WrapTmServiceCall(RemoteAddress, async client =>
            {
                taskInfo = await client.GetTaskInfoAsync(taskId);
            });

            return taskInfo.ToString(); // extract status
        }

        public async Task<string> AddTaskAsync(string taskName, string arguments, DateTime? runAfter = null)
        {
            object taskInfo = null; // convert task to request object

            object response = null; // proper responseType
            
            await _callWrapper.WrapTmServiceCall(RemoteAddress, async client =>
            {
                 response = await client.AddTaskAsync(taskInfo);
            });

            return response.ToString(); // return proper status
        }
    }
}