using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using zlib;

namespace ZlibTool
{
    public partial class MainForm : Form
    {

        byte[] bytes = new byte[1];

        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {

                BinaryReader reader = new BinaryReader(File.OpenRead(dlg.FileName));

                using (MemoryStream outMemoryStream = new MemoryStream())
                {
                    using (ZOutputStream decompress = new ZOutputStream(outMemoryStream))
                    {
                        decompress.Write(reader.ReadBytes((int)reader.BaseStream.Length), 0, (int)reader.BaseStream.Length);
                        decompress.finish();
                        reader.Close();
                    }
                    bytes = outMemoryStream.ToArray();
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                using (MemoryStream outMemoryStream = new MemoryStream())
                using (ZOutputStream compress = new ZOutputStream(outMemoryStream, zlibConst.Z_DEFAULT_COMPRESSION))
                {
                    compress.Write(bytes, 0, bytes.Length);
                    compress.finish();

                    byte[] data = outMemoryStream.ToArray();
                    bytes = data;
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                BinaryWriter writer = new BinaryWriter(File.OpenWrite(dlg.FileName));
                writer.Write(bytes);
                writer.Close();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                BinaryWriter writer = new BinaryWriter(File.OpenWrite(dlg.FileName));
                writer.Write(bytes);
                writer.Close();
            }
        }
    }
}
