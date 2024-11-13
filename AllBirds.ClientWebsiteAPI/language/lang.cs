using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

public class TwoLetterLangConstraint : IRouteConstraint
{
    

    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        var value = values[routeKey]?.ToString();
        return !string.IsNullOrEmpty(value) && value.Length == 2 && Regex.IsMatch(value, @"^[a-zA-Z]{2}$");
    }
}