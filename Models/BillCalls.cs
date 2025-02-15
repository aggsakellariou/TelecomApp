using System.Collections.Generic;

namespace TelecomApp.Models
{
    public class BillCalls
    {
        public required Bill Bill { get; set; }
        public List<Call> Calls { get; set; } = [];
    }
}