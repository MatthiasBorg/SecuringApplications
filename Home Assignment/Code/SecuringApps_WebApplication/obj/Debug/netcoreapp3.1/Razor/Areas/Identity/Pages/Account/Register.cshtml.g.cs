#pragma checksum "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "34b4ee256fe9a5c1cab28d5c56f3e5b451ae469a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Pages_Account_Register), @"mvc.1.0.razor-page", @"/Areas/Identity/Pages/Account/Register.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\_ViewImports.cshtml"
using SecuringApps_WebApplication.Areas.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\_ViewImports.cshtml"
using SecuringApps_WebApplication.Areas.Identity.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\_ViewImports.cshtml"
using SecuringApps_WebApplication.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\_ViewImports.cshtml"
using SecuringApps_WebApplication.Areas.Identity.Pages.Account;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34b4ee256fe9a5c1cab28d5c56f3e5b451ae469a", @"/Areas/Identity/Pages/Account/Register.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"571b4d787b9ae171fc00d5299a54e458e931ed0a", @"/Areas/Identity/Pages/_ViewImports.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7e1a3f5545abe73918e57089063129d3e8bc52fe", @"/Areas/Identity/Pages/Account/_ViewImports.cshtml")]
    public class Areas_Identity_Pages_Account_Register : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("external-account"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "./ExternalLogin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ValidationScriptsPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
  
    ViewData["Title"] = "Register";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>");
#nullable restore
#line 7 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\n\n<div class=\"row\">\n    <div class=\"col-md-6 col-md-offset-2\">\n        <section>\n            <h4>Teacher Registrations</h4>\n            <hr />\n");
#nullable restore
#line 14 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
              
                if ((Model.ExternalLogins?.Count ?? 0) == 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div>
                        <p>
                            There are no external authentication services configured. See <a href=""https://go.microsoft.com/fwlink/?LinkID=532715"">this article</a>
                            for details on setting up this ASP.NET application to support logging in via external services.
                        </p>
                    </div>
");
#nullable restore
#line 23 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "34b4ee256fe9a5c1cab28d5c56f3e5b451ae469a8394", async() => {
                WriteLiteral("\n                        <div>\n                            <p>\n");
#nullable restore
#line 29 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
                                 foreach (var provider in Model.ExternalLogins)
                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    <button type=\"submit\" class=\"btn btn-primary\" name=\"provider\"");
                BeginWriteAttribute("value", " value=\"", 1214, "\"", 1236, 1);
#nullable restore
#line 31 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
WriteAttributeValue("", 1222, provider.Name, 1222, 14, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("title", " title=\"", 1237, "\"", 1292, 6);
                WriteAttributeValue("", 1245, "Log", 1245, 3, true);
                WriteAttributeValue(" ", 1248, "in", 1249, 3, true);
                WriteAttributeValue(" ", 1251, "using", 1252, 6, true);
                WriteAttributeValue(" ", 1257, "your", 1258, 5, true);
#nullable restore
#line 31 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
WriteAttributeValue(" ", 1262, provider.DisplayName, 1263, 21, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue(" ", 1284, "account", 1285, 8, true);
                EndWriteAttribute();
                WriteLiteral(">");
#nullable restore
#line 31 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
                                                                                                                                                                            Write(provider.DisplayName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\n");
#nullable restore
#line 32 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
                                }

#line default
#line hidden
#nullable disable
                WriteLiteral("                            </p>\n                        </div>\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Page = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-returnUrl", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 26 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
                                                                                    WriteLiteral(Model.ReturnUrl);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["returnUrl"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-returnUrl", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["returnUrl"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 36 "C:\Users\borgm\Documents\Git Repos\Securing Applications\Securing-Applications\Home Assignment\Code\SecuringApps_WebApplication\Areas\Identity\Pages\Account\Register.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </section>\n    </div>\n</div>\n\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "34b4ee256fe9a5c1cab28d5c56f3e5b451ae469a14559", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RegisterModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<RegisterModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<RegisterModel>)PageContext?.ViewData;
        public RegisterModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
