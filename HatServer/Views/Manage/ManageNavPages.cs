using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using JetBrains.Annotations;

namespace HatServer.Views.Manage
{
    public static class ManageNavPages
    {
        [NotNull]
        public static string ActivePageKey => "ActivePage";

        [NotNull]
        public static string Index => "Index";

        [NotNull]
        public static string ChangePassword => "ChangePassword";

        [NotNull]
        public static string ExternalLogins => "ExternalLogins";

        [NotNull]
        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        [CanBeNull]
        public static string IndexNavClass([NotNull] ViewContext viewContext) => PageNavClass(viewContext, Index);

        [CanBeNull]
        public static string ChangePasswordNavClass([NotNull] ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        [CanBeNull]
        public static string ExternalLoginsNavClass([NotNull] ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        [CanBeNull]
        public static string TwoFactorAuthenticationNavClass([NotNull] ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        [CanBeNull]
        public static string PageNavClass([NotNull] ViewContext viewContext, [CanBeNull] string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage([NotNull] this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
