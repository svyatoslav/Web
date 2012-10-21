using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Models;
using System.Xml;
using System.Xml.XPath;

namespace WebApp.Helper
{
    public static class XMLUserWriter
    {
        public static XmlDocument WriteXML(IEnumerable<User> users, Paging pagingModel, UrlOptions options)
        {
            // Create the xml document container
            XmlDocument doc = new XmlDocument();// Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);// Create the root element
            XmlElement root = doc.CreateElement("page");
            doc.AppendChild(root);

            XmlElement usersEl = CreateUsersListNode(users, doc);

            root.AppendChild(usersEl);

            XmlElement paging = CreatePagingNode(pagingModel, doc);

            root.AppendChild(paging);

            XmlElement updateUrl = doc.CreateElement("updateAction");
            updateUrl.InnerText = options.UpdateUrl;
            root.AppendChild(updateUrl);

            return doc;
        }

        private static XmlElement CreateUsersListNode(IEnumerable<User> users, XmlDocument doc)
        {
            XmlElement usersEl = doc.CreateElement("users");

            foreach (User user in users)
            {
                XmlElement userEl = doc.CreateElement("user");
                XmlElement id = doc.CreateElement("User_Id");
                id.InnerText = user.UserID.ToString();
                XmlElement name = doc.CreateElement("Name");
                name.InnerText = user.Name;
                XmlElement surname = doc.CreateElement("Surname");
                surname.InnerText = user.Surname;
                XmlElement address = doc.CreateElement("Address");
                address.InnerText = user.Address;
                XmlElement phone = doc.CreateElement("Phone");
                phone.InnerText = user.Phone;
                XmlElement checkedEl = doc.CreateElement("Checked");
                checkedEl.InnerText = user.Checked.ToString();
                userEl.AppendChild(name);
                userEl.AppendChild(surname);
                userEl.AppendChild(address);
                userEl.AppendChild(id);
                userEl.AppendChild(phone);
                userEl.AppendChild(checkedEl);
                usersEl.AppendChild(userEl);
            }
            return usersEl;
        }

        private static XmlElement CreatePagingNode(Paging pagingModel, XmlDocument doc)
        {
            XmlElement paging = doc.CreateElement("paging");
            XmlElement current = doc.CreateElement("current");
            current.InnerText = pagingModel.Current.ToString();
            XmlElement total = doc.CreateElement("total");
            total.InnerText = pagingModel.Total.ToString();
            XmlElement baseUrl = doc.CreateElement("baseUrl");
            baseUrl.InnerText = pagingModel.BaseUrl.ToString();
            XmlElement showNext = doc.CreateElement("showNext");
            showNext.InnerText = pagingModel.ShowNext.ToString();
            XmlElement showPrev = doc.CreateElement("showPrev");
            showPrev.InnerText = pagingModel.ShowPrev.ToString();
            XmlElement showFirst = doc.CreateElement("showFirst");
            showFirst.InnerText = pagingModel.ShowFirst.ToString();
            XmlElement showLast = doc.CreateElement("showLast");
            showLast.InnerText = pagingModel.ShowLast.ToString();

            XmlElement nextUrl = doc.CreateElement("nextUrl");
            nextUrl.InnerText = pagingModel.NextUrl.ToString();
            XmlElement prevUrl = doc.CreateElement("prevUrl");
            prevUrl.InnerText = pagingModel.PrevUrl.ToString();
            XmlElement firstUrl = doc.CreateElement("firstUrl");
            firstUrl.InnerText = pagingModel.FirstUrl.ToString();
            XmlElement lastUrl = doc.CreateElement("lastUrl");
            lastUrl.InnerText = pagingModel.LastUrl.ToString();



            paging.AppendChild(current);
            paging.AppendChild(showNext);
            paging.AppendChild(total);
            paging.AppendChild(showLast);
            paging.AppendChild(showFirst);
            paging.AppendChild(showPrev);

            paging.AppendChild(baseUrl);
            paging.AppendChild(nextUrl);
            paging.AppendChild(prevUrl);
            paging.AppendChild(firstUrl);
            paging.AppendChild(lastUrl);
            return paging;
        }
    }
}