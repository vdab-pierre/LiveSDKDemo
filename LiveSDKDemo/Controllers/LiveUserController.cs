using LiveSDKUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiveSDKDemo.Controllers
{
    public class LiveUserController : Controller
    {
        public void SigninToLive()
        {
            LiveUser.LiveSignin();
        }

        public ViewResult ShowCode(string code)
        {
            return View(code);
        }

        public ViewResult ShowUserSpecs()
        {
            var user = LiveUser.GetUser();
            return View(user);
        }
    }
}