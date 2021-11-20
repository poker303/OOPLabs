namespace Backups
{
    public class File
    {
        public File(string name, int size)
        {
            FileName = name;
            Size = size;
        }

        public string FileName { get; }
        public int Size { get; set; }

        public void ChangeSize(int newSize)
        {
            Size = newSize;
        }
    }
}