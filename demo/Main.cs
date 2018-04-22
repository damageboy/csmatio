using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using csmatio.io;
using csmatio.types;

namespace CSMatIOTest
{
    public partial class Main : Form
    {
      bool toggleCheck;

        public Main()
        {
            InitializeComponent();
        }

      void btnRead_Click(object sender, EventArgs e)
        {
            DialogResult dRes = openFileDialog.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                var fileName = openFileDialog.FileName;

                txtOutput.Text = txtOutput.Text + "Attempting to read the file '" + fileName + "'...";
                try
                {
                    var mfr = new MatFileReader(fileName);
                    txtOutput.Text += "Done!\nMAT-file contains the following:\n";
                    txtOutput.Text += mfr.MatFileHeader + "\n";
                    foreach (var mla in mfr.Data)
                    {
                        txtOutput.Text = txtOutput.Text + mla.ContentToString() + "\n";
                    }
                }
                catch (IOException)
                {
                    txtOutput.Text = txtOutput.Text + "Invalid MAT-file!\n";
                    MessageBox.Show("Invalid binary MAT-file! Please select a valid binary MAT-file.",
                        "Invalid MAT-file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

      void btnCreate_Click(object sender, EventArgs e)
        {
            var mlList = new List<MLArray>();
            // Go through each of the options to add in the file
            if (chkCell.Checked)
            {
                mlList.Add(CreateCellArray());
            }
            if (chkStruct.Checked)
            {
                mlList.Add(CreateStructArray());
            }
            if (chkChar.Checked)
            {
                mlList.Add(CreateCharArray());
            }
            if (chkSparse.Checked)
            {
                mlList.Add(CreateSparseArray());
            }
            if (chkDouble.Checked)
            {
                mlList.Add(CreateDoubleArray());
            }
            if (chkSingle.Checked)
            {
                mlList.Add(CreateSingleArray());
            }
            if (chkInt8.Checked)
            {
                mlList.Add(CreateInt8Array());
            }
            if (chkUInt8.Checked)
            {
                mlList.Add(CreateUInt8Array());
            }
            if (chkInt16.Checked)
            {
                mlList.Add(CreateInt16Array());
            }
            if (chkUInt16.Checked)
            {
                mlList.Add(CreateUInt16Array());
            }
            if (chkInt32.Checked)
            {
                mlList.Add(CreateInt32Array());
            }
            if (chkUInt32.Checked)
            {
                mlList.Add(CreateUIn32Array());
            }
            if (chkInt64.Checked)
            {
                mlList.Add(CreateInt64Array());
            }
            if (chkUInt64.Checked)
            {
                mlList.Add(CreateUInt64Array());
            }
            if (chkImagMatrix.Checked)
            {
                mlList.Add(CreateImaginaryArray());
            }

            if (mlList.Count <= 0)
            {
                MessageBox.Show("You must select at least one MAT-file Creation Element in order to" +
                    " create a MAT-file.", "No MAT-file elements selected", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            // Get a filename name to write the file out to
            saveFileDialog.ShowDialog();
            var filename = saveFileDialog.FileName;

            txtOutput.Text += "Creating the MAT-file '" + filename + "'...";

            try
            {
                var mfw = new MatFileWriter(filename, mlList, chkCompress.Checked);
            }
            catch (Exception err)
            {
                MessageBox.Show("There was an error when creating the MAT-file: \n" + err,
                    "MAT-File Creation Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtOutput.Text += "Failed!\n";
                return;
            }

            txtOutput.Text += "Done!\nMAT-File created with the following data: \n";
            foreach (var mla in mlList)
                txtOutput.Text += mla.ContentToString() + "\n";
        }

      void btnCheckEmAll_Click(object sender, EventArgs e)
		{
			toggleCheck = !toggleCheck;

			chkCell.Checked =
			chkStruct.Checked =
			chkChar.Checked =
			chkSparse.Checked =
			chkDouble.Checked =
			chkSingle.Checked =
			chkInt8.Checked =
			chkUInt8.Checked =
			chkInt16.Checked =
			chkUInt16.Checked =
			chkInt32.Checked =
			chkUInt32.Checked =
			chkInt64.Checked =
			chkUInt64.Checked =
			chkImagMatrix.Checked =
			toggleCheck;
		}

      static MLArray CreateCellArray()
        {
            var names = new[] { "Hello", "World", "I am", "a", "MAT-file" };
            var cell = new MLCell("Names", new[] { names.Length, 1 });
            for (var i = 0; i < names.Length; i++)
                cell[i] = new MLChar(null, names[i]);
            return cell;
        }

      static MLArray CreateStructArray()
        {
            var structure = new MLStructure("X", new[] { 1, 1 });
            structure["w", 0] = new MLUInt8("", new byte[] { 1 }, 1);
            structure["y", 0] = new MLUInt8("", new byte[] { 2 }, 1);
            structure["z", 0] = new MLUInt8("", new byte[] { 3 }, 1);
            return structure;
        }

      static MLArray CreateCharArray()
        {
			    return new MLChar("AName", "Hello World v4.0!");
        }

      static MLArray CreateSparseArray()
        {
            var sparse = new MLSparse("S", new[] { 3, 3 }, 0, 3);
            sparse.SetReal(1.5, 0, 0);
            sparse.SetReal(2.5, 1, 1);
            sparse.SetReal(3.5, 2, 2);
            return sparse;
        }

      static MLArray CreateDoubleArray() => new MLDouble("Double", new[] { double.MaxValue, double.MinValue }, 1);

      static MLArray CreateSingleArray() => new MLSingle("Single", new[] { float.MinValue, float.MaxValue }, 1);

      static MLArray CreateInt8Array() => new MLInt8("Int8", new[] { sbyte.MinValue, sbyte.MaxValue }, 1);

      static MLArray CreateUInt8Array() => new MLUInt8("UInt8", new[] { byte.MinValue, byte.MaxValue }, 1);

      static MLArray CreateInt16Array()
        {
            return new MLInt16("Int16", new[] { short.MinValue, short.MaxValue }, 1);
        }

      static MLArray CreateUInt16Array()
        {
            return new MLUInt16("UInt16", new[] { ushort.MinValue, ushort.MaxValue }, 1);
        }

      static MLArray CreateInt32Array()
        {
            return new MLInt32("Int32", new[] { int.MinValue, int.MaxValue }, 1);
        }

      static MLArray CreateUIn32Array()
        {
            return new MLUInt32("UInt32", new[] { uint.MinValue, uint.MaxValue }, 1);
        }

      static MLArray CreateInt64Array()
        {
            return new MLInt64("Int64", new[] { long.MinValue, long.MaxValue }, 1);
        }

      static MLArray CreateUInt64Array()
        {
            return new MLUInt64("UInt64", new[] { ulong.MinValue, ulong.MaxValue }, 1);
        }

      static MLArray CreateImaginaryArray()
        {
            // Create a large, randomaly generated imaginary array
            var myRealNums = new long[2000];
            var myImagNums = new long[2000];
            var numGen = new Random();
            for (var i = 0; i < myRealNums.Length; i++)
            {
                myRealNums[i] = numGen.Next(int.MinValue, int.MaxValue);
                myImagNums[i] = numGen.Next(int.MinValue, int.MaxValue);
            }
            var myImagArray =
                new MLInt64("IA", myRealNums, myImagNums, myRealNums.Length / 5);
            return myImagArray;
        }
    }
}