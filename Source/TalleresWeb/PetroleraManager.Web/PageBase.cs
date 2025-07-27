using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL.Fwk.Presentation.Web.Pages;
using System.Web.UI;

namespace PetroleraManager.Web
{
    public class PageBase : PLPageBase
    {
        protected override void OnLoad(EventArgs e)
        {

            //if (!Context.User.Identity.IsAuthenticated)
            //{
            //    Response.Redirect("/Account/login.aspx");
            //}

            base.OnLoad(e);

        }
    }

    public static class PageExtensions
    {
        public static IEnumerable<T> AllControls<T>(this Control startingPoint) where T : Control
        {
            bool hit = startingPoint is T;
            if (hit)
            {
                yield return startingPoint as T;
            }
            foreach (var child in startingPoint.Controls.Cast<Control>())
            {
                foreach (var item in AllControls<T>(child))
                {
                    yield return item;
                }
            }
        }
    }    
}
