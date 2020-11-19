using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Ishareshop.Api.Controllers
{
    public class CountryRegionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> InitCountryRegion()
        {
            //创建XML
            var xml = new XmlDocument();
            //载入xml
            var xmlFilePath = Path.Combine(Environment.CurrentDirectory, "Data/国家地区-中文.xml");
            xml.Load(xmlFilePath);
            //获取第一个匹配的节点
            var rootnode = xml.SelectSingleNode("Location");
            //遍历
            foreach (var item in rootnode.ChildNodes)
            {
                var e = item as XmlElement;
                string countryName= e.GetAttribute("name");
                string countryCode = e.GetAttribute("code");
                string countryParentId = "";
                //插入数据库中

                //遍历子节点
                foreach (var jitem in e.ChildNodes)
                {
                    var j = jitem as XmlElement;
                    string provinceName = j.GetAttribute("name");
                    string provinceCode = j.GetAttribute("code");
                    string provinceParentId = countryCode;
                    //插入数据库

                    //遍历三级子节点
                    foreach (var kitem in j.ChildNodes)
                    {
                        var k = kitem as XmlElement;

                        string cityName = k.GetAttribute("name");
                        string cityCode = k.GetAttribute("code");
                        string cityParentId = provinceCode;

                        //插入数据库

                        //遍历四级子节点
                        foreach (var litem in k.ChildNodes)
                        {
                            var l = litem as XmlElement;

                            string regionName = l.GetAttribute("name");
                            string regionCode = l.GetAttribute("code");
                            string regionParentId = cityCode;

                            //插入数据库

                        }
                    }
                    //WriteLine(k.Name+"："+k.InnerText);
                }
            }

            return Ok(true);
        }
        public async Task<IActionResult> WriteXmlFile()
        {
            Random rnd = new Random(100);

            XmlDocument doc = new XmlDocument();
            //添加文档声明
            var declaration = doc.CreateXmlDeclaration("1.0","utf-8","yes");
            doc.AppendChild(declaration);

            //添加根节点
            var root = doc.CreateElement("students");
            doc.AppendChild(root);

            //循环添加
            for (int i=0; i<3;i++)
            {
                //添加子节点
                var child = doc.CreateElement("student");
                //设置属性
                child.SetAttribute("姓名","叶子"+i);
                child.SetAttribute("学号",rnd.Next(1000,10000).ToString());
                child.SetAttribute("年龄",rnd.Next(10,20).ToString());
                root.AppendChild(child);

                //添加孙子节点
                var grade1 = doc.CreateElement("语文");
                grade1.InnerText = rnd.Next(100).ToString();
                var grade2 = doc.CreateElement("数学");
                grade2.InnerText = rnd.Next(100).ToString();
                var grade3 = doc.CreateElement("英语");
                grade3.InnerText = rnd.Next(100).ToString();

                child.AppendChild(grade1);
                child.AppendChild(grade2);
                child.AppendChild(grade3);
            }
            //保存文件
            string fileName = "ClassGrade.xml";
            var xmlFilePath = Path.Combine(Environment.CurrentDirectory, "Data/", fileName);

            doc.Save(xmlFilePath);

            return Ok(true);
        }
        public async Task<IActionResult> SerializerXmlFile()
        {
            Student student = new Student()
            {
                Name = "",
                Number = "",
                Age = 18
            };
            //Xml序列化器
            XmlSerializer serializer = new XmlSerializer(typeof(Student));
            //打开文件流
            using (FileStream fs=new FileStream("test.xml",FileMode.Create))
            {
                //序列化
                serializer.Serialize(fs, student);
            }
            return Ok();
        }
        public async Task<IActionResult> DeserializeXmlFile()
        {
            //xml序列化器
            XmlSerializer serializer = new XmlSerializer(typeof(Student));

            //打开文件流
            using (FileStream fs=new FileStream("test.xml",FileMode.Open))
            {
                //反序列化
                var student = serializer.Deserialize(fs) as Student;
            }
            return Ok();
        }
      
    }
    public class Student
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int Age { get; set; }
        public Score score { get; set; } = new Score();
    }
    public class Score
    {
        public string Mathematics { get; set; }
        public string Chinese { get; set; }
        public string English { get; set; }
    }
}
