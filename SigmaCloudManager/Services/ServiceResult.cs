using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class ServiceResult
    {
        public ServiceResult()
        {
        }

        private List<string> Messages = new List<string>();

        public bool IsSuccess { get; set; }

        public List<string> GetMessageList()
        {
            return Messages.ToList();
        }

        public void Add(string message)
        {
            Messages.Add(message);
        }

        public void AddRange(IEnumerable<string> messages)
        {
            Messages.AddRange(messages);
        }

        public object Item { get; set; }
        public object Context { get; set; }
    }
}
