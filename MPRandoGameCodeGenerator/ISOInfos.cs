using System.IO;
using System.Text;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Windows.Forms;

namespace MPRandoGameCodeGenerator
{
    class ISOManager
    {
        internal static String GetISOGameCode(String FileName)
        {
            if (Path.GetExtension(FileName).ToLower() == ".iso")
            {
                using (var reader = new BinaryReader(File.OpenRead(FileName)))
                    return Encoding.ASCII.GetString(reader.ReadBytes(6));
            }
            else if (Path.GetExtension(FileName).ToLower() == ".ciso")
            {
                using (var reader = new BinaryReader(File.OpenRead(FileName)))
                {
                    reader.BaseStream.Position = 0x8000;
                    return Encoding.ASCII.GetString(reader.ReadBytes(6));
                }
            }
            else
                return "ERROR";
        }

        internal static void PatchGameCode(String FileName, String GameCode)
        {
            if (Path.GetExtension(FileName).ToLower() == ".iso")
            {
                using (var file = File.Open(FileName, FileMode.Open))
                using (var writer = new BinaryWriter(file))
                {
                    writer.Write(Encoding.ASCII.GetBytes(GameCode), 0, 6);
                }
            }
            else if (Path.GetExtension(FileName).ToLower() == ".ciso")
            {
                using (var file = File.Open(FileName, FileMode.Open))
                using (var writer = new BinaryWriter(file))
                {
                    file.Position = 0x8000;
                    writer.Write(Encoding.ASCII.GetBytes(GameCode), 0, 6);
                }
            }
        }

        internal static void PatchGameTitle(String FileName, String GameTitle)
        {
            long i = 0, j = 0;
            if (Path.GetExtension(FileName).ToLower() == ".iso")
            {
                using (var file = File.Open(FileName, FileMode.Open))
                using (var reader = new BinaryReader(file))
                using (var writer = new BinaryWriter(file))
                {
                    long FST_start = 0;
                    long FST_end = 0;
                    long StringTable_start = 0;
                    if (Encoding.ASCII.GetString(reader.ReadBytes(4)) != "GM8E")
                        return;
                    file.Position = 0x20;
                    for(i=0;i<0x20;i++) writer.Write((byte)0);
                    file.Position = 0x20;
                    for (i = 0; i < GameTitle.Length; i++) writer.Write((byte)GameTitle[(int)i]);
                    file.Position = 0x424;
                    FST_start = BitConverter.ToInt32(reader.ReadBytes(4).Reverse().ToArray(), 0);
                    FST_end = FST_start + BitConverter.ToInt32(reader.ReadBytes(4).Reverse().ToArray(), 0);
                    char chr;
                    bool isValidChar;
                    for (i = FST_end - 1; i >= FST_start; i--)
                    {
                        file.Position = i;
                        chr = (char)reader.ReadByte();
                        isValidChar = chr == 0 ||
                    (chr >= 'a' && chr <= 'z') ||
                    (chr >= 'A' && chr <= 'Z') ||
                    (chr >= '0' && chr <= '9') ||
                                    chr == '-' ||
                                    chr == '.' ||
                                    chr == '_';
                        if (!isValidChar)
                        {
                            StringTable_start = i;
                            break;
                        }
                    }
                    file.Position = FST_start;
                    bool rootDir = true;
                    bool IsDir = true;
                    long EntryOffsetStringTable = 0;
                    long NumEntries = 0;
                    long NextDirEntryOffset = 0;
                    long EntryOffset = 0;
                    long FileLength = 0;
                    long oldPos = reader.BaseStream.Position;
                    String CurFileName = "";
                    while (file.Position < FST_end)
                    {
                        rootDir = file.Position == FST_start;
                        IsDir = reader.ReadByte() == 1;
                        EntryOffsetStringTable = reader.ReadByte() * 0x10000 + reader.ReadByte() * 0x100 + reader.ReadByte();
                        EntryOffset = BitConverter.ToInt32(reader.ReadBytes(4).Reverse().ToArray(), 0);
                        if (rootDir)
                        {
                            NumEntries = BitConverter.ToInt32(reader.ReadBytes(4).Reverse().ToArray(), 0);
                        }
                        else
                        {
                            if (IsDir)
                            {
                                NextDirEntryOffset = BitConverter.ToInt32(reader.ReadBytes(4).Reverse().ToArray(), 0);
                            }
                            else
                            {
                                FileLength = BitConverter.ToInt32(reader.ReadBytes(4).Reverse().ToArray(), 0);
                                oldPos = file.Position;
                                reader.BaseStream.Position = StringTable_start + EntryOffsetStringTable + 1;
                                CurFileName = "";
                                while ((chr = (char)reader.ReadByte()) != 0)
                                    CurFileName += chr;
                                file.Position = oldPos;
                                if (CurFileName == "opening.bnr")
                                {
                                    file.Position = EntryOffset + 0x1820;
                                    for (i = 0; i < 0x20; i++) writer.Write((byte)0);
                                    file.Position = EntryOffset + 0x1820;
                                    for (i = 0; i < GameTitle.Length; i++) writer.Write((byte)GameTitle[(int)i]);
                                    file.Position = EntryOffset + 0x1860;
                                    for (i = 0; i < 0x20; i++) writer.Write((byte)0);
                                    file.Position = EntryOffset + 0x1860;
                                    for (i = 0; i < GameTitle.Length; i++) writer.Write((byte)GameTitle[(int)i]);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if(Path.GetExtension(FileName).ToLower() == ".ciso")
            {
                using (var file = File.Open(FileName, FileMode.Open))
                using (var reader = new BinaryReader(file))
                using (var writer = new BinaryWriter(file))
                {
                    long FileSize = file.Length;
                    long BeforeEndFile = 0x1280000;
                    long BeforeEndFileMax = 0x300000;
                    long CISOHeader_end = 0x8000;
                    file.Position = CISOHeader_end;
                    if (Encoding.ASCII.GetString(reader.ReadBytes(4)) != "GM8E")
                        return;
                    file.Position = 0x8020;
                    for (i = 0; i < 0x20; i++) writer.Write((byte)0);
                    file.Position = 0x8020;
                    for (i = 0; i < (long)GameTitle.Length; i++) writer.Write((byte)GameTitle[(int)i]);
                    file.Position = FileSize - BeforeEndFile;
                    for(i=0;i< BeforeEndFileMax; i++)
                    {
                        file.Position = FileSize - BeforeEndFile + i;
                        if (Encoding.ASCII.GetString(reader.ReadBytes(4)) == "BNR1")
                        {
                            file.Position += 0x1820 - 4;
                            for (j = 0; j < 0x20; j++) writer.Write((byte)0);
                            file.Position -= 0x20;
                            for (j = 0; j < GameTitle.Length; j++) writer.Write((byte)GameTitle[(int)j]);
                            file.Position += 0x40 - GameTitle.Length;
                            for (j = 0; j < 0x20; j++) writer.Write((byte)0);
                            file.Position -= 0x20;
                            for (j = 0; j < GameTitle.Length; j++) writer.Write((byte)GameTitle[(int)j]);
                            break;
                        }
                    }
                }
            }
        }
    }
}