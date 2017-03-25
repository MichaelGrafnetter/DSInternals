namespace DSInternals.Common.Data
{
    using System.IO;

    public abstract class DPAPIObject
    {        
        public byte[] Data
        {
            get;
            protected set;
        }

        public abstract void SaveTo(string directoryPath);

        public void Save()
        {
            this.SaveTo(Directory.GetCurrentDirectory());
        }
    }
}
