#pragma checksum "C:\Users\jakcy\OneDrive\Desktop\WebApplication2\WebApplication2\Views\Home\UploadTalent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ebaeb7c90b01e3ce355b226e7f804f352a3f7219"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_UploadTalent), @"mvc.1.0.view", @"/Views/Home/UploadTalent.cshtml")]
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
#line 1 "C:\Users\jakcy\OneDrive\Desktop\WebApplication2\WebApplication2\Views\_ViewImports.cshtml"
using WebApplication2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\jakcy\OneDrive\Desktop\WebApplication2\WebApplication2\Views\_ViewImports.cshtml"
using WebApplication2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ebaeb7c90b01e3ce355b226e7f804f352a3f7219", @"/Views/Home/UploadTalent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b36aee4455a440795f240a74431c307640c545e", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_UploadTalent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""jumbotron"">
    <h1>Task 7</h1>
</div>

<input type=""file"" id=""inputFile"" name=""filename"">
<br />
Name
<input type=""text"" id=""Name"" name=""Name"">
<br />
Reknown
<input type=""text"" id=""Reknown"" name=""Reknown"">
<br />
Bio
<input type=""text"" id=""Bio"" name=""Bio"">

<div class=""image-preview"" id=""imagePreview"">
    <img");
            BeginWriteAttribute("src", " src=\"", 342, "\"", 348, 0);
            EndWriteAttribute();
            WriteLiteral(" alt=\"Image Preview\" class=\"image-preview__image\" />\r\n</div>\r\n<br />\r\n<br />\r\n<input type=\"button\" class=\"btn btn-primary\" value=\"Create Talent\" id=\"tagButton\" />\r\n\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script src=""https://sdk.amazonaws.com/js/aws-sdk-2.1.24.min.js""></script>

    <script>
        jQuery('#tagButton').on('click', function () {
            var bio = document.getElementById(""Bio"").value;
            var reknown = document.getElementById(""Reknown"").value;
            var name = document.getElementById(""Name"").value;
            var shortname = document.getElementById(""Name"").value.replaceAll("" "", ""-"");
            console.log(shortname);
            var formData = new FormData();
            formData.append('file', document.getElementById('inputFile').files[0]);
            console.log(formData);
            loadData();

            function loadData() {
                $.ajax({
                    method:
                        'POST',
                    url: `/ImageRecognition/ImageRecognition`,
                    data: formData,
                    processData: false,  // tell jQuery not to process the data
                    contentType: false,
             ");
                WriteLiteral(@"       async: true,
                    cache: false,
                }).done(function (data) {
                    console.log(data);
                    if (data == false) {
                        alert(""Not a human!"");
                    } else {
                        AWS.config.update({
                            accessKeyId: 'AKIAJMSEJ5ZE3UINM7QA',
                            secretAccessKey: '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',
                            sessionToken: ''
                        });
                        AWS.config.region = 'us-east-1';
                        var s3 = new AWS.S3({
                            params: { Bucket: 'cscasgn-task-5' }
                        });
                        var file = document.getElementById('inputFile').files[0];
                        if (file) {
                            s3.putObject({
                                Key: file.name,
                                ContentType: file.type,
                   ");
                WriteLiteral(@"             Body: file,
                                ACL: ""public-read""
                            },
                                function (err, data) {
                                    if (data !== null) {
                                    }
                                    else {
                                        alert(""Upload failed!"");
                                    }
                                });
                        }


                        $.ajax({
                            method:
                                'POST',
                            url: `/Talent/Upload?Bio=${bio}&Photo=${""https://cscasgn-task-5.s3-ap-southeast-1.amazonaws.com/"" + file.name}&Name=${name}&Shortname=${shortname}&Reknown=${reknown}`,
                            dataType: 'json',
                            async: true,
                            cache: false,
                        }).done(function (data) {
                            alert(""Verified is a hum");
                WriteLiteral(@"an,uploaded image to S3 and inserted a record to database."");
                        }).fail(function (jqXHR) {
                            if (parseInt(jqXHR.status) === 400) {
                                window.alert(""Error, please try again"")
                            }
                        });

                    }

                }).fail(function (jqXHR) {
                    if (parseInt(jqXHR.status) === 400) {
                        window.alert(""Error, please try again"")
                    }
                });//End of ajax().done()//End of ajax().done()
            }//End of loadData
        })

        const inputFile = document.getElementById('inputFile');
        const previewElement = document.getElementById('imagePreview');
        const previewImage = previewElement.querySelector("".image-preview__image"")
        var fileNameFirst = """";
        var fileNameSecond = """";

        inputFile.addEventListener(""change"", function () {
            const file = this");
                WriteLiteral(@".files[0];

            if (file) {
                const reader = new FileReader();

                reader.addEventListener(""load"", function () {
                    console.log(this);
                    previewImage.setAttribute(""src"", this.result);
                });
                reader.readAsDataURL(file);
                var fullFileName = inputFile.value.split(""\\"").pop();
                fileNameFirst = fullFileName.substr(0, fullFileName.lastIndexOf("".""));
                fileNameSecond = fullFileName.split(""."").pop();
                console.log(fullFileName);
                console.log(fileNameFirst);
                console.log(fileNameSecond);
            } else {
                window.alert(""Invalid image file uploaded"");
            }
        });
    </script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
