<%@ Page Language="C#" %>

<%
    var filePath = MapPath("~/404.html");
    Response.StatusCode = 404;
    Response.ContentType = "text/html; charset=utf-8";
    Response.WriteFile(filePath);
%>