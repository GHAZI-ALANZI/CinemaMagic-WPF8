﻿namespace CinemaMagic.ViewModels
{
    public static class TaskUtilities
    {
        public interface IErrorHandler
        {
            void HandleError(Exception ex);
        }
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }

    }
}
