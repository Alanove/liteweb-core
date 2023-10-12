using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw.Core.WebUtils;
public static class SvgHelper
{
    public static string ReadSvgFile(string svgFilePath)
    {
        // Read the contents of the SVG file into a string.
        string svgContent = File.ReadAllText(svgFilePath.Replace("~/", "wwwroot/"));

        // Return the SVG content.
        return svgContent;
    }

    public static IHtmlContent Svg(string svgFilePath)
    {
        return new HtmlString(ReadSvgFile(svgFilePath));
    }
}