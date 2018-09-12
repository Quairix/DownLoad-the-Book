using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace LoadSite
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText(@"D:\page1.html", " ");//Чистим html в который будем сохранять
            int i = 3;
            System.Net.WebClient wc = new System.Net.WebClient();
            while (i < 158)
            {
                byte[] raw = wc.DownloadData("https://bookocean.net/read/b/23329/p/" + i);//Адрес страницы + цифра
                string webData = System.Text.Encoding.UTF8.GetString(raw);

                var pageDoc = new HtmlDocument();
                pageDoc.LoadHtml(webData);
                var pageText = pageDoc.ParsedText;

                if (!String.IsNullOrEmpty(pageText))
                {
                    i++;
                    string trimmed = pageText.Trim();

                    int count = trimmed.IndexOf("за неудобство.</div><div class=\"text\">") + 39;//Через исх код страницы ищем между чем заключен текст
                    int count2 = trimmed.IndexOf("<span class=\"pagenum");
                    File.AppendAllText(@"D:\page1.html", trimmed.Substring(count, count2 - count) + "\n");//Добавляем в наш html
                }
            }
        }
    }

    // тестовый комментарий
}