using System;
using System.Reflection;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.Demo.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}