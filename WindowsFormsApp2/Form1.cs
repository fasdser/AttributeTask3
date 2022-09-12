using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Assembly assembly = null;
        public Form1()
        {
            InitializeComponent();
            FillLisstBox();
        }

        void FillLisstBox()
        {
            Array memberInfo = Enum.GetValues(typeof(MemberTypes));

            for (int i = 0; i < memberInfo.Length - 1; i++)
            {
                int index = checkedListBox1.Items.Add(memberInfo.GetValue(i));
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                try
                {
                    assembly = Assembly.Load(path);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void CloseTooStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GetView_Click(object sender, EventArgs e)
        {
            textBox.Text += "";
            textBox.Text += "Список всех типов в сборке:    " + assembly.FullName + Environment.NewLine + Environment.NewLine;
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                textBox.Text += "Type: " + type + Environment.NewLine;
                var members = type.GetMembers();
                if (members != null)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (checkedListBox1.GetItemChecked(i))
                        {
                            object element = Enum.Parse(typeof(MemberTypes), checkedListBox1.Items[i].ToString());
                            MemberTypes memberTypes = (MemberTypes)element;
                            foreach (var member in members)
                            {
                                if (member.MemberType == memberTypes)
                                {
                                    string methStr = member.MemberType + " " + member.Name + "\n";

                                    textBox.Text += methStr + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                textBox.Text += Environment.NewLine;
            }



        }
    }
}
