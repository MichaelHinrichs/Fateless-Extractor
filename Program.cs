//Written for Fateless. https://store.steampowered.com/app/1186670
using System.IO;

namespace Fateless_Extractor
{
    class Program
    {
        public static BinaryReader br;
        static void Main(string[] args)
        {
            br = new(File.OpenRead(args[0]));

            if (new string(br.ReadChars(3)) != "DAT")
                throw new System.Exception("This is not the right file. Please input fateless.dat.");

            byte version = br.ReadByte();
            br.BaseStream.Position = 19;

            System.Collections.Generic.List<Subfile> subfiles = new();
            for(int i = 0; i < 470; i++)
                subfiles.Add(new());

            string path = Path.GetDirectoryName(args[0]) + "\\" + Path.GetFileNameWithoutExtension(args[0]) + "\\";
            Directory.CreateDirectory(path);
            foreach (Subfile file in subfiles)
            {
                br.BaseStream.Position = + file.start;
                BinaryWriter bw = new(File.Create(path + file.name));
                bw.Write(br.ReadBytes(file.size));
                bw.Close();
            }
        }

        class Subfile
        {
            public string name = new string(br.ReadChars(259)).TrimEnd('\0');
            public int start = br.ReadInt32();
            public int size = br.ReadInt32();
        }
    }
}
