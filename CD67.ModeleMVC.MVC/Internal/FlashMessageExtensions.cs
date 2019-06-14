using System.Web;
using System.Web.Mvc;

namespace CD67.ModeleMVC.MVC.Internal
{
    internal static class FlashMessageExtensions
    {
        public static Controller Error(this Controller result, string message)
        {
            CreateFlashMessage(Notification.Error, message);
            return result;
        }

        public static Controller Warning(this Controller result, string message)
        {
            CreateFlashMessage(Notification.Warning, message);
            return result;
        }

        public static Controller Success(this Controller result, string message)
        {
            CreateFlashMessage(Notification.Success, message);
            return result;
        }

        public static Controller Information(this Controller result, string message)
        {
            CreateFlashMessage(Notification.Info, message);
            return result;
        }

        private static void CreateFlashMessage(Notification notification, string message)
        {
            System.Web.HttpContext.Current.Session[notification.ToString()] = message;
        }

        private enum Notification
        {
            Error,
            Warning,
            Success,
            Info
        }
    }
}