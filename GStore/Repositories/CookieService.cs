using GStore.Repositories.Interfaces;
using GStore.Utils.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace GStore.Repositories
{
    public class CookieService : ICookieService
    {
        IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCookieValue()
        {

            string myCookieValue = "";

            myCookieValue = _httpContextAccessor.HttpContext.Request.Cookies[CoockieOptions.Name];

            if (String.IsNullOrEmpty(myCookieValue))
            {
                myCookieValue = SetCookie();
            }

            //SelectListItem
            //var request = HttpContext.Request;  sessionId = Guid.NewGuid().ToString();
            //HttpCookie //HttpCookie cookie = request.Cookies.Get(".AspNet.ApplicationCookie");
            return myCookieValue;
        }

        public string SetCookie()
        {
            string myCookieValue = Guid.NewGuid().ToString();

            CookieOptions cookieOptions = new CookieOptions();

            cookieOptions.Expires = DateTime.Now.AddMonths(CoockieOptions.ExpAddMonths);

            cookieOptions.IsEssential = true;
            cookieOptions.Path = "/";

            cookieOptions.Secure = true;

            _httpContextAccessor.HttpContext.Response
                .Cookies.Append(CoockieOptions.Name, myCookieValue, cookieOptions);


            return myCookieValue;
        }

        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(CoockieOptions.Name);
        }

    }
}
