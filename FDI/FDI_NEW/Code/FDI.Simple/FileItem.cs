using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class FileItem : BaseSimple
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string TypeIcon { get; set; }
        public DateTime Created { get; set; }
        public byte[] Data { get; set; }
        public string DataSize { get; set; }
        public int TotalNews { get; set; }
        public int TotalAlbum { get; set; }
        public int TotalAnswer { get; set; }
        public int TotalProduct { get; set; }
        public int TotalGuide { get; set; }
    }
    public class ModelFileItem : BaseModelSimple
    {
        public IEnumerable<FileItem> ListItem { get; set; }
    }
}
