using System.Runtime.CompilerServices;

namespace travelapi.Utils
{
    public static class ExceptionExtensions
    {
        #region Members :: Failin()

        public static Exception Failin(this Exception exception, string message = "", [CallerMemberName] string sourceMemberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNo = 0)
        {
            try
            {
                string errorMessage = string.Format("Fail in ({0}). File: {1}. Line: {2}.", sourceMemberName, sourceFilePath, sourceLineNo);

                var ex = (!string.IsNullOrEmpty(message))
                    ? new Exception(errorMessage, new Exception(message, exception))
                    : new Exception(errorMessage, exception);

                return ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail in Failin()", ex);
            }
        }

        #endregion 
    }
}
