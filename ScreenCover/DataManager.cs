using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace ScreenCover
{
    public class DataManager
    {
        public const string Root = "DataManager";
        public string Path { get; private set; }
        public DataList DataList = new DataList();
        public FolderList FolderList = new FolderList();

        XmlDocument file = new XmlDocument();

        public DataManager(string path)
        {
            bool b = !File.Exists(path);
            Path = path;

            if (b)
            {
                file.AppendChild(file.CreateXmlDeclaration("1.0", "utf-8", null));
                file.AppendChild(file.CreateElement(Root));
                file.Save(path);
            }
            else
            {
                file.Load(path);
            }

            foreach (XmlNode xn in file.DocumentElement.ChildNodes)
            {
                try
                {
                    if (xn.Name == "Folder")
                    {
                        Folder f = new Folder(xn.Attributes["Name"].InnerText);
                        if (xn.Attributes["WindowX"] != null)
                        {
                            f.WindowX = Convert.ToInt32(xn.Attributes["WindowX"].Value);
                        }
                        if (xn.Attributes["WindowY"] != null)
                        {
                            f.WindowY = Convert.ToInt32(xn.Attributes["WindowY"].Value);
                        }
                        if (xn.Attributes["ImageW"] != null)
                        {
                            f.ImageW = Convert.ToInt32(xn.Attributes["ImageW"].Value);
                        }
                        if (xn.Attributes["ImageH"] != null)
                        {
                            f.ImageH = Convert.ToInt32(xn.Attributes["ImageH"].Value);
                        }
                        if (xn.Attributes["Transparent"] != null)
                        {
                            f.Transparent = Convert.ToDouble(xn.Attributes["Transparent"].Value);
                        }
                        ReadChilds(f, xn);
                        FolderList.Add(f);
                    }
                    else if (xn.Name == "Data")
                    {
                        DataList.Add(new Data(xn.Attributes["Name"].InnerText, xn.Attributes["Value"].InnerText));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        void ReadChilds(Folder folder, XmlNode node)
        {
            if (node.ChildNodes.Count > 0)
            {
                foreach (XmlNode xn in node.ChildNodes)
                {
                    try
                    {
                        if (xn.Name == "Folder")
                        {
                            Folder f = new Folder(xn.Attributes["Name"].InnerText);
                            if (xn.Attributes["WindowX"] != null)
                            {
                                f.WindowX = Convert.ToInt32(xn.Attributes["WindowX"].Value);
                            }
                            if (xn.Attributes["WindowY"] != null)
                            {
                                f.WindowY = Convert.ToInt32(xn.Attributes["WindowY"].Value);
                            }
                            if (xn.Attributes["ImageW"] != null)
                            {
                                f.ImageW = Convert.ToInt32(xn.Attributes["ImageW"].Value);
                            }
                            if (xn.Attributes["WindowH"] != null)
                            {
                                f.ImageH = Convert.ToInt32(xn.Attributes["ImageH"].Value);
                            }
                            if (xn.Attributes["Transparent"] != null)
                            {
                                f.Transparent = Convert.ToDouble(xn.Attributes["Transparent"].Value);
                            }
                            ReadChilds(f, xn);
                            folder.SubFolders.Add(f);
                        }
                        else if (xn.Name == "Data")
                        {
                            folder.Childs.Add(new Data(xn.Attributes["Name"].InnerText, xn.Attributes["Value"].InnerText));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public bool UpdateFolderSettings(Folder f)
        {
            foreach (XmlNode xn in file.DocumentElement.ChildNodes)
            {
                if (xn.Name == "Folder")
                {
                    if (xn.Attributes["Name"] != null)
                    {
                        if (xn.Attributes["Name"].Value == f.Name)
                        {
                            xn.Attributes.RemoveAll();
                            XmlAttribute attrName = file.CreateAttribute("Name");
                            XmlAttribute attrX = file.CreateAttribute("WindowX");
                            XmlAttribute attrY = file.CreateAttribute("WindowY");
                            XmlAttribute attrW = file.CreateAttribute("ImageW");
                            XmlAttribute attrH = file.CreateAttribute("ImageH");
                            XmlAttribute attrTransparent = file.CreateAttribute("Transparent");

                            attrName.Value = f.Name;
                            attrX.Value = f.WindowX.ToString();
                            attrY.Value = f.WindowY.ToString();
                            attrW.Value = f.ImageW.ToString();
                            attrH.Value = f.ImageH.ToString();
                            attrTransparent.Value = f.Transparent.ToString();

                            xn.Attributes.Append(attrName);
                            xn.Attributes.Append(attrX);
                            xn.Attributes.Append(attrY);
                            xn.Attributes.Append(attrW);
                            xn.Attributes.Append(attrH);
                            xn.Attributes.Append(attrTransparent);

                            file.Save(Path);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }

    public class Folder
    {
        public string Name { get; set; }
        public int WindowX = -1;
        public int WindowY = -1;
        public int ImageW = -1;
        public int ImageH = -1;
        public double Transparent = -1;

        public FolderList SubFolders = new FolderList();
        public DataList Childs = new DataList();

        public Folder() { }
        public Folder(object name)
        {
            this.Name = name.ToString();
        }

        public bool HasChild()
        {
            if (Childs.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool HasSubFolders()
        {
            if (SubFolders.Count > 0)
            {
                return true;
            }
            return false;
        }
    }

    public class FolderList : IEnumerable
    {
        List<Folder> list = new List<Folder>();

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public Folder this[string name]
        {
            get
            {
                foreach (Folder f in list)
                {
                    if (f.Name == name)
                    {
                        return f;
                    }
                }
                return null;
            }
            set
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Name == name)
                    {
                        list[i] = value;
                        return;
                    }
                }
                throw new Exception("Can't find that Folder");
            }
        }

        public Folder this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                if (index < 0 || index >= list.Count)
                {
                    throw new Exception("Can't find that Data");
                }
                list[index] = value;
            }
        }

        public void Add(Folder folder)
        {
            list.Add(folder);
        }

        public void Remove(string name)
        {
            Folder tmp = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == name)
                {
                    tmp = list[i];
                    break;
                }
            }

            list.Remove(tmp);
        }

        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }

    public class Data
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Data() { }
        public Data(object name, object value)
        {
            this.Name = name.ToString();
            this.Value = value.ToString();
        }
    }

    public class DataList : IEnumerable
    {
        List<Data> list = new List<Data>();

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public Data this[string name]
        {
            get
            {
                foreach (Data d in list)
                {
                    if (d.Name == name)
                    {
                        return d;
                    }
                }
                return null;
            }
            set
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Name == name)
                    {
                        list[i] = value;
                        return;
                    }
                }
                throw new Exception("Can't find that Data");
            }
        }

        public Data this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                if (index < 0 || index >= list.Count)
                {
                    throw new Exception("Can't find that Data");
                }
                list[index] = value;
            }
        }

        public void Add(Data data)
        {
            list.Add(data);
        }

        public void Remove(string name)
        {
            Data tmp = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == name)
                {
                    tmp = list[i];
                    break;
                }
            }

            list.Remove(tmp);
        }

        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
