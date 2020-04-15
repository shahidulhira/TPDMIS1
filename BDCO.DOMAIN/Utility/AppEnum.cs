namespace BDCO.Domain.Utility
{
    public class ExecutionResult
    {
        public bool OperationResult { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }

        string methodName = "";        
        public string MethodName 
        { 
            get 
            {
                return methodName; 
            } 
        }

        public ExecutionResult(string _methodName)
        {
            methodName = _methodName;
        }

        public void SetResult(bool operationResult, string dataError, errorType dataErrorType, string message)
        {
            OperationResult = operationResult;
            ErrorCode = ErrorNumber.SetType(dataError, dataErrorType);
            Message = message;
        }
        public void SetResult(bool operationResult, string errorCode, string message)
        {
            OperationResult = operationResult;
            ErrorCode = errorCode;
            Message = message;
        }
    }
    public static class ErrorNumber
    {
        public static string PermissionDenied = "1000050";
        public static string DataOperationError = "1000051";
        public static string SetType(string ErrorCode, errorType eType)
        {
            string sRet = "";
            if (eType == errorType.ConnectionError) sRet = ErrorCode + " (Connection Error)";
            if (eType == errorType.DataOperationError) sRet = ErrorCode + " (Data Operation Error)";
            if (eType == errorType.SendMailError) sRet = ErrorCode + " (Send Mail Error)";
            if (eType == errorType.ReportError) sRet = ErrorCode + " (Report Error)";
            if (eType == errorType.LoginError) sRet = ErrorCode + " (Login Error)";
            if (eType == errorType.PermissionError) sRet = ErrorCode + " (Permission Error)";
            return sRet;
        }
    }
    public enum errorType
    {
        ConnectionError = 0,
        DataOperationError = 1,
        SendMailError = 2,
        ReportError = 3,
        LoginError = 4,
        PermissionError = 5
    };

    public enum dbOperation
    {
        Insert,
        Delete,
        Update,
        CopyAndInsert
    };

    public enum AppsCookie
    {
        IAT_PNGO,
        IAT_DIV,
        IAT_DIS,
        IAT_UPA,
        IAT_UNI,
        IAT_IATNO,
        IAT_SCH,
        IAT_GRD,
        IAT_SECTION,
        IAT_TEACHER,
        IAT_CONDUCTIONDATE,
        TIQ_PNGO,
        TIQ_DIS,
        TIQ_UPA,
        TIQ_UNI,
        TIQ_SCH,
        CRCChk_PNGO,
        CRCChk_DIS,
        CRCChk_UPA,
        CRCChk_UNI,
        CRCChk_SCH,
        CRCChk_SCONDUCTIONDATE,
        CROReading_PNGO,
        CROReading_DIS,
        CROReading_UPA,
        CROReading_UNI,
        CROReading_SCH,
        CROReading_GRD,
        CROReading_CONDUCTIONDATE
    }

    public enum MessageType
    {
        Error = 0,
        Success = 1,
        Validation = 2
    }

    public enum ConnectionType
    {
        Open,
        Close,
        OpenGOClose
    }


    //EF Block
    public enum controlType
    {
        KendoDropDown = 0,
        KendoListView = 1,
        MultiSelect = 2,
        KendoTreeView = 3
    }

    public enum operationType
    {
        ShowRecord = 0,
        SaveRecord = 1,
        UpdateRecord = 2,
        DeleteRecord = 3
    }
}
