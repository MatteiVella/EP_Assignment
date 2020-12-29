using System;

namespace Ep_Assignment.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string code { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
