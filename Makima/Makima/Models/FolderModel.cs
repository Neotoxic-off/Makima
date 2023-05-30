using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makima.Models
{
    [Serializable]
    public class FolderModel
    {
        public string Path { get; set; }
        public bool Exists { get; set; }
        public bool Force { get; set; }

        public FolderModel(string path)
        {
            this.Path = path;
            this.Exists = Directory.Exists(path);
        }

        public FolderModel(string path, bool force_creation)
        {
            this.Path = path;
            this.Exists = Directory.Exists(path);
            this.Force = force_creation;

            Create();
        }

        private void Create()
        {
            if (this.Exists == false && Force == true)
            {
                Directory.CreateDirectory(this.Path);
            }
        }
    }
}
