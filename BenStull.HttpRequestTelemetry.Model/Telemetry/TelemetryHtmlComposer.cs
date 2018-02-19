using System.Linq;
using System.Text;
using System.Xml.Linq;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Model.Telemetry
{
    public class TelemetryHtmlComposer : ITelemetryHtmlComposer
    {
        private const string _htmlTemplate = @"
<div style='width: 100%'>
  <table>
    <tr>
      <th>Metric</th>
      <th>Description</th>
      <th>Value</th>
      <th>Unit</th>
    </tr>
    {0}
  </table>
</div>
";

        public string ComposeTelemetryHtml(IHttpRequestTelemetry telemetry)
        {
            var rowData =
                from dataPoint in telemetry.TelemetryDataPoints
                select new XElement("tr",
                    new XElement("td", dataPoint.MetricName),
                    new XElement("td", dataPoint.Description),
                    new XElement("td", dataPoint.Value),
                    new XElement("td", dataPoint.Unit));

            var rowDataText = new StringBuilder();
            foreach (var row in rowData) {
                rowDataText.Append(row);
            }

            return string.Format(_htmlTemplate, rowDataText);
        }
    }
}