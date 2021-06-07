using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadHTML
{
    class Program
    {
        static void Main(string[] args)
        {
            string htmlFilePath = @"C:\_MyProjects\ReadHTMLPage\_NET 6 Preview 4 Released.html";
            string jsonFilePath = @"Files\nodes.json";
            HtmlDocument doc = ReadFile(htmlFilePath);

            Markup root = new Markup();
            root.Children = IterateHTMLElements(doc.DocumentNode.SelectNodes("//div[@class='b-container page-body']").FirstOrDefault());

            CreateJsonFile(root.Children, jsonFilePath);
        }

        static void CreateJsonFile(object data, string filePath)
        {
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory.Split("bin").ToList().FirstOrDefault() + filePath, JsonConvert.SerializeObject(data));
        }
        static HtmlDocument ReadFile(string filePath)
        {
            HtmlDocument document = new HtmlDocument();
            document.Load(filePath);
            return document;
        }
        static List<Markup> IterateHTMLElements(HtmlNode parent)
        {
            List<Markup> childNodes = new List<Markup>();

            foreach (HtmlNode node in parent.ChildNodes)
            {
                Markup childNode = new Markup();

                childNode.Name = node.Name;
                childNode.NodeType = node.NodeType.ToString();
                childNode.Value = node.InnerText;
                childNode.Attributes = new Dictionary<string, string>();
                Console.WriteLine(node.InnerText);

                if (node.HasAttributes)
                {
                    IEnumerable<HtmlAttribute> attributes = node.GetAttributes();
                    foreach (HtmlAttribute attribute in attributes)
                    {    
                        childNode.Attributes.Add(attribute.Name.ToString(), attribute.DeEntitizeValue.ToString());
                    }
                }

                childNode.Children = IterateHTMLElements(node);
                childNodes.Add(childNode);
            }

            return childNodes;
        }
    }
}
