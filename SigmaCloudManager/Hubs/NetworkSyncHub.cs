using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SCM.Hubs
{
    /// <summary>
    /// A simple SignalR Hub for sending progress updates on 
    /// network sync and check-sync requests
    /// </summary>
    public class NetworkSyncHub : Hub
    {
        public Task JoinGroup(string groupName)
        {
            return Groups.AddAsync(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.RemoveAsync(Context.ConnectionId, groupName);
        }
    }
}
