using System;
using System.Collections.Generic;
using System.Text;

namespace Medicare.Application.Models.CommonModels.ErrorLog
{
    public class ErrorLogModel
    {
        public int ErrorId { get; set; }
        public bool IsDBError { get; set; }
        public string Error_Message { get; set; }
        public string Error_Procedure { get; set; }
        public string Error_Trace { get; set; }
    }
}
