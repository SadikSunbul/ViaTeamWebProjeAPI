﻿using Microsoft.AspNetCore.Builder;

namespace Core.Localization.Resource.WebApi;

public static class ApplicationBuilderLocalizationMiddlewareExtensions
{
    public static IApplicationBuilder UseResponseLocalization(this IApplicationBuilder builder) =>
        builder.UseMiddleware<LocalizationMiddleware>();
}
