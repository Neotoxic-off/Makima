using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makima.Models
{
    [Serializable]
    public class FileModel
    {
        public string Path { get; set; }
        public bool Exists { get; set; }
        public bool Force { get; set; }

        public FileModel(string path)
        {
            this.Path = path;
            this.Exists = File.Exists(path);
        }

        public FileModel(string path, bool force_creation)
        {
            this.Path = path;
            this.Exists = File.Exists(path);
            this.Force = force_creation;

            Create();
        }

        private void Create()
        {
            if (this.Exists == false && Force == true)
            {
                File.Create(this.Path);
            }
        }
    }
}
