using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using mshtml;
using SHDocVw;

namespace SingleSignOn
{
    public class WebApplicationLogin
    {
        const string UserIDPrompt = ">>userId: ";
        const string PasswordPromp = ">>password: ";
        const string CyberoamUserNameControlID = "username";
        const string CyberoamPasswordControlID = "password";
        const string CyberoamTitle = "Captive Portal";

        private static string UserId { get; set; }
        private static string Password { get; set; }

        /// <summary>
        /// Log-into the web application.
        /// </summary>
        /// <param name="navigationUrl">The navigation URL.</param>
        /// <param name="userNameTextBoxID">The user name text box ID.</param>
        /// <param name="passwordTextBoxID">The password text box ID.</param>
        public static void Login(string navigationUrl, string userNameTextBoxID, string passwordTextBoxID)
        {
            InternetExplorer browser = new InternetExplorer();
            object mVal = System.Reflection.Missing.Value;
            browser.Navigate(navigationUrl, ref mVal, ref mVal, ref mVal, ref mVal);

            HTMLDocument pageDocument = new HTMLDocument();
            System.Threading.Thread.Sleep(2000);
            pageDocument = (HTMLDocument)browser.Document;
            LoginInternal(userNameTextBoxID, passwordTextBoxID, pageDocument);
            browser.Visible = true;

        }

        /// <summary>
        /// Log-into Cyberoam.
        /// </summary>
        public static void LoginCyberRoam()
        {
            InternetExplorer browser = new InternetExplorer();
            object mVal = System.Reflection.Missing.Value;
            string googleUrl = @"http://google.com";
            browser.Navigate(googleUrl, ref mVal, ref mVal, ref mVal, ref mVal);

            HTMLDocument pageDocument = new HTMLDocument();
            System.Threading.Thread.Sleep(2000);
            pageDocument = (HTMLDocument)browser.Document;
            if (pageDocument.title.Contains(CyberoamTitle))
            {
                LoginInternal(CyberoamUserNameControlID, CyberoamPasswordControlID, pageDocument);
                System.Threading.Thread.Sleep(500);
            }
            browser.Visible = false;
        }

        /// <summary>
        /// Login helper method.
        /// </summary>
        /// <param name="userNameTextBoxID">The user name text box ID.</param>
        /// <param name="passwordTextBoxID">The password text box ID.</param>
        /// <param name="pageDocument">The page document.</param>
        private static void LoginInternal(string userNameTextBoxID, string passwordTextBoxID, HTMLDocument pageDocument)
        {
            HTMLInputElement userIDControl = (HTMLInputElement)pageDocument.all.item(userNameTextBoxID, 0);
            if (userIDControl != null)
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    Console.Write(UserIDPrompt);
                    UserId = Console.ReadLine();
                }
                userIDControl.value = UserId;
            }
            HTMLInputElement passwordControl = (HTMLInputElement)pageDocument.all.item(passwordTextBoxID, 0);
            if (passwordControl != null)
            {
                if (string.IsNullOrEmpty(Password))
                {
                    Console.Write(PasswordPromp);
                    Password = Console.ReadLine();
                }
                passwordControl.value = Password;
                passwordControl.form.submit();
            }
        }
    }
}
,