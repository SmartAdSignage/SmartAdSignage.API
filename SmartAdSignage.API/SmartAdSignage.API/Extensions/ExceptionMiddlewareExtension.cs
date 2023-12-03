using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Serilog;
using SmartAdSignage.Core.DTOs.Exception;
using SmartAdSignage.Services.Services.Interfaces;
using System;
using System.Net;
using System.Reflection;
using System.Security.Authentication;

namespace SmartAdSignage.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        private static readonly Serilog.ILogger Logger = Log.ForContext(MethodBase.GetCurrentMethod()?.DeclaringType);


        public static void ConfigureExceptionHandler(this IApplicationBuilder app/*, ILoggerService logger*/)
        {
            app.UseExceptionHandler(
                appError =>
                {
                    appError.Run(async context =>
                    {
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                        //await response.WriteAsync(message);

                        /*context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";*/


                        if (contextFeature != null)
                        {
                            Logger.Error(contextFeature.Error, "error during executing {Context}", context.Request.Path.Value);
                            //var response = context.Response;
                            context.Response.ContentType = "application/json";

                            // get the response code and message
                            var status = GetResponse(contextFeature.Error);
                            //response.StatusCode = (int)status;
                            //logger.LogError($"Something went wrong: {contextFeature.Error}");
                            await context.Response.WriteAsync(
                                new ExceptionResponse()
                                {
                                    StatusCode = (int) status,
                                    Message = contextFeature.Error.InnerException == null ? contextFeature.Error.Message : contextFeature.Error.InnerException.Message /*"Internal Server Error."*/
                                }.ToString());
                        }
                    });
                });
        }

        public static HttpStatusCode GetResponse(Exception exception)
        {
            Exception exception1 = exception.InnerException ?? exception;
            var code = exception1 switch
            {
                KeyNotFoundException
                                    or FileNotFoundException => HttpStatusCode.NotFound,
                UnauthorizedAccessException
                                    or AuthenticationException => HttpStatusCode.Unauthorized,
                ArgumentNullException
                                    or NullReferenceException
                                    or ArgumentException
                                    or InvalidOperationException
                                    or DbUpdateException
                                    or AutoMapperMappingException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };
            return code;
        }
    }
}
