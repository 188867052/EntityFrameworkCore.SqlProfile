using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.SqlProfile
{
    public class EntityFrameworkLogger : ILogger
    {
        private readonly string _categoryName;
        public const string DefaultRoute = "logger";
        public static IList<SqlInfo> SqlInfoCache { get; set; } = new List<SqlInfo>();

        public EntityFrameworkLogger(string categoryName)
        {
            _categoryName = categoryName;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (_categoryName == "Microsoft.EntityFrameworkCore.Database.Command")
            {
                string logContent = formatter(state, exception);
                if (logContent != "A data reader was disposed.")
                {
                    var sql = ConvertToSql(logContent, out string ms);
                    var SqlOperateType = sql.StartsWith("SELECT") ? SqlTypeEnum.Select :
                                         sql.StartsWith("UPDATE") ? SqlTypeEnum.Update :
                                         sql.StartsWith("DELETE") ? SqlTypeEnum.Delete :
                                         sql.StartsWith("INSERT") ? SqlTypeEnum.Insert :
                                         sql.StartsWith("CREATE") ? SqlTypeEnum.Create :
                                         SqlTypeEnum.None;
                    if (SqlOperateType != SqlTypeEnum.None)
                    {
                        if (!string.IsNullOrEmpty(ms))
                        {
                            SqlInfoCache.Add(new SqlInfo()
                            {
                                ExcuteTime = ms,
                                Sql = sql,
                                Type = SqlOperateType,
                                RequestTime = DateTime.Now
                            });
                        }
                    }
                };
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        private string ConvertToSql(string content, out string ms)
        {
            string sql = Regex.Match(content, "(SELECT|UPDATE|INSERT|DELETE)+(.|\\n)+").Value;
            ms = Regex.Match(content, "([0-9,]+ms)")?.Value;
            string pare = Regex.Match(content, "Parameters[^\\]]+").Value.Replace("Parameters=[", string.Empty);
            var matches = Regex.Matches(pare, "@[^@='()]+(=')[^@='()]+(')");
            foreach (Match item in matches)
            {
                string key = item.Value.Split('=')[0];
                string value = item.Value.Split('=')[1].Replace(",", string.Empty);
                sql = sql.Replace(key, value);
            }

            Match match = Regex.Match(sql, "OFFSET+.+ROWS FETCH NEXT+.+ROWS ONLY");
            if (!string.IsNullOrEmpty(match.Value))
            {
                string pagSql = match.Value;
                sql = sql.Replace(pagSql, pagSql.Replace("'", string.Empty));
            }

            return sql;
        }

        public static string GetHtml(IList<SqlInfo> infos)
        {
            var head = HtmlContent.TagHelper("tr");
            head.Content.AppendHtml(HtmlContent.TagHelper("th", nameof(SqlInfo.Index)));
            head.Content.AppendHtml(HtmlContent.TagHelper("th", nameof(SqlInfo.RequestTime)));
            head.Content.AppendHtml(HtmlContent.TagHelper("th", nameof(SqlInfo.ExcuteTime)));
            head.Content.AppendHtml(HtmlContent.TagHelper("th", nameof(SqlInfo.Type)));
            head.Content.AppendHtml(HtmlContent.TagHelper("th", nameof(SqlInfo.Sql)));

            var thead = HtmlContent.TagHelper("thead", head);

            var tbody = HtmlContent.TagHelper("tbody");
            infos = infos.OrderByDescending(o => o.RequestTime).ToList();
            foreach (var item in infos)
            {
                var tr = HtmlContent.TagHelper("tr");
                tr.Content.AppendHtml(HtmlContent.TagHelper("td", new TagHelperAttribute("class", "text-muted"), (1 + infos.IndexOf(item)).ToString()));
                tr.Content.AppendHtml(HtmlContent.TagHelper("td", item.RequestTime.ToString()));
                tr.Content.AppendHtml(HtmlContent.TagHelper("td", item.ExcuteTime));
                tr.Content.AppendHtml(HtmlContent.TagHelper("td", item.Type.ToString()));
                tr.Content.AppendHtml(HtmlContent.TagHelper("td", item.Sql));
                tbody.Content.AppendHtml(tr);
            }

            TagHelperAttributeList attributes = new TagHelperAttributeList
            {
                { "class", "table table-bordered table-striped table-hover" },
                { "style", "font-size: 15px" },
            };
            var table = HtmlContent.TagHelper("table", attributes, thead, tbody);
            using (var writer = new StringWriter())
            {
                var title = HtmlContent.TagHelper("title", "services");
                var htmlHead = HtmlContent.TagHelper("head", title);
                var css = HtmlContent.TagHelper("style", HtmlContent.Style);
                var htmlBody = HtmlContent.TagHelper("body", css, table);
                htmlHead.PostElement.SetHtmlContent(htmlBody);
                htmlHead.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}