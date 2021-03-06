﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using System;

namespace Jackett.Server.Middleware
{
    public class RewriteRules
    {
        public static void RewriteBasePath(RewriteContext context)
        {
            var request = context.HttpContext.Request;

            string serverBasePath = Helper.ServerService.BasePath() ?? string.Empty;

            if (request.Path != null && request.Path.HasValue && serverBasePath.Length > 0
                && (request.Path.Value.StartsWith(serverBasePath + "/", StringComparison.Ordinal)
                    || request.Path.Value.Equals(serverBasePath, StringComparison.Ordinal)))
            {
                string path = request.Path.Value.Substring(serverBasePath.Length);
                path = string.IsNullOrEmpty(path) ? "/" : path;
                request.Path = new PathString(path);
            }
        }
    }
}
